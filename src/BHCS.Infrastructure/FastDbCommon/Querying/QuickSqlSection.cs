using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using BHCS.Infrastructure.FastDbCommon.Persistents.Attributes;
using BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying
{
    public class QuickSqlSection
    {
        private readonly IInterpreterExecutor executor;
        private readonly IInterpreterProvider provider;

        public QuickSqlSection(IInterpreterExecutor executor, IInterpreterProvider provider)
        {
            this.executor = executor;
            this.provider = provider;
        }

        public object ToResult(string strsql)
        {
            DbCommand command = this.provider.DbProviderFactory.CreateCommand();
            command.CommandText = strsql;

            return this.executor.ExecuteScalar(command);
        }

        public T ToFirstOrDefault<T>(string strsql)where T:class
        {
            T t = null;
            using (IDataReader reader = ToDataReader(strsql))
            {
                var result = EntityUtils.ReaderToEnumerable<T>(reader).ToArray();
                if (result.Any())
                {
                    t = result.First();
                }
            }

            if (t == null)
                t = DbUtils.Create<T>();
            return t;
        }

        public List<T> ToList<T>(string strsql)
        {
            List<T> list;
            using (var reader = ToDataReader(strsql))
            {
                list = EntityUtils.ReaderToEnumerable<T>(reader).ToList();
            }

            return list;
        }

        #region QueryBuilder
        public object Find(QueryBuilder queryBuilder)
        {
            if (queryBuilder == null || queryBuilder.TableName == null || queryBuilder.TableName.Count <= 0)
            {
                throw new InterpreterException("调用此方法，必须显示声明表名！");
            }

            return QueryEnvironment.Current.GetQuickSqlSection().ToResult(this.provider.InterpreterQueryBuilder(queryBuilder));
        }

        public T Find<T>(QueryBuilder queryBuilder) where T : class
        {
            queryBuilder = this.CheckTableNameAndAddDefaultTableName<T>(queryBuilder);

            return QueryEnvironment.Current.GetQuickSqlSection().ToFirstOrDefault<T>(this.provider.InterpreterQueryBuilder(queryBuilder));
        }

        public IEnumerable<T> FindAll<T>(QueryBuilder queryBuilder) where T : class
        {
            queryBuilder = this.CheckTableNameAndAddDefaultTableName<T>(queryBuilder);

            return QueryEnvironment.Current.GetQuickSqlSection().ToList<T>(this.provider.InterpreterQueryBuilder(queryBuilder));
        }

        public IPaged<T> ToPage<T>(QueryBuilder queryBuilder)where T:class
        {
            if (queryBuilder.PageSize <= 0 || queryBuilder.PageNumber <= 0)
            {
                throw new InterpreterException("查询分页信息，请传入PageSize和PageNumber！");
            }

            queryBuilder = this.CheckTableNameAndAddDefaultTableName<T>(queryBuilder);

            QuickSqlSection section = QueryEnvironment.Current.GetQuickSqlSection();

            var data = section.ToList<T>(this.provider.InterpreterQueryBuilder(queryBuilder));
            QueryBuilder queryBuilderPageCount = new QueryBuilder();
            var counts = Convert.ToInt32(section.ToResult(this.provider.InterpreterQueryBuilder(queryBuilderPageCount.AddCount("0").FromTable(queryBuilder.TableName.ToArray()).AddWhere(queryBuilder.Where))));

            int pageCounts = 0;
            if (counts % queryBuilder.PageSize == 0)
                pageCounts = counts / queryBuilder.PageSize;
            else
                pageCounts = (counts / queryBuilder.PageSize) + 1;

            return new Paged<T>(pageCounts, queryBuilder.PageNumber, counts, queryBuilder.PageSize, data == null ? new List<T>() : data.ToList());
        }
#endregion

        private IDataReader ToDataReader(string strsql)
        {
            DbCommand command = this.provider.DbProviderFactory.CreateCommand();
            command.CommandText = strsql;

            return executor.ExecuteDataReader(command);
        }

        private QueryBuilder CheckTableNameAndAddDefaultTableName<T>(QueryBuilder queryBuilder) where T : class
        {
            if (queryBuilder.TableName == null || queryBuilder.TableName.Count <= 0)
            {
                TableAttribute table = typeof(T).GetTypeInfo().GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
                if (table == null)
                {
                    queryBuilder.FromTable(typeof(T).Name);
                }
                else
                {
                    queryBuilder.FromTable(table.Table);
                }
            }

            return queryBuilder;
        }
    }
}
