using BHCS.Infrastructure.FastDbCommon.Querying.Clips;
using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Model
{
    public class Field
    {
        public static readonly Field All = new Field("*");

        /// <summary>
        /// LIKE %
        /// </summary>
        private const string likeString = "%";

        /// <summary>
        /// IN
        /// </summary>
        private const string selectInString = " IN ";

        /// <summary>
        /// NOT IN
        /// </summary>
        private const string selectNotInString = " NOT IN ";

        private string tableName;
        private string fieldName;
        private DbType? parameterDbType;
        private int? parameterSize;
        private string description;
        private string aliasName;

        Field() { }

        public Field(string fieldName) : this(fieldName, null) { }

        public Field(string fieldName, string tableName) : this(fieldName, tableName, null, null, fieldName) { }

        public Field(string fieldName, string tableName, string description)
            : this(fieldName, tableName, null, null, description)
        { }

        public Field(string fieldName, string tableName, DbType? parameterDbType, int? parameterSize, string description)
            : this(fieldName, tableName, parameterDbType, parameterSize, description, null)
        { }

        public Field(string fieldName, string tableName, DbType? parameterDbType, int? parameterSize, string description, string aliasName)
        {
            this.fieldName = fieldName;
            this.tableName = tableName;
            //this.description = description;
            //this.aliasName = aliasName;
            //this.parameterDbType = parameterDbType;
            //this.parameterSize = parameterSize;
            
        }

        /// <summary>
        /// 字段数据库中类型
        /// </summary>
        public DbType? ParameterDbType
        {
            get { return parameterDbType; }
        }

        /// <summary>
        /// 字段数据库中长度
        /// </summary>
        public int? ParameterSize
        {
            get { return parameterSize; }
        }

        /// <summary>
        /// 返回  别名，当别名为空返回字段名
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(aliasName))
                    return fieldName;
                return aliasName;
            }
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(aliasName))
                    return TableFieldName;

                return string.Concat(TableFieldName, " AS {0}", aliasName, "{1}");
            }
        }

        public GroupByClip GroupBy
        {
            get
            {
                return new GroupByClip(this);
            }
        }

        /// <summary>
        /// 返回 字段名
        /// </summary>
        public string FieldName
        {
            get
            {
                if (fieldName.Trim() == "*" || fieldName.IndexOf('\'') >= 0
                    || fieldName.IndexOf('(') >= 0 || fieldName.IndexOf(')') >= 0
                    || fieldName.Contains("{0}") || fieldName.Contains("{1}")
                    || fieldName.IndexOf(" as ", StringComparison.OrdinalIgnoreCase) >= 0
                    || fieldName.Contains("*")
                    || fieldName.IndexOf("distinct ", StringComparison.OrdinalIgnoreCase) >= 0
                    || fieldName.IndexOf('[') >= 0 || fieldName.IndexOf(']') >= 0
                    || fieldName.IndexOf('"') >= 0 || fieldName.IndexOf('`') >= 0)
                {
                    return fieldName;
                }

                return string.Concat("{0}", fieldName, "{1}");
            }
        }


        /// <summary>
        /// 返回  表名
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(tableName))
                    return tableName;

                return string.Concat("{0}", tableName, "{1}");
            }
        }

        /// <summary>
        /// 返回属性名  即fileName
        /// </summary>
        public string PropertyName
        {
            get { return fieldName; }
        }

        /// <summary>
        /// 返回  别名
        /// </summary>
        public string AliasName
        {
            get
            {
                return aliasName;
            }
            set
            {
                aliasName = value;
            }
        }

        public string TableFieldName
        {
            get
            {
                if (string.IsNullOrEmpty(tableName))
                    return FieldName;

                return string.Concat(TableName, ".", FieldName);
            }
        }

        /// <summary>
        /// 倒叙
        /// </summary>
        public OrderByClip Desc
        {
            get
            {
                return new OrderByClip(this, OrderByOperater.DESC);
            }
        }
        
        /// <summary>
        /// 正序
        /// </summary>
        public OrderByClip Asc
        {
            get
            {
                return new OrderByClip(this, OrderByOperater.ASC);
            }
        }
        
        public Field As(string aliasName)
        {
            return new Field(this.fieldName, this.tableName, this.parameterDbType, this.parameterSize, this.description, aliasName);
        }
        
        /// <summary>
        /// AS
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public Field As(Field field)
        {
            return As(field.Name);
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(Field field)
        {
            if (null == (object)field || string.IsNullOrEmpty(field.PropertyName))
                return true;
            return false;
        }
        
        /// <summary>
        /// where field in (value,value,value)。同In.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public WhereClip SelectNotIn(params object[] values)
        {
            return selectInOrNotIn<object>(this, selectNotInString, values);
        }

        /// <summary>
        /// 同SelectNotIn。
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public WhereClip NotIn(params object[] values)
        {
            return SelectNotIn(values);
        }

        /// <summary>
        /// SelectIn  
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public WhereClip SelectIn(params object[] values)
        {
            return selectInOrNotIn<object>(this, selectInString, values);
        }

        /// <summary>
        /// 字段 为null <example>field is null</example>
        /// </summary>
        /// <returns></returns>
        public WhereClip IsNull()
        {
            return new WhereClip(string.Concat(this.TableFieldName, " is null "));
        }

        /// <summary>
        /// 字段 为null <example>field is not null</example>
        /// </summary>
        /// <returns></returns>
        public WhereClip IsNotNull()
        {
            return new WhereClip(string.Concat(this.TableFieldName, " is not null "));
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public Field Count()
        {
            if (this.PropertyName.Trim().Equals("*"))
                return new Field("count(*)").As("cnt");
            return new Field(string.Concat("count(", this.TableFieldName, ")")).As(this);
        }

        /// <summary>
        /// Sum
        /// </summary>
        /// <returns></returns>
        public Field Sum()
        {
            return new Field(string.Concat("sum(", this.TableFieldName, ")")).As(this);
        }

        /// <summary>
        /// Avg
        /// </summary>
        /// <returns></returns>
        public Field Avg()
        {
            return new Field(string.Concat("avg(", this.TableFieldName, ")")).As(this);
        }

        /// <summary>
        /// len
        /// </summary>
        /// <returns></returns>
        public Field Len()
        {
            return new Field(string.Concat("len(", this.TableFieldName, ")")).As(this);
        }

        /// <summary>
        /// 子查询
        /// </summary>
        /// <param name="field"></param>
        /// <param name="join"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private WhereClip selectInOrNotIn<T>(Field field, string join, params T[] values)
        {
            return selectInOrNotIn<T>(field, join, true, values);
        }
        
        /// <summary>
        /// 子查询
        /// </summary>
        /// <param name="field"></param>
        /// <param name="join"></param>
        /// <param name="isParameter">是否参数化</param>
        /// <param name="values"></param>
        /// <returns></returns>
        private WhereClip selectInOrNotIn<T>(Field field, string join, bool isParameter, params T[] values)
        {
            if (values.Length == 0)
            {
                return new WhereClip("1=2");
                //2015-09-22注释
                //return WhereClip.All;
            }

            var whereString = new StringBuilder(field.TableFieldName);
            whereString.Append(join);
            whereString.Append("(");
            var ps = new List<Parameter>();
            var inWhere = new StringBuilder();
            var i = 0;
            foreach (T value in values)
            {
                i++;
                string paraName = null;

                if (isParameter)
                {
                    paraName = DbUtils.MakeUniqueKey(field);
                    // paraName = field.tableName + field.Name + i;
                    Parameter p = new Parameter(paraName, value, field.ParameterDbType, field.ParameterSize);
                    ps.Add(p);
                }
                else
                {
                    if (value == null)
                        continue;

                    paraName = value.ToString();

                    if (string.IsNullOrEmpty(paraName))
                        continue;

                }

                inWhere.Append(",");
                inWhere.Append(paraName);
            }
            whereString.Append(inWhere.ToString().Substring(1));
            whereString.Append(")");

            return new WhereClip(whereString.ToString(), ps.ToArray());
        }



    }
}
