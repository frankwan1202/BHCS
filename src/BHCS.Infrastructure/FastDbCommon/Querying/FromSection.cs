using BHCS.Infrastructure.FastDbCommon.Querying.Clips;
using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Expressions;
using BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Reflection;
using BHCS.Infrastructure.FastDbCommon.Infrastructure;

namespace BHCS.Infrastructure.FastDbCommon.Querying
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public class FromSection<T>:FromSection 
        where T : class
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="database"></param>
        /// <param name="trans"></param>
        public FromSection(IInterpreterProvider interpreterProvider, IInterpreterExecutor executor)
            : base(interpreterProvider, executor, interpreterProvider.BuildTableName(EntityCache.GetTableName<T>(), EntityCache.GetUserName<T>()), "")
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="database"></param>
        /// <param name="trans"></param>
        /// <param name="asName"></param>
        public FromSection(IInterpreterProvider interpreterProvider,IInterpreterExecutor executor, string asName)
            : base(interpreterProvider, executor, interpreterProvider.BuildTableName(EntityCache.GetTableName<T>(), EntityCache.GetUserName<T>()), asName)
        {

        }

        #region 连接  Join

        /// <summary>
        /// Inner Join。Lambda写法：.InnerJoin&lt;Model2>((a, b) => a.ID == b.ID)
        /// </summary>
        public FromSection<T> InnerJoin<TEntity>(WhereClip where, string asName = "", string asName2 = "")
             where TEntity : class
        {
            return Join(EntityCache.GetTableName<TEntity>(), EntityCache.GetUserName<TEntity>(), where, JoinType.InnerJoin);
        }

        /// <summary>
        /// Inner Join。Lambda写法：.InnerJoin&lt;Model2>((a, b) => a.ID == b.ID)
        /// </summary>
        public FromSection<T> InnerJoin<TEntity>(Expression<Func<T, TEntity, bool>> lambdaWhere, string asName = "")
             where TEntity : class
        {
            //this.asNames.Add(EntityCache.GetTableName<TEntity>() + "|" +asName);
            //this.asNames.Add(EntityCache.GetTableName<TEntity>() + "|" + asName2);
            return Join(EntityCache.GetTableName<TEntity>(), EntityCache.GetUserName<TEntity>(), ExpressionToClip<T>.ToJoinWhere(lambdaWhere), JoinType.InnerJoin);//EntityCache.GetTableName<TEntity>() + "|" + asName
        }
        /// <summary>
        /// Cross Join
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection<T> CrossJoin<TEntity>(WhereClip where)
            where TEntity : class
        {
            return Join(EntityCache.GetTableName<TEntity>(), EntityCache.GetUserName<TEntity>(), where, JoinType.CrossJoin);
        }

        /// <summary>
        /// Right Join
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection<T> RightJoin<TEntity>(WhereClip where)
            where TEntity : class
        {
            return Join(EntityCache.GetTableName<TEntity>(), EntityCache.GetUserName<TEntity>(), where, JoinType.RightJoin);
        }

        /// <summary>
        /// Left Join。经典写法：Model1._.ID == Model2._.ID
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection<T> LeftJoin<TEntity>(WhereClip where)
             where TEntity : class
        {
            return Join(EntityCache.GetTableName<TEntity>(), EntityCache.GetUserName<TEntity>(), where, JoinType.LeftJoin);
        }

        /// <summary>
        /// Left Join。Lambda写法：.LeftJoin&lt;Model2>((d1,d2) => d1.ID == d2.ID)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="lambdaWhere"></param>
        /// <returns></returns>
        public FromSection<T> LeftJoin<TEntity>(Expression<Func<T, TEntity, bool>> lambdaWhere)
             where TEntity : class
        {
            return Join(EntityCache.GetTableName<TEntity>(), EntityCache.GetUserName<TEntity>(), ExpressionToClip<T>.ToJoinWhere(lambdaWhere), JoinType.LeftJoin);
        }

        /// <summary>
        /// Full Join
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection<T> FullJoin<TEntity>(WhereClip where)
            where TEntity : class
        {
            return Join(EntityCache.GetTableName<TEntity>(), EntityCache.GetUserName<TEntity>(), where, JoinType.FullJoin);
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userName"></param>
        /// <param name="where"></param>
        /// <param name="joinType"></param>
        /// <returns></returns>
        private FromSection<T> Join(string tableName, string userName, WhereClip where, JoinType joinType)
        {
            return (FromSection<T>)base.join(tableName, userName, where, joinType);

            //if (string.IsNullOrEmpty(tableName) || WhereClip.IsNullOrEmpty(where))
            //    return this;

            //tableName = dbProvider.BuildTableName(tableName);

            //if (!joins.ContainsKey(tableName))
            //{
            //    string joinString = string.Empty;
            //    switch (joinType)
            //    {
            //        case JoinType.InnerJoin:
            //            joinString = "INNER JOIN";
            //            break;
            //        case JoinType.LeftJoin:
            //            joinString = "LEFT OUTER JOIN";
            //            break;
            //        case JoinType.RightJoin:
            //            joinString = "RIGHT OUTER JOIN";
            //            break;
            //        case JoinType.CrossJoin:
            //            joinString = "CROSS JOIN";
            //            break;
            //        case JoinType.FullJoin:
            //            joinString = "FULL OUTER JOIN";
            //            break;
            //        default:
            //            joinString = "INNER JOIN";
            //            break;
            //    }

            //    joins.Add(tableName, new KeyValuePair<string, WhereClip>(joinString, where));
            //}

            //return this;
        }

        #endregion

        #region 私有方法


        /// <summary>
        ///  设置默认主键排序 
        /// </summary>
        private void setPrimarykeyOrderby()
        {

            Field[] primarykeys = EntityCache.GetPrimaryKeyFields<T>();

            OrderByClip temporderBy;

            if (null != primarykeys && primarykeys.Length > 0)
            {
                temporderBy = new OrderByClip(primarykeys[0]);
            }
            else
            {
                temporderBy = new OrderByClip(EntityCache.GetFirstField<T>());
            }

            OrderBy(temporderBy);
        }

        #endregion

        #region 操作


        /// <summary>
        /// Having 
        /// </summary>
        public new FromSection<T> Having(WhereClip havingWhere)
        {
            return (FromSection<T>)base.Having(havingWhere);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection<T> Having(Where where)
        {
            return (FromSection<T>)base.Having(where.ToWhereClip());
        }

        public FromSection<T> Having(Expression<Func<T, bool>> lambdaHaving)
        {
            return (FromSection<T>)base.Having(ExpressionToClip<T>.ToWhereClip(lambdaHaving));
        }

        /// <summary>
        /// whereclip
        /// </summary>
        public new FromSection<T> Where(WhereClip where)
        {
            return (FromSection<T>)base.Where(where);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereParam"></param>
        /// <returns></returns>
        public FromSection<T> Where(Where<T> whereParam)
        {
            return (FromSection<T>)base.Where(whereParam.ToWhereClip());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereParam"></param>
        /// <returns></returns>
        public FromSection<T> Where(Where whereParam)
        {
            return (FromSection<T>)base.Where(whereParam.ToWhereClip());
        }

        /// <summary>
        /// 
        /// </summary>
        public FromSection<T> Where(Expression<Func<T, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="lambdaWhere"></param>
        /// <returns></returns>
        public FromSection<T> Where<T2>(Expression<Func<T, T2, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="lambdaWhere"></param>
        /// <returns></returns>
        public FromSection<T> Where<T2, T3>(Expression<Func<T, T2, T3, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="lambdaWhere"></param>
        /// <returns></returns>
        public FromSection<T> Where<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }

        /// <summary>
        /// 
        /// </summary>
        public FromSection<T> Where<T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="lambdaWhere"></param>
        /// <returns></returns>
        public FromSection<T> Where<T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaWhere"></param>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public FromSection<T> Select<T2>(Expression<Func<T, T2, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaWhere"></param>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public FromSection<T> Select<T2, T3>(Expression<Func<T, T2, T3, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaWhere"></param>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        public FromSection<T> Select<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }
        public FromSection<T> Select<T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaWhere"></param>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <returns></returns>
        public FromSection<T> Select<T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, bool>> lambdaWhere)
        {
            return Where(ExpressionToClip<T>.ToWhereClip(lambdaWhere));
        }
        /// <summary>
        /// groupby
        /// </summary>
        public new FromSection<T> GroupBy(GroupByClip groupBy)
        {
            return (FromSection<T>)base.GroupBy(groupBy);
        }
        /// <summary>
        /// groupby
        /// </summary>
        public new FromSection<T> GroupBy(params Field[] fields)
        {
            return (FromSection<T>)base.GroupBy(fields);
        }
        public new FromSection<T> GroupBy(Expression<Func<T, object>> lambdaGroupBy)
        {
            return (FromSection<T>)base.GroupBy(ExpressionToClip<T>.ToGroupByClip(lambdaGroupBy));
        }
        #region 2015-09-08新增
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public FromSection<T> OrderBy(params Field[] f)
        {
            var gb = f.Aggregate(OrderByClip.None, (current, field) => current && field.Asc);
            return (FromSection<T>)base.OrderBy(gb);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public FromSection<T> OrderByDescending(params Field[] f)
        {
            var gb = f.Aggregate(OrderByClip.None, (current, field) => current && field.Desc);
            return (FromSection<T>)base.OrderBy(gb);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public new FromSection<T> OrderBy(OrderByClip orderBy)
        {
            return (FromSection<T>)base.OrderBy(orderBy);
        }
        /// <summary>
        /// orderby
        /// </summary>
        public new FromSection<T> OrderBy(Expression<Func<T, object>> lambdaOrderBy)
        {
            return (FromSection<T>)base.OrderBy(ExpressionToClip<T>.ToOrderByClip(lambdaOrderBy));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaOrderBy"></param>
        /// <returns></returns>
        public FromSection<T> OrderByDescending(Expression<Func<T, object>> lambdaOrderBy)
        {
            return (FromSection<T>)base.OrderBy(ExpressionToClip<T>.ToOrderByDescendingClip(lambdaOrderBy));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public new FromSection<T> OrderBy(params OrderByClip[] orderBys)
        {
            return (FromSection<T>)base.OrderBy(orderBys);
        }
        /// <summary>
        /// select field
        /// </summary>
        public new FromSection<T> Select(params Field[] fields)
        {
            return (FromSection<T>)base.Select(fields);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaSelect"></param>
        /// <returns></returns>
        public FromSection<T> Select(Expression<Func<T, object>> lambdaSelect)
        {
            return (FromSection<T>)base.Select(ExpressionToClip<T>.ToSelect(lambdaSelect));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="lambdaSelect"></param>
        /// <returns></returns>
        public FromSection<T> Select<T2>(Expression<Func<T, T2, object>> lambdaSelect)
        {
            return (FromSection<T>)base.Select(ExpressionToClip<T>.ToSelect(lambdaSelect));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="lambdaSelect"></param>
        /// <returns></returns>
        public FromSection<T> Select<T2, T3>(Expression<Func<T, T2, T3, object>> lambdaSelect)
        {
            return (FromSection<T>)base.Select(ExpressionToClip<T>.ToSelect(lambdaSelect));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="lambdaSelect"></param>
        /// <returns></returns>
        public FromSection<T> Select<T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> lambdaSelect)
        {
            return (FromSection<T>)base.Select(ExpressionToClip<T>.ToSelect(lambdaSelect));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="lambdaSelect"></param>
        /// <returns></returns>
        public FromSection<T> Select<T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object>> lambdaSelect)
        {
            return (FromSection<T>)base.Select(ExpressionToClip<T>.ToSelect(lambdaSelect));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="lambdaSelect"></param>
        /// <returns></returns>
        public FromSection<T> Select<T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, object>> lambdaSelect)
        {
            return (FromSection<T>)base.Select(ExpressionToClip<T>.ToSelect(lambdaSelect));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaSelect"></param>
        /// <returns></returns>
        public FromSection<T> Select(Expression<Func<T, bool>> lambdaSelect)
        {
            return (FromSection<T>)base.Select(ExpressionToClip<T>.ToSelect(lambdaSelect));
        }
        /// <summary>
        /// Distinct
        /// </summary>
        /// <returns></returns>
        public new FromSection<T> Distinct()
        {
            return (FromSection<T>)base.Distinct();
        }

        /// <summary>
        /// Top
        /// </summary>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public new FromSection<T> Top(int topCount)
        {
            return From(1, topCount);
        }


        /// <summary>
        /// Page
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public new FromSection<T> Page(int pageSize, int pageIndex)
        {
            return From(pageSize * (pageIndex - 1) + 1, pageIndex * pageSize);
        }


        /// <summary>
        /// 设置默认排序
        /// </summary>
        private void SetDefaultOrderby()
        {
            if (!OrderByClip.IsNullOrEmpty(this.OrderByClip)) return;
            if (fields.Count > 0)
            {
                if (fields.Any(f => f.PropertyName.Trim().Equals("*")))
                {
                    setPrimarykeyOrderby();
                }

            }
            else
            {
                setPrimarykeyOrderby();
            }
        }

        /// <summary>
        /// From  1-10
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public new FromSection<T> From(int startIndex, int endIndex)
        {
            if (startIndex > 1)
            {
                SetDefaultOrderby();
            }
            return (FromSection<T>)base.From(startIndex, endIndex);
            //return (FromSection<T>)dbProvider.CreatePageFromSection(this, startIndex, endIndex);
        }

        /// <summary>
        /// 重新加载
        /// </summary>
        /// <returns></returns>
        public new FromSection<T> Refresh()
        {
            isRefresh = true;
            return this;
        }


        /// <summary>
        /// select sql
        /// </summary>
        /// <param name="fromSection"></param>
        /// <returns></returns>
        public new FromSection<T> AddSelect(FromSection fromSection)
        {
            return AddSelect(fromSection, null);
        }

        /// <summary>
        /// select sql
        /// </summary>
        /// <param name="fromSection"></param>
        /// <param name="aliasName">别名</param>
        /// <returns></returns>
        public new FromSection<T> AddSelect(FromSection fromSection, string aliasName)
        {
            return (FromSection<T>)base.AddSelect(fromSection, aliasName);
        }

        #endregion

        #region 查询
        private readonly string[] _notClass = new string[] { "String" };
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public List<TResult> ToList<TResult>()
        {
            var typet = typeof(TResult);
            if (typet == typeof(T))
            {
                return ToList() as List<TResult>;
            }
            var from = GetPagedFromSection();
            if (typet.GetTypeInfo().IsClass && !_notClass.Contains(typet.Name))
            {
                List<TResult> list;
                using (var reader = ToDataReader(from))
                {
                    list = EntityUtils.ReaderToEnumerable<TResult>(reader).ToList();
                    reader.Close();
                }
                
                return list;
            }
            if (!@from.Fields.Any())
            {
                throw new Exception(".ToList<" + typet.Name + ">()至少需要.Select()一个字段！");
            }
            if (@from.Fields.Count > 1)
            {
                throw new Exception(".ToList<" + typet.Name + ">()最多.Select()一个字段！");
            }
            {
                var list = new List<TResult>();
                using (var reader = ToDataReader(@from))
                {
                    while (reader.Read())
                    {
                        list.Add(DbUtils.ConvertValue<TResult>(reader[@from.Fields[0].Name]));
                    }
                    reader.Close();
                }

                return list;
            }
        }

        /// <summary>
        /// To List&lt;T>
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            var from = GetPagedFromSection();
           
            List<T> list;
            using (var reader = ToDataReader(from))
            {
                list = EntityUtils.ReaderToEnumerable<T>(reader).ToList();
            }
            
            return list;
        }

        /// <summary>
        /// 返回懒加载数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> ToEnumerable()
        {
            var from = GetPagedFromSection();
            using (var reader = ToDataReader(from))
            {
                var info = new EntityUtils.CacheInfo
                {
                    Deserializer = EntityUtils.GetDeserializer(typeof(T), reader, 0, -1, false)
                };
                while (reader.Read())
                {
                    dynamic next = info.Deserializer(reader);
                    yield return (T)next;
                }
            }
        }

        /// <summary>
        /// 返回第一个实体  如果为null，则默认实例化一个
        /// </summary>
        /// <returns></returns>
        public T ToFirstDefault()
        {
            T t = this.ToFirst();
            if (t == null)
                t = DbUtils.Create<T>();
            return t;
        }

        /// <summary>
        /// 返回第一个实体，同ToFirst()。无数据返回Null。
        /// </summary>
        /// <returns></returns>
        public T First()
        {
            return ToFirst();
        }

        /// <summary>
        /// 返回第一个实体，同ToFirst()。无数据返回Null。
        /// </summary>
        /// <returns></returns>
        public TResult First<TResult>() where TResult : class
        {
            return ToFirst<TResult>();
        }

        /// <summary>
        /// 返回第一个实体 ，同First()。无数据返回Null。
        /// </summary>
        /// <returns></returns>
        public TResult ToFirst<TResult>() where TResult : class
        {
            var typet = typeof(TResult);
            if (typet == typeof(T))
            {
                return ToFirst() as TResult;
            }
            FromSection from = this.Top(1).GetPagedFromSection();
            
            TResult t = null;
            using (IDataReader reader = ToDataReader(from))
            {
                var result = EntityUtils.ReaderToEnumerable<TResult>(reader).ToArray();
                if (result.Any())
                {
                    t = result.First();
                }
            }
            
            return t;
        }

        /// <summary>
        /// 返回第一个实体 ，同First()。无数据返回Null。
        /// </summary>
        /// <returns></returns>
        public T ToFirst()
        {
            FromSection from = this.Top(1).GetPagedFromSection();
            
            T t = null;
            using (IDataReader reader = ToDataReader(from))
            {
                var result = EntityUtils.ReaderToEnumerable<T>(reader).ToArray();
                if (result.Any())
                {
                    t = result.First();
                }
            }

            return t;
        }

        public IPaged<T> ToPage(int pageSize, int pageNumber) 
        {
            return ToPage<T>(pageSize, pageNumber);
        }

        public IPaged<TResult> ToPage<TResult>(int pageSize,int pageNumber) where TResult : class
        {
            int counts = this.Count();
            this.Page(pageSize, pageNumber);
            int pageCounts = 0;
            if (counts % pageSize == 0)
                pageCounts = counts / pageSize;
            else
                pageCounts = (counts / pageSize) + 1;

            var data=this.ToList<TResult>();

            return new Paged<TResult>(pageCounts, pageNumber, counts, pageSize, data == null ? new List<TResult>() : data.ToList());
        }

        #endregion

        #region Union

        /// <summary>
        /// Union
        /// </summary>
        /// <param name="fromSection"></param>
        /// <returns></returns>
        public new FromSection<T> Union(FromSection fromSection)
        {
            StringBuilder tname = new StringBuilder();

            tname.Append("(");

            tname.Append(this.SqlNoneOrderbyString);

            tname.Append(" UNION ");

            tname.Append(fromSection.SqlNoneOrderbyString);

            tname.Append(")");

            tname.Append(" ");

            tname.Append(EntityCache.GetTableName<T>());


            FromSection<T> tmpfromSection = new FromSection<T>(this.InterpreterProvider,this.InterpreterExecutor);
            tmpfromSection.tableName = tname.ToString();

            tmpfromSection.parameters.AddRange(this.Parameters);
            tmpfromSection.parameters.AddRange(fromSection.Parameters);

            return tmpfromSection;
        }

        /// <summary>
        /// Union All
        /// </summary>        
        /// <param name="fromSection"></param>
        /// <returns></returns>
        public new FromSection<T> UnionAll(FromSection fromSection)
        {
            StringBuilder tname = new StringBuilder();

            tname.Append("(");

            tname.Append(this.SqlNoneOrderbyString);

            tname.Append(" UNION ALL ");

            tname.Append(fromSection.SqlNoneOrderbyString);

            tname.Append(")");

            tname.Append(" ");

            tname.Append(EntityCache.GetTableName<T>());

            FromSection<T> tmpfromSection = new FromSection<T>(this.InterpreterProvider,this.InterpreterExecutor);
            tmpfromSection.tableName = tname.ToString();

            tmpfromSection.parameters.AddRange(this.Parameters);
            tmpfromSection.parameters.AddRange(fromSection.Parameters);

            return tmpfromSection;
        }

        #endregion
    }

    public class FromSection
    {
        #region 变量
        /// <summary>
        /// 
        /// </summary>
        protected WhereClip where = WhereClip.All;
        /// <summary>
        /// 
        /// </summary>
        protected WhereClip havingWhere = WhereClip.All;
        /// <summary>
        /// 
        /// </summary>
        protected OrderByClip orderBy = OrderByClip.None;
        /// <summary>
        /// 
        /// </summary>
        protected GroupByClip groupBy = GroupByClip.None;
        /// <summary>
        /// 
        /// </summary>
        protected string tableName;
        ///// <summary>
        ///// 
        ///// </summary>
        protected string asName;
        /// <summary>
        /// 
        /// </summary>
        //public List<string> asNames = new List<string>();//2016-05-10新增
        /// <summary>
        /// 
        /// </summary>
        protected List<Parameter> parameters = new List<Parameter>();
        /// <summary>
        /// 
        /// </summary>
        protected List<Field> fields = new List<Field>();
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, KeyValuePair<string, WhereClip>> joins = new Dictionary<string, KeyValuePair<string, WhereClip>>();
        /// <summary>
        /// 
        /// </summary>
        protected string distinctString;
        /// <summary>
        /// 
        /// </summary>
        protected string prefixString;

        private string limitString;

        /// <summary>
        /// 开始项
        /// </summary>
        protected int startIndex;

        /// <summary>
        /// 结束项
        /// </summary>
        protected int endIndex;

        /// <summary>
        /// 
        /// </summary>
        protected string typeTableName;

        /// <summary>
        /// 是否重新加载
        /// </summary>
        protected bool isRefresh = false;

        /// <summary>
        /// 是否已经执行过分页
        /// </summary>
        protected bool isPageFromSection = false;

        protected IInterpreterProvider interpreterProvider;

        protected IInterpreterExecutor executor;

        /// <summary>
        /// 
        /// </summary>
        protected int Identity { get; set; }
        #endregion

        #region 属性

        /// <summary>
        /// 设置 distinct
        /// </summary>
        internal string DistinctString
        {
            set
            {
                distinctString = value;
            }
        }

        /// <summary>
        /// 前置值如 Top N
        /// </summary>
        internal string PrefixString
        {
            set
            {
                prefixString = value;
            }
        }

        /// <summary>
        /// limit 
        /// </summary>
        public string LimitString
        {
            set
            {
                limitString = value;
            }
        }

        /// <summary>
        /// 记录数sql语句 count
        /// </summary>
        public string CountSqlString
        {
            get
            {
                StringBuilder sql = new StringBuilder();

                if (GroupByClip.IsNullOrEmpty(groupBy) && string.IsNullOrEmpty(distinctString))
                {
                    sql.Append(" SELECT count(*) as r_cnt FROM ");
                    sql.Append(FromString);
                    if (!WhereClip.IsNullOrEmpty(where))
                    {
                        sql.Append(where.WhereString);
                    }
                }
                else
                {

                    sql.Append("SELECT count(*) as r_cnt FROM (");

                    sql.Append(SqlNoneOrderbyString);

                    sql.Append(") tmp__table");
                }

                return sql.ToString();
            }
        }

        /// <summary>
        /// 没有没有排序字段
        /// </summary>
        internal string SqlNoneOrderbyString
        {
            get
            {
                StringBuilder sql = new StringBuilder();

                sql.Append(" SELECT ");
                sql.Append(distinctString);
                sql.Append(" ");
                sql.Append(prefixString);
                sql.Append(" ");
                sql.Append(ColumnsString);
                sql.Append(" FROM ");
                sql.Append(FromString);
                sql.Append(" ");

                if (!WhereClip.IsNullOrEmpty(where))
                {
                    sql.Append(where.WhereString);
                }
                if (!GroupByClip.IsNullOrEmpty(groupBy))
                {
                    sql.Append(GroupByString);
                    if (!WhereClip.IsNullOrEmpty(havingWhere))
                    {
                        sql.Append(" HAVING ");
                        sql.Append(havingWhere.ToString());
                    }
                }
                return sql.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal string FromString
        {
            get
            {
                StringBuilder fromstring = new StringBuilder();


                //处理ACCESS 的多表联合查询
                //if (this.InterpreterProvider is AccessInterpreterProvider)
                //{
                //    fromstring.Append('(', joins.Count);
                //    fromstring.Append(tableName);
                //    foreach (KeyValuePair<string, KeyValuePair<string, WhereClip>> kv in joins)
                //    {
                //        fromstring.Append(" ");
                //        fromstring.Append(kv.Value.Key);
                //        fromstring.Append(" ");
                //        fromstring.Append(kv.Key);
                //        fromstring.Append(" ON ");
                //        fromstring.Append(kv.Value.Value.ToString());
                //        fromstring.Append(")");
                //    }

                //}
                //else
                //{
                    fromstring.Append(tableName);
                    foreach (KeyValuePair<string, KeyValuePair<string, WhereClip>> kv in joins)
                    {
                        fromstring.Append(" ");
                        fromstring.Append(kv.Value.Key);
                        fromstring.Append(" ");
                        fromstring.Append(kv.Key);
                        fromstring.Append(" ON ");
                        fromstring.Append(kv.Value.Value.ToString());
                    }
                //}

                return fromstring.ToString();
            }
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        internal Dictionary<string, KeyValuePair<string, WhereClip>> Joins
        {
            get
            {
                return joins;
            }
            set
            {
                joins = value;
            }
        }

        /// <summary>
        /// 获取 sql语句
        /// </summary>
        public string SqlString
        {
            get
            {
                StringBuilder sql = new StringBuilder();

                sql.Append(" SELECT ");
                sql.Append(distinctString);
                sql.Append(" ");
                sql.Append(prefixString);
                sql.Append(" ");
                sql.Append(ColumnsString);
                sql.Append(" FROM ");
                sql.Append(FromString);
                sql.Append(" ");

                if (!WhereClip.IsNullOrEmpty(where))
                {
                    sql.Append(where.WhereString);
                }
                if (!GroupByClip.IsNullOrEmpty(groupBy))
                {
                    sql.Append(GroupByString);
                    if (!WhereClip.IsNullOrEmpty(havingWhere))
                    {
                        sql.Append(" HAVING ");
                        sql.Append(havingWhere.ToString());
                    }
                }

                sql.Append(OrderByString);
                sql.Append(" ");
                sql.Append(limitString);
                return sql.ToString();
            }
        }

        /// <summary>
        /// 返回  表名
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }
            internal set
            {
                tableName = value;

                this.joins = new Dictionary<string, KeyValuePair<string, WhereClip>>();
            }
        }
        
        /// <summary>
        /// 返回  排序
        /// </summary>
        public OrderByClip OrderByClip
        {
            get
            {
                return orderBy;
            }
            internal set
            {
                orderBy = value;
            }
        }

        /// <summary>
        /// 返回排序字符串  例如：orderby id desc
        /// </summary>
        public string OrderByString
        {
            get
            {
                if (OrderByClip.IsNullOrEmpty(orderBy))
                    return string.Empty;

                if (tableName.IndexOf('(') >= 0 || tableName.IndexOf(')') >= 0 || tableName.IndexOf(" FROM ", StringComparison.OrdinalIgnoreCase) >= 0 || tableName.IndexOf(" AS ", StringComparison.OrdinalIgnoreCase) >= 0)
                    return orderBy.RemovePrefixTableName().OrderByString;
                return orderBy.OrderByString;
            }
        }

        /// <summary>
        /// 返回 分组
        /// </summary>
        public GroupByClip GroupByClip
        {
            get
            {
                return groupBy;
            }
            internal set
            {
                groupBy = value;
            }
        }

        /// <summary>
        /// 返回排序字符串 例如：group by id
        /// </summary>
        public string GroupByString
        {
            get
            {
                if (GroupByClip.IsNullOrEmpty(groupBy))
                    return string.Empty;
                if (tableName.IndexOf('(') >= 0 || tableName.IndexOf(')') >= 0 || tableName.IndexOf(" FROM ", StringComparison.OrdinalIgnoreCase) >= 0 || tableName.IndexOf(" AS ", StringComparison.OrdinalIgnoreCase) >= 0)
                    return groupBy.RemovePrefixTableName().GroupByString;
                return groupBy.GroupByString;
            }
        }

        /// <summary>
        /// 返回 条件
        /// </summary>
        public WhereClip GetWhereClip()
        {
            return where;
        }

        /// <summary>
        /// 返回 参数  (包含where 和 from)
        /// </summary>
        public List<Parameter> Parameters
        {
            get
            {
                List<Parameter> ps = new List<Parameter>();

                if (!WhereClip.IsNullOrEmpty(where))
                    ps.AddRange(where.Parameters);

                //处理groupby的having
                if (!GroupByClip.IsNullOrEmpty(groupBy) && !WhereClip.IsNullOrEmpty(havingWhere))
                    ps.AddRange(havingWhere.Parameters);

                ps.AddRange(parameters);

                return ps;
            }
            internal set
            {
                this.parameters = value;
            }
        }

        /// <summary>
        /// 返回  选择列
        /// </summary>
        public string ColumnsString
        {
            get
            {
                if (fields.Count == 0)
                    return "*";

                StringBuilder columns = new StringBuilder();
                foreach (Field filed in fields)
                {
                    columns.Append(",");
                    columns.Append(filed.FullName);
                }

                return columns.ToString().Substring(1);
            }
        }
        
        /// <summary>
        /// 查询的字段
        /// </summary>
        public List<Field> Fields
        {
            get
            {
                return this.fields;
            }
        }

        protected IInterpreterProvider InterpreterProvider { get { return this.interpreterProvider; } }

        protected IInterpreterExecutor InterpreterExecutor { get { return this.executor; } }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        public FromSection(IInterpreterProvider interpreterProvider,IInterpreterExecutor executor, string tableName)
            : this(interpreterProvider, executor, tableName, "")
        {
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <param name="asName"></param>
        /// <param name="trans"></param>
        public FromSection(IInterpreterProvider interpreterProvider, IInterpreterExecutor executor, string tableName, string asName)
        {
            this.interpreterProvider = interpreterProvider;
            this.executor = executor;
            this.tableName = tableName;
            this.asName = asName;
            //asNames.Add(tableName + "|" +asName);
            this.typeTableName = tableName.Trim(this.interpreterProvider.LeftToken, this.interpreterProvider.RightToken);
        }

        #endregion

        #region 操作
                
        /// <summary>
        /// 重新加载
        /// </summary>
        /// <returns></returns>
        public FromSection Refresh()
        {
            isRefresh = true;
            return this;
        }


        /// <summary>
        /// whereclip
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection Where(WhereClip where)
        {
            //2015-09-08修改
            //this.where = where;
            this.where = this.where && where;
            return this;
        }

        public void ClearWhere()
        {
            this.where = new WhereClip();
        }

        /// <summary>
        /// groupby
        /// </summary>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public FromSection GroupBy(GroupByClip groupBy)
        {
            //2015-09-08修改
            this.groupBy = groupBy;
            //this.groupBy = this.groupBy && groupBy;
            return this;
        }


        /// <summary>
        /// having条件
        /// </summary>
        /// <param name="havingWhere"></param>
        /// <returns></returns>
        public FromSection Having(WhereClip havingWhere)
        {
            //2015-09-08修改
            this.havingWhere = havingWhere;
            //this.havingWhere = this.havingWhere && havingWhere;
            return this;
        }

        /// <summary>
        /// groupby
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public FromSection GroupBy(params Field[] fields)
        {
            if (null == fields || fields.Length <= 0) return this;
            var tempgroupby = fields.Aggregate(GroupByClip.None, (current, f) => current && f.GroupBy);
            //2015-09-08修改
            this.groupBy = tempgroupby;
            //this.groupBy = this.groupBy && tempgroupby;
            return this;
        }

        /// <summary>
        /// orderby
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public FromSection OrderBy(OrderByClip orderBy)
        {
            //2015-09-08修改
            this.orderBy = orderBy;
            //this.orderBy = this.orderBy && orderBy;
            return this;
        }


        /// <summary>
        /// orderby
        /// </summary>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public FromSection OrderBy(params OrderByClip[] orderBys)
        {
            if (null == orderBys || orderBys.Length <= 0) return this;
            var temporderby = orderBys.Aggregate(OrderByClip.None, (current, ob) => current && ob);
            //2015-09-08修改
            this.orderBy = temporderby;
            //this.orderBy = this.orderBy && temporderby;
            return this;
        }


        /// <summary>
        /// select field
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public FromSection Select(params Field[] fields)
        {
            ////2015-09-08取消注释
            //this.fields.Clear();
            return AddSelect(fields);
        }

        public FromSection ClearSelect()
        {
            this.fields.Clear();
            return this;
        }


        /// <summary>
        /// select sql
        /// </summary>
        /// <param name="fromSection"></param>
        /// <returns></returns>
        public FromSection AddSelect(FromSection fromSection)
        {
            return AddSelect(fromSection, null);
        }

        /// <summary>
        /// select sql
        /// </summary>
        /// <param name="fromSection"></param>
        /// <param name="aliasName">别名</param>
        /// <returns></returns>
        public FromSection AddSelect(FromSection fromSection, string aliasName)
        {
            if (null == fromSection)
                return this;
            
            this.fields.Add(new Field(string.Concat("(", fromSection.SqlString, ")")).As(aliasName));

            this.parameters.AddRange(fromSection.Parameters);

            return this;
        }


        /// <summary>
        /// add select field
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        internal FromSection AddSelect(params Field[] fields)
        {
            if (null != fields && fields.Length > 0)
            {
                foreach (Field field in fields)
                {
                    //2015-09-25修改
                    Field f = this.fields.Find(fi => fi.Name.Equals(field.Name) && fi.TableName.Equals(field.TableName));
                    if (Field.IsNullOrEmpty(f))
                        this.fields.Add(field);
                }
            }
            return this;
        }

        /// <summary>
        /// Distinct
        /// </summary>
        /// <returns></returns>
        public FromSection Distinct()
        {
            this.distinctString = " DISTINCT ";
            return this;
        }

        /// <summary>
        /// Top
        /// </summary>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public FromSection Top(int topCount)
        {
            return From(1, topCount);
        }
        
        /// <summary>
        /// Page
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        public FromSection Page(int pageSize, int pageIndex)
        {
            return From(pageSize * (pageIndex - 1) + 1, pageIndex * pageSize);
        }
        
        /// <summary>
        /// From startIndex to endIndex
        /// </summary>
        /// <param name="startIndex">开始记录数</param>
        /// <param name="endIndex">结束记录数</param>
        /// <returns></returns>
        public FromSection From(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;

            isPageFromSection = false;

            return this;
        }
        
        /// <summary>
        /// 格式化sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        protected string formatSql(string sql, FromSection from)
        {
            string tempSql = DbUtils.FormatSQL(sql, from.InterpreterProvider.LeftToken, from.InterpreterProvider.RightToken);
            List<Parameter> listPara = from.Parameters;
            foreach (Parameter p in listPara)
            {
                tempSql = tempSql.Replace(p.ParameterName, p.ParameterValue == null ? string.Empty : p.ParameterValue.ToString());
            }
            return tempSql;
        }

        #endregion

        #region 查询
        
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            FromSection from;

            if (!isPageFromSection)
                from = GetPagedFromSection();
            else
                from = this;

            return Count(GetPagedFromSection());
        }

        /// <summary>
        /// 获取记录数(内部使用)
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        internal int Count(FromSection from)
        {
            return DbUtils.ConvertValue<int>(this.InterpreterExecutor.ExecuteScalar(CreateCommand(from.CountSqlString, from.Parameters.ToArray())));
        }
        
        /// <summary>
        /// To DataSet
        /// </summary>
        /// <returns></returns>
        public DataTable ToDataTable()
        {
            FromSection from = GetPagedFromSection();

            return this.InterpreterExecutor.ExecuteDataTable(CreateCommand(from.SqlString, from.Parameters.ToArray()));
        }

        /// <summary>
        /// 获取分页过的FromSection
        /// </summary>
        /// <returns></returns>
        internal FromSection GetPagedFromSection()
        {
            if (startIndex > 0 && endIndex > 0 && !isPageFromSection)
            {
                isPageFromSection = true;
                return this.InterpreterProvider.CreatePageFromSection(this, startIndex, endIndex);
            }
            return this;
        }


        /// <summary>
        /// 转换命令参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected DbParameter[] ConvertCommandParameter(Parameter[] parameters)
        {
            if (null == parameters || parameters.Length == 0)
                return new DbParameter[0];

            DbParameter[] dbParameters = new DbParameter[parameters.Length];
            for (int i = 0; i < parameters.Length;i++ )
            {
                Parameter p = parameters[i];

                DbParameter dbParameter = this.InterpreterProvider.DbProviderFactory.CreateParameter();
                dbParameter.ParameterName = this.InterpreterProvider.BuildParameterName(p.ParameterName);

                dbParameter.Value = p.ParameterValue;
                if (p.ParameterDbType.HasValue)
                    dbParameter.DbType = p.ParameterDbType.Value;
                if (p.ParameterSize.HasValue)
                    dbParameter.Size = p.ParameterSize.Value;

                dbParameters[i] = dbParameter;
            }

            return dbParameters;
        }

        protected DbCommand CreateCommand(string sqlCommandText,Parameter[] parameters)
        {
            DbCommand command = this.InterpreterProvider.DbProviderFactory.CreateCommand();
            command.CommandText = sqlCommandText;
            command.Parameters.AddRange(this.ConvertCommandParameter(parameters.ToArray()));
            this.InterpreterProvider.PrepareCommand(command);

            return command;
        }

        /// <summary>
        /// To DataReader
        /// </summary>
        /// <returns></returns>
        public IDataReader ToDataReader()
        {
            return ToDataReader(GetPagedFromSection());
        }

        /// <summary>
        ///  To DataReader
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        protected IDataReader ToDataReader(FromSection from)
        {
            return this.InterpreterExecutor.ExecuteDataReader(CreateCommand(@from.SqlString, @from.Parameters.ToArray()));
        }

        /// <summary>
        /// To Scalar
        /// </summary>
        /// <returns></returns>
        public object ToScalar()
        {
            FromSection from = GetPagedFromSection();

            return this.InterpreterExecutor.ExecuteScalar(CreateCommand(from.SqlString,from.Parameters.ToArray()));

        }

        /// <summary>
        /// To Scalar
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public TResult ToScalar<TResult>()
        {
            return DbUtils.ConvertValue<TResult>(this.ToScalar());
        }


        #endregion

        #region 连接 join

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userName"></param>
        /// <param name="where"></param>
        /// <param name="joinType"></param>
        /// <returns></returns>
        protected FromSection join(string tableName, string userName, WhereClip where, JoinType joinType)
        {
            if (string.IsNullOrEmpty(tableName) || WhereClip.IsNullOrEmpty(where))
                return this;

            tableName = this.InterpreterProvider.BuildTableName(tableName, userName);

            if (!joins.ContainsKey(tableName))
            {
                string joinString = string.Empty;
                switch (joinType)
                {
                    case JoinType.InnerJoin:
                        joinString = "INNER JOIN";
                        break;
                    case JoinType.LeftJoin:
                        joinString = "LEFT OUTER JOIN";
                        break;
                    case JoinType.RightJoin:
                        joinString = "RIGHT OUTER JOIN";
                        break;
                    case JoinType.CrossJoin:
                        joinString = "CROSS JOIN";
                        break;
                    case JoinType.FullJoin:
                        joinString = "FULL OUTER JOIN";
                        break;
                    default:
                        joinString = "INNER JOIN";
                        break;
                }

                joins.Add(tableName, new KeyValuePair<string, WhereClip>(joinString, where));

                if (where.Parameters.Count > 0)
                    parameters.AddRange(where.Parameters);
            }

            return this;
        }
        
        /// <summary>
        /// Inner Join
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection InnerJoin(string tableName, WhereClip where, string userName = null)
        {
            return join(tableName, userName, where, JoinType.InnerJoin);
        }
        
        /// <summary>
        /// Left Join
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection LeftJoin(string tableName, WhereClip where, string userName = null)
        {
            return join(tableName, userName, where, JoinType.LeftJoin);
        }
        
        /// <summary>
        /// Right Join
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection RightJoin(string tableName, WhereClip where, string userName = null)
        {
            return join(tableName, userName, where, JoinType.RightJoin);
        }
        
        /// <summary>
        /// Cross Join
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection CrossJoin(string tableName, WhereClip where, string userName = null)
        {
            return join(tableName, userName, where, JoinType.CrossJoin);
        }
        
        /// <summary>
        /// Full Join
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public FromSection FullJoin(string tableName, WhereClip where, string userName = null)
        {
            return join(tableName, userName, where, JoinType.FullJoin);
        }

        #endregion

        #region Union

        /// <summary>
        /// Union
        /// </summary>
        /// <param name="fromSection"></param>
        /// <returns></returns>
        public FromSection Union(FromSection fromSection)
        {
            StringBuilder tname = new StringBuilder();

            tname.Append("(");

            tname.Append(this.SqlNoneOrderbyString);

            tname.Append(" UNION ");

            tname.Append(fromSection.SqlNoneOrderbyString);

            tname.Append(") tempuniontable ");

            FromSection tmpfromSection = new FromSection(this.InterpreterProvider, this.InterpreterExecutor, tname.ToString());
            tmpfromSection.typeTableName = this.typeTableName;
            tmpfromSection.isRefresh = this.isRefresh;


            tmpfromSection.parameters.AddRange(this.Parameters);
            tmpfromSection.parameters.AddRange(fromSection.Parameters);

            return tmpfromSection;
        }

        /// <summary>
        /// Union All
        /// </summary>
        /// <param name="fromSection"></param>
        /// <returns></returns>
        public FromSection UnionAll(FromSection fromSection)
        {
            StringBuilder tname = new StringBuilder();

            tname.Append("(");

            tname.Append(this.SqlNoneOrderbyString);

            tname.Append(" UNION ALL ");

            tname.Append(fromSection.SqlNoneOrderbyString);

            tname.Append(") tempuniontable ");

            FromSection tmpfromSection = new FromSection(this.InterpreterProvider,this.InterpreterExecutor, tname.ToString());
            tmpfromSection.typeTableName = this.typeTableName;
            tmpfromSection.isRefresh = this.isRefresh;

            tmpfromSection.parameters.AddRange(this.Parameters);
            tmpfromSection.parameters.AddRange(fromSection.Parameters);

            return tmpfromSection;
        }

        #endregion
    }
}
