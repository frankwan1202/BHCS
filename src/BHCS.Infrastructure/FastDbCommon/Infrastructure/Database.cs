using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using BHCS.Infrastructure.FastDbCommon.Persistents;
using System.Reflection;
using System.Text.RegularExpressions;
using SkyCreative.QuickApp.Data.Extensions;
using BHCS.Infrastructure.FastDbCommon.Persistents.Transactions;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure
{
    public abstract class Database:IInterpreterExecutor
    {
        private readonly string _connectString;

        public Database(string nameOrConnectString)
        {
            _connectString = nameOrConnectString;
        }

        public String ConnectString { get { return _connectString; } }

        public abstract IDbTransaction CreateTransaction(IDbConnection connection=null);

        public abstract IDbConnection CreateConnection();

        public abstract object ExecuteScalar(DbCommand command);

        public abstract DataTable ExecuteDataTable(DbCommand command);

        public abstract IDataReader ExecuteDataReader(DbCommand command);

        public bool Insert(object entity, IDbTransaction transaction =null )
        {
            IDictionary<string, object> columns = SqlExtension.GetPropertyKeyValueList(entity);

            IList<object> valueList = columns.Values.ToList();
            for (int i = 0; i < valueList.Count; i++)
            {
                object value = valueList[i];
                if (value == null)
                {
                    continue;
                }
                Type valueType = value.GetType();
                if (valueType.GetTypeInfo().IsClass && valueType.GetTypeInfo().IsGenericType)
                {
                    valueList.Remove(value);
                    continue;
                }
            }

            bool result = ExecuteSql(GetEntitySql(entity.GetType()).InsertSql, valueList.ToArray(), transaction);
            if (!result)
                throw new PersistentException("执行实体 " + entity.GetType().Name + "添加操作失败，请查看数据日志！");

            return result;
        }

        public bool Modify(object entity, IDbTransaction transaction = null)
        {
            
            string[] updateSql = Regex.Split(GetEntitySql(entity.GetType()).UpdateSql, "where", RegexOptions.IgnoreCase);
            IList<string> keys = GetEntitySql(entity.GetType()).Keys;

            IDictionary<string, object> columns = SqlExtension.GetPropertyKeyValueList(entity);
            IList<Object> keyValues = new List<Object>();
            IList<Object> valueList = new List<Object>();
            foreach (KeyValuePair<String, Object> column in columns)
            {
                if (column.Value == null)
                {
                    continue;
                }

                Type valueType = column.Value.GetType();
                if (valueType.GetTypeInfo().IsClass && valueType.GetTypeInfo().IsGenericType)
                    continue;

                if (keys.Contains(column.Key))
                    keyValues.Add(column.Value);

                valueList.Add(column.Value);
            }
            foreach (Object keyValue in keyValues)
            {
                valueList.Add(keyValue);
            }

            bool result = ExecuteSql(updateSql[0] + " where " + updateSql[1], valueList.ToArray(), transaction);
            if (!result)
                throw new PersistentException("执行实体 " + entity.GetType().Name + "修改操作失败，请查看数据日志！");

            return result;
        }

        public bool Delete(object entity, IDbTransaction transaction = null)
        {
            IList<string> keys = GetEntitySql(entity.GetType()).Keys;

            IDictionary<string, object> columns = SqlExtension.GetPropertyKeyValueList(entity);
            IList<Object> keyValues = new List<Object>();
            foreach (string key in keys)
            {
                keyValues.Add(columns[key]);
            }

            bool result = this.ExecuteSql(GetEntitySql(entity.GetType()).DeleteSql, keyValues.ToArray(), transaction);
            if (!result)
                throw new PersistentException("执行实体 " + entity.GetType().Name + "删除操作失败，请查看数据日志！");

            return result;
        }

        public abstract TEntity GetById<TEntity>(object id);

        protected abstract EntitySql GetEntitySql(Type entityType);

        protected abstract bool ExecuteSql(string executeSql,object[] parameters,IDbTransaction transaction=null);
    }
}
