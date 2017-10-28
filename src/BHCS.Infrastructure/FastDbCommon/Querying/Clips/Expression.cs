using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System.Collections.Generic;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Clips
{
    public class Expression
    {/// <summary>
     /// 
     /// </summary>
        protected string expressionString = string.Empty;


        /// <summary>
        /// 参数
        /// </summary>
        protected List<Parameter> parameters = new List<Parameter>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public Expression()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expressionString"></param>
        public Expression(string expressionString)
        {
            this.expressionString = expressionString;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expressionString"></param>
        /// <param name="parameters"></param>
        public Expression(string expressionString, params Parameter[] parameters)
        {
            if (!string.IsNullOrEmpty(expressionString))
            {
                this.expressionString = expressionString;

                if (null != parameters && parameters.Length > 0)
                    this.parameters.AddRange(parameters);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="oper"></param>
        public Expression(Field field, object value, QueryOperator oper)
            : this(field, value, oper, true)
        {

        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="oper"></param>
        /// <param name="isFieldBefore"></param>
        public Expression(Field field, object value, QueryOperator oper, bool isFieldBefore)
        {
            if (!Field.IsNullOrEmpty(field))
            {
                string valuestring = null;
                if (value is Expression)
                {
                    Expression expression = (Expression)value;
                    valuestring = expression.ToString();
                    parameters.AddRange(expression.Parameters);
                }
                else if (value is Field)
                {
                    Field fieldValue = (Field)value;
                    valuestring = fieldValue.TableFieldName;
                }
                else
                {
                    valuestring = DbUtils.MakeUniqueKey(field);
                    //valuestring = field.tableName + field.Name;
                    var p = new Parameter(valuestring, value, field.ParameterDbType, field.ParameterSize);
                    parameters.Add(p);
                }

                if (isFieldBefore)
                {
                    this.expressionString = string.Concat(field.TableFieldName, DbUtils.ToString(oper), valuestring);
                }
                else
                {
                    this.expressionString = string.Concat(valuestring, DbUtils.ToString(oper), field.TableFieldName);
                }
            }
        }

        /// <summary>
        /// 返回参数
        /// </summary>
        internal List<Parameter> Parameters
        {
            get
            {
                return parameters;
            }
        }


        /// <summary>
        /// 返回组合字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return expressionString;
        }
    }
}
