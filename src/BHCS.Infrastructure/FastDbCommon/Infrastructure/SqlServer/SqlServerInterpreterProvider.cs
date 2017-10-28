using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHCS.Infrastructure.FastDbCommon.Querying.Clips;
using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System.Data.SqlClient;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.SqlServer
{
    public class SqlServerInterpreterProvider : InterpreterProvider
    {
        private bool isPageSql = false;
        private WhereClip whereClip;
        private string pageWhere;

        public SqlServerInterpreterProvider()
            : base(DatabaseType.SqlServer, SqlClientFactory.Instance, '[', ']', ' ')
        {
        }

        public override FromSection CreatePageFromSection(FromSection fromSection, int startIndex, int endIndex)
        {        
            int pageSize = endIndex - startIndex + 1;
            if (startIndex == 1)
            {
                fromSection.PrefixString = string.Concat(" TOP ", pageSize.ToString());
            }
            else
            {
                this.isPageSql = true;
                OrderByClip temp_orderByClip = fromSection.OrderByClip;

                fromSection.PrefixString = string.Format(" ROW_NUMBER() OVER ({0}) AS ROW, ", temp_orderByClip.OrderByString);
                fromSection.OrderByClip = null;
                fromSection.TableName = string.Concat(" (", fromSection.SqlString, ") AS Paged ");

                fromSection.OrderByClip = temp_orderByClip;
                fromSection.PrefixString = string.Empty;
                fromSection.DistinctString = string.Empty;
                fromSection.GroupBy(GroupByClip.None);
                fromSection.ClearSelect();
                fromSection.Select(Field.All);
                fromSection.OrderBy(fromSection.OrderByClip);
                fromSection.Where(string.Format("Row > {0} AND Row <={1}", startIndex-1, endIndex));

                pageWhere = string.Format("Row > {0} AND Row <={1}", startIndex - 1, endIndex);
                 whereClip=fromSection.GetWhereClip();
            }

            return fromSection;// base.CreatePageFromSection(fromSection, startIndex, endIndex);
        }

        public override void PrepareCommand(DbCommand cmd)
        {
            if (isPageSql)
            {
                cmd.CommandText = cmd.CommandText.Replace(whereClip.WhereString, "where " + pageWhere);
                isPageSql = false;
            }

            base.PrepareCommand(cmd);
        }

        protected override string DoInterpreter(QueryBuilder queryBuilder)
        {
            if (queryBuilder.PageNumber > 0 && queryBuilder.PageSize > 0)
            {
                return GetPageSql(queryBuilder);
            }

            //{0}  需要插入的Top信息   
            //{1}  需要插入的查询列信息
            //{2}  需要插入的表信息
            //{3}  需要插入的表连接信息
            //{4}  需要插入的条件信息
            //{5}  需要插入的排序信息
            return string.Format("select {0} {1} {2} {3} from {4} {5} {6} {7}", "",
                BuildLimit(queryBuilder), BuildCount(queryBuilder), BuildColumns(queryBuilder), BuildTable(queryBuilder), "", BuildWhere(queryBuilder), BuildOrder(queryBuilder));

        }

        protected string GetTopInfo(QueryBuilder queryBuilder)
        {
            return null;
        }

        protected string GetPageSql(QueryBuilder queryBuilder)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("select * FROM (SELECT ROW_NUMBER() OVER ({0}) AS Row, {1} FROM {2}  {3} ) AS Paged  WHERE {4}",
                BuildOrder(queryBuilder), BuildColumns(queryBuilder), BuildTable(queryBuilder), BuildWhere(queryBuilder), BuildPaged(queryBuilder));

            if (string.IsNullOrWhiteSpace(queryBuilder.PageAddColumns))
            {
                return sqlBuilder.ToString();
            }

            string sql = sqlBuilder.ToString();
            sql = sql.Insert(sql.IndexOf("select") + 6, " " + queryBuilder.PageAddColumns + ",");

            return sql;
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
                return string.Format("where {0}", queryBuilder.Where);
            }
        }

        protected string BuildPaged(QueryBuilder QueryBuilder)
        {
            int rowStart = (QueryBuilder.PageNumber - 1) * QueryBuilder.PageSize;
            int rowEnd = QueryBuilder.PageNumber * QueryBuilder.PageSize;

            return string.Format("Row > {0} AND Row <={1}", rowStart, rowEnd);
        }

        protected string BuildCount(QueryBuilder queryBuilder)
        {
            if (queryBuilder.CountList == null || queryBuilder.CountList.Count <= 0)
            {
                return "";
            }

            StringBuilder stringBuilder = new StringBuilder("");
            foreach (String count in queryBuilder.CountList)
            {
                if (count.Equals("*"))
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
            if (queryBuilder.EndLimit <= 0)
            {
                return "";
            }

            return "top " + queryBuilder.EndLimit + " ";
        }
    }
}
