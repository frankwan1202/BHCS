using BHCS.Infrastructure.FastDbCommon.Persistents.Model;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyCreative.QuickApp.Data.Extensions
{
    public static class SqlExtension
    {
        public static string BuildCreateSql(this Type entityType, string tableName)
        {
            string sql = "INSERT INTO " + tableName + " ({0}) VALUES ({1}) ";
            IDictionary<string, string> columns = GetPropertyList(entityType); //获取实体的属性及属性值的字典集合

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

        public static string BuildUpdateSql(this Type entityType, IList<String> keys, string tableName)
        {
            string sql = "UPDATE {0} SET {1} WHERE {2}";
            IDictionary<string, string> columns = GetPropertyList(entityType);
            StringBuilder sbFieldName = new StringBuilder();
            StringBuilder sbWhereCondition = new StringBuilder();
            int counter = 0;
            foreach (KeyValuePair<string, string> column in columns)
            {
                sbFieldName.AppendFormat(" {0}=@{1}, ", column.Key, column.Value);
                counter++;
            }
            for (int i = 0; i < keys.Count; i++) 
            {
                if (i > 0)
                {
                    sbWhereCondition.Append(" AND ");
                }
                sbWhereCondition.AppendFormat(" {0} = @{1}", keys[i], counter++);
            }

            string updateFieldName = sbFieldName.ToString().Substring(0, sbFieldName.Length - 2);
            string primaryKeyFieldName = sbWhereCondition.ToString();
            sql = string.Format(sql, tableName, updateFieldName, primaryKeyFieldName);

            return sql;
        }

        public static string BuildDeleteSql(this Type entityType, IList<String> keys, string tableName)
        {
            string sql = string.Format("DELETE FROM {0} WHERE ", tableName);
            for (int i = 0; i < keys.Count; i++)
            {
                sql = string.Format(" {0} {1} {2}=@{3} ", sql, i == 0 ? string.Empty : " AND ",keys[i], i);
            }

            return sql;
        }

        /// <summary>
        /// 获取数据实体的字段信息
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetPropertyList(this Type entityType)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            PropertyInfo[] properties = entityType.GetAccordPropertyInfoArrForEntityType();
            int i = 0;
            foreach (PropertyInfo property in properties)
            {
                if (RefelectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrNull<NotMappedAttribute>(property) != null)
                {
                    continue;
                }

                string propertyName = property.Name;
                list.Add(propertyName,i.ToString());
                i++;
            }
            return list;
        }

        /// <summary>
        /// 获取数据实体的字段信息
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetPropertyKeyValueList(object entity)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            PropertyInfo[] properties = entity.GetType().GetAccordPropertyInfoArrForEntityType();
            foreach (PropertyInfo property in properties)
            {
                if (RefelectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrNull<NotMappedAttribute>(property) != null)
                {
                    continue;
                }

                string propertyName = property.Name;
                Type propertyType = property.PropertyType;
                object value;
                if (propertyType.GetTypeInfo().IsEnum)
                {
                    value = (int)property.GetValue(entity,null);
                }
                else
                {
                    value = property.GetValue(entity, null); //获取该属性字段的值
                }

                if (value == null)
                {
                    value = DBNull.Value;
                }
                //if (value != DBNull.Value && value != null)
                //{
                    list.Add(propertyName.ToLower(), value);
                //}
            }
            return list;
        }

        public static PropertyInfo[] GetAccordPropertyInfoArrForEntityType(this Type entityType)
        {
            return entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase).OrderBy(p=>p.Name).ToArray();
        }

        public static Object ConvertSelectSqlValue(this Object value)
        {
            if (typeof(Boolean) == value.GetType())
                return Boolean.Parse(value.ToString()) ? '1' : '0';

            if (typeof(DateTime) == value.GetType())
            {
                if (((DateTime)value).Year == 1)
                {
                    return null;
                }
                return "'" + DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }

            if (typeof(Guid) == value.GetType())
                return "'" + value.ToString() + "'";

            if (typeof(String) == value.GetType())
                return "'" + value.ToString() + "'";

            return value;
        }

        public static object[] SensitiveWordFilter(object[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            { 
                if(parameters[i]==null||parameters[i].GetType()!=typeof(string))
                {
                    continue;
                }

                parameters[i] = parameters[i];
            }

            return parameters;
        }

        #region 添加SQL参数扩展方法

        /// <summary>
        /// 添加多个参数
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <param name="param">参数</param>
        public static void AddParams(this DbCommand cmd, params object[] param)
        {
            foreach (object p in param)
            {
                AddParam(cmd, p);
            }
        }

        /// <summary>
        /// 添加单个参数
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <param name="param">参数</param>
        public static void AddParam(this DbCommand cmd, object param)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = string.Format("@{0}", cmd.Parameters.Count);
            if (param == null)
            {
                parameter.Value = DBNull.Value;
            }
            else if (param.GetType() == typeof(DateTime) && DateTime.Parse(param.ToString()) == DateTime.MinValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = param;
            }
            cmd.Parameters.Add(parameter);
        }

        #endregion
    }
}
