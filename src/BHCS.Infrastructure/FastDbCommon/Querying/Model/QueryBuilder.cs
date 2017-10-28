using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Model
{
    public class QueryBuilder
    {
        private IList<string> selectColumns;
        private IList<string> tableName;
        private string where;
        private IList<string> innerJoin;
        private IList<string> orderByAsc;
        private IList<string> orderByDesc;
        private int startLimit;
        private int endLimit;
        private IList<string> countlist;
        private int pageSize;
        private int pageNumber;
        private string pageAddColumns;

        public QueryBuilder()
        {
            this.selectColumns = new List<string>();
            this.tableName = new List<string>();
            this.where =string.Empty;
            this.innerJoin = new List<string>();
            this.orderByAsc = new List<string>();
            this.orderByDesc = new List<string>();
            this.startLimit = -1;
            this.endLimit = -1;
            this.countlist = new List<string>();
            this.pageSize = -1;
            this.pageNumber = -1;
        }

        public IList<string> SelectColumns { get { return this.selectColumns; } }

        public IList<string> TableName { get { return this.tableName; } }

        public string Where { get { return this.where; } }

        public IList<string> InnerJoin { get { return this.innerJoin; } }

        public IList<string> OrderByAsc { get { return this.orderByAsc; } }

        public IList<string> OrderByDesc { get { return this.orderByDesc; } }

        public int StartLimit { get { return this.startLimit; } }

        public int EndLimit { get { return this.endLimit; } }

        public IList<string> CountList { get { return this.countlist; } }

        public int PageSize { get { return this.pageSize; } }

        public int PageNumber { get { return this.pageNumber; } }

        public string PageAddColumns { get { return this.pageAddColumns; } }

        public QueryBuilder FromTable(params string[] tableNames)
        {
            this.tableName = this.AddRange(this.tableName, tableNames);

            return this;
        }

        public QueryBuilder ClearTable()
        {
            this.tableName.Clear();

            return this;
        }

        public QueryBuilder AddSelectColumn(string selectColumns)
        {
            if (String.IsNullOrWhiteSpace(selectColumns))
            {
                return this;
            }

            this.selectColumns.Add(selectColumns);

            return this;
        }

        public QueryBuilder AddWhere(string wheres)
        {
            this.where = wheres;

           return this;
        }

        public QueryBuilder AddInnerJoin(string tableName)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                return this;
            }

            this.innerJoin.Add(tableName);

            return this;
        }

        public QueryBuilder AddOrderByAsc(string columnName)
        {
            if (String.IsNullOrWhiteSpace(columnName))
            {
                return this;
            }

            this.orderByAsc.Add(columnName);

            return this;
        }

        public QueryBuilder AddOrderByDesc(string columnName)
        {
            if (String.IsNullOrWhiteSpace(columnName))
            {
                return this;
            }

            this.orderByDesc.Add(columnName);

            return this;
        }

        public QueryBuilder AddLimit(int startLimit, int endLimit = -1)
        {
            this.startLimit = startLimit;
            this.endLimit = endLimit;

            return this;
        }

        public QueryBuilder AddCount(string countColumns)
        {
            this.countlist.Add(countColumns);

            return this;
        }

        public QueryBuilder AddPageInfo(int pageSize,int pageNumber)
        {
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;

            return this;
        }

        public QueryBuilder AddPageColumns(string pageColumns)
        {
            this.pageAddColumns = pageColumns;
            return this;
        }

        public override string ToString()
        {
            throw new NotImplementedException("该类不允许执行ToString方法！");
        }

        private IList<string> AddRange(IList<string> list, string[] addDatas)
        {
            if (addDatas == null || addDatas.Length <= 0)
            {
                return list;
            }

            foreach (String item in addDatas)
            {
                if (String.IsNullOrWhiteSpace(item))
                {
                    continue;
                }

                list.Add(item);
            }

            return list;
        }

    }
}
