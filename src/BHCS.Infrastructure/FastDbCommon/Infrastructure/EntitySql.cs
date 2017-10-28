using BHCS.Infrastructure.FastDbCommon.Persistents.Model;
using SkyCreative.QuickApp.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure
{
    public abstract class EntitySql
    {
        private string _insertSql;
        private string _updateSql;
        private string _deleteSql;
        private string _tableName;
        private string _selectByIdSql;
        private IList<string> _keys;
        private readonly Type _entityType;
        private readonly Database _database;

        public string InsertSql { get { return _insertSql; } }

        public string UpdateSql { get { return _updateSql; } }

        public string DeleteSql { get { return _deleteSql; } }

        public string SelectByIdSql { get { return _selectByIdSql; } }

        public string TableName { get { return _tableName; } }

        public IList<string> Keys { get { return _keys; } }

        public Type EntityType { get { return _entityType; } }

        public Database Database { get { return _database; } }

        public EntitySql(Type entityType,Database database)
        {
            _entityType = entityType;
            _database = database;

            GetTableName();
            _keys=CreateKeys();
            _insertSql=CreateInsertSql();
            _updateSql= CreateUpdateSql();
            _deleteSql= CreateDeleteSql();
            _selectByIdSql = CreateSelectByIdSql();
        }

        protected virtual string CreateInsertSql()
        {
            string sql = "INSERT INTO " + TableName + " ({0}) VALUES ({1}) ";
            IDictionary<string, string> columns = SqlExtension.GetPropertyList(EntityType); //获取实体的属性及属性值的字典集合

            List<object> param = new List<object>();
            StringBuilder sbFieldName = new StringBuilder();
            StringBuilder sbParamName = new StringBuilder();

            foreach (KeyValuePair<string, string> column in columns)
            {
                sbFieldName.AppendFormat("{0},", column.Key);
                sbParamName.AppendFormat("@{0},", column.Value);
            }
            if (sbFieldName.Length > 0)
            {
                sbFieldName.Length--;
                sbParamName.Length--;
            }
            return sql = string.Format(sql, sbFieldName, sbParamName);
        }

        protected virtual string CreateUpdateSql()
        {
            string sql = "UPDATE {0} SET {1} WHERE {2}";
            IDictionary<string, string> columns = SqlExtension.GetPropertyList(EntityType);
            StringBuilder sbFieldName = new StringBuilder();
            StringBuilder sbWhereCondition = new StringBuilder();
            int counter = 0;
            foreach (KeyValuePair<string, string> column in columns)
            {
                sbFieldName.AppendFormat(" {0}=@{1}, ", column.Key, column.Value);
                counter++;
            }
            for (int i = 0; i < Keys.Count; i++)
            {
                if (i > 0)
                {
                    sbWhereCondition.Append(" AND ");
                }
                sbWhereCondition.AppendFormat(" {0} = @{1}", Keys[i], counter++);
            }

            string updateFieldName = sbFieldName.ToString().Substring(0, sbFieldName.Length - 2);
            string primaryKeyFieldName = sbWhereCondition.ToString();
            sql = string.Format(sql, TableName, updateFieldName, primaryKeyFieldName);

            return sql;
        }

        protected virtual string CreateDeleteSql()
        {
            string sql = string.Format("DELETE FROM {0} WHERE ", TableName);
            for (int i = 0; i < Keys.Count; i++)
            {
                sql = string.Format(" {0} {1} {2}=@{3} ", sql, i == 0 ? string.Empty : " AND ", Keys[i], i);
            }

            return sql;
        }

        protected virtual string CreateSelectByIdSql()
        {
            int count = 0;
            if (_keys.Count >= Keys.Count)
                count = Keys.Count;
            else if (_keys.Count < Keys.Count)
                count = _keys.Count;

            StringBuilder strSelectSql = new StringBuilder();
            Object[] keyValues = new Object[count];
            strSelectSql.AppendFormat("select * from {0} where 1=1 ", _tableName);
            for (int i = 0; i < count; i++)
            {
                strSelectSql.AppendFormat(" and {0}=@{1}", this.Keys[i], i);
                keyValues[i] = _keys[i];
            }

            return strSelectSql.ToString();
        }

        protected abstract IList<string> CreateKeys();

        private void GetTableName()
        {
            if (string.IsNullOrWhiteSpace(_tableName))
            {
                TableAttribute dbConfigAttribute = EntityType.GetTypeInfo().GetCustomAttribute<TableAttribute>(true);
                if (dbConfigAttribute == null)
                    _tableName= EntityType.Name;
                else
                    _tableName = dbConfigAttribute.TableName;
            }
        }
        
    }
}
