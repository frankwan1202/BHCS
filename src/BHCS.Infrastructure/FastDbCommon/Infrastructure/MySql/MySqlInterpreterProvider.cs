using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using System.Data.Common;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using MySql.Data.MySqlClient;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Infrastructure.FastDbCommon.Querying.Clips;
using System.Text;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.MySql
{
    public class MySqlInterpreterProvider : InterpreterProvider
    {
        private WhereClip whereClip;
        private string pageWhere;

        public MySqlInterpreterProvider()
            : base(DatabaseType.MySql, MySqlClientFactory.Instance, '`', '`', ' ')
        {
        }

        public override FromSection CreatePageFromSection(FromSection fromSection, int startIndex, int endIndex)
        {
            int pageSize = endIndex - startIndex + 1;

            fromSection.LimitString=$" Limit {(startIndex - 1)},{pageSize}" ;

            return fromSection;// base.CreatePageFromSection(fromSection, startIndex, endIndex);
        }

        protected override string DoInterpreter(QueryBuilder queryBuilder)
        {
            //if (queryBuilder.PageNumber > 0 && queryBuilder.PageSize > 0)
            //{
            //    return GetPageSql(queryBuilder);
            //}

            return $"select {BuildCount(queryBuilder)} {BuildColumns(queryBuilder)} from {BuildTable(queryBuilder)} {BuildWhere(queryBuilder)} {BuildOrder(queryBuilder)} {BuildLimit(queryBuilder)}";
        }

        protected string BuildOrder(QueryBuilder queryBuilder)
        {
            StringBuilder stringBuilder = new StringBuilder("Order By ");
            if (queryBuilder.OrderByAsc.Count <= 0 && queryBuilder.OrderByDesc.Count <= 0)
            {
                return "";
            }

            if (queryBuilder.OrderByAsc != null && queryBuilder.OrderByAsc.Count > 0)
            {
                foreach (string orderby in queryBuilder.OrderByAsc)
                {
                    stringBuilder.AppendFormat("{0} asc,", orderby);
                }
            }
            if (queryBuilder.OrderByDesc != null && queryBuilder.OrderByDesc.Count > 0)
            {
                foreach (string orderby in queryBuilder.OrderByDesc)
                {
                    stringBuilder.AppendFormat("{0} desc,", orderby);
                }
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
        }

        protected string BuildColumns(QueryBuilder queryBuilder)
        {
            if (queryBuilder.SelectColumns == null || queryBuilder.SelectColumns.Count <= 0)
            {
                if (string.IsNullOrWhiteSpace(BuildCount(queryBuilder)))
                {
                    return "*";
                }
                else
                {
                    return "";
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string column in queryBuilder.SelectColumns)
            {
                stringBuilder.AppendFormat("{0},", column);
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
        }

        protected string BuildTable(QueryBuilder queryBuilder)
        {
            if (queryBuilder.TableName == null || queryBuilder.TableName.Count <= 0)
            {
                throw new QueryInterpreterException("没有设置Table 无法生成语句！");
            }

            if (queryBuilder.TableName[0].ToLower().Contains("select")) return $"({queryBuilder.TableName[0]}) InnerTable";

            StringBuilder stringBuilder = new StringBuilder();
            foreach (String tableItem in queryBuilder.TableName)
            {
                stringBuilder.AppendFormat("{0},", tableItem);
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
        }

        protected string BuildWhere(QueryBuilder queryBuilder)
        {
            if (string.IsNullOrWhiteSpace(queryBuilder.Where))
            {
                return queryBuilder.Where;
            }
            else
            {
                return $"where {queryBuilder.Where}";
            }
        }
        
        protected string BuildCount(QueryBuilder queryBuilder)
        {
            if (queryBuilder.CountList == null || queryBuilder.CountList.Count <= 0)
            {
                return "";
            }

            if (queryBuilder.CountList.Count == 1) return $"count({queryBuilder.CountList[0]})"; 

            StringBuilder stringBuilder = new StringBuilder("");
            foreach (String count in queryBuilder.CountList)
            {
                if (count.Equals("*")||count.Equals("0"))
                {
                    stringBuilder.AppendFormat("count({0}),", count);
                }
                else
                {
                    stringBuilder.AppendFormat("count({0}) as {1},", count, count);
                }
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
        }

        protected string BuildLimit(QueryBuilder queryBuilder)
        {
            if (queryBuilder.PageNumber <= 0 || queryBuilder.PageSize <= 0)
            {
                return string.Empty;
            }

            int startIndex = (queryBuilder.PageNumber-1) * queryBuilder.PageSize;

            return $" limit {startIndex},{queryBuilder.PageSize}";
        }

        protected string GetPageSql(QueryBuilder queryBuilder)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("select {0} FROM ({1}) AS Paged  {2} {3} {4} ",
                BuildColumns(queryBuilder), BuildTable(queryBuilder), BuildWhere(queryBuilder), BuildOrder(queryBuilder), BuildLimit(queryBuilder));

            return sqlBuilder.ToString();
        }
    }
}
