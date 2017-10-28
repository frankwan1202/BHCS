using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Data;
using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Clips;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure;
using System.Reflection;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure
{
    public abstract class InterpreterProvider : IInterpreterProvider
    {
        private readonly DbConnectionStringBuilder dbConnectionStringBuilder;
        private readonly char paramPrefixToken;

        public InterpreterProvider(DatabaseType databaseType, DbProviderFactory dbProviderFactory, char leftToken, char rightToken, char paramPrefixToken)
        {
            this.DatabaseType = databaseType;
            this.DbProviderFactory = dbProviderFactory;
            this.LeftToken = leftToken;
            this.RightToken = rightToken;
            this.paramPrefixToken = paramPrefixToken;
        }

        public DatabaseType DatabaseType { get; private set; }

        public DbProviderFactory DbProviderFactory { get; private set; }

        public char LeftToken { get; private set; }

        public char RightToken { get; private set; }

        public virtual string BuildParameterName(string name)
        {
            string nameStr = name.Trim(this.LeftToken, this.RightToken);
            if (nameStr[0] != paramPrefixToken)
            {
                if ("@?:".Contains(nameStr[0].ToString()))
                {
                    nameStr = nameStr.Substring(1).Insert(0, new string(paramPrefixToken, 1));
                }
                else
                {
                    nameStr = nameStr.Insert(0, new string(paramPrefixToken, 1));
                }
            }
            //剔除参数中的“.” 
            return nameStr.Replace(".", "_").TrimStart();
        }

        public virtual string BuildTableName(string name, string userName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    return string.Concat(this.LeftToken.ToString(), name.Trim(this.LeftToken, this.RightToken), this.RightToken.ToString());
                }
                return string.Concat(this.LeftToken.ToString(), userName.Trim(this.LeftToken, this.RightToken), this.RightToken.ToString())
                    + "."
                    + string.Concat(this.LeftToken.ToString(), name.Trim(this.LeftToken, this.RightToken), this.RightToken.ToString());
            }
        }

        public virtual FromSection CreatePageFromSection(FromSection fromSection, int startIndex, int endIndex)
        {            
            int pageSize = endIndex - startIndex + 1;
            if (startIndex == 1)
            {
                fromSection.PrefixString = string.Concat(" TOP ", pageSize.ToString());
            }
            else
            {
                if (OrderByClip.IsNullOrEmpty(fromSection.OrderByClip))
                {
                    foreach (Field f in fromSection.Fields)
                    {
                        if (!f.PropertyName.Equals("*") && f.PropertyName.IndexOf('(') == -1)
                        {
                            fromSection.OrderBy(f.Asc);
                            break;
                        }
                    }
                }

                int count = fromSection.Count(fromSection);

                List<Parameter> list = fromSection.Parameters;

                if (endIndex > count)
                {
                    int lastnumber = count - startIndex + 1;
                    if (startIndex > count)
                        lastnumber = count % pageSize;

                    fromSection.PrefixString = string.Concat(" TOP ", lastnumber.ToString());

                    fromSection.OrderBy(fromSection.OrderByClip.ReverseOrderByClip);

                    //

                    fromSection.TableName = string.Concat(" (", fromSection.SqlString, ") AS temp_table ");

                    fromSection.PrefixString = string.Empty;

                    fromSection.DistinctString = string.Empty;

                    fromSection.GroupBy(GroupByClip.None);

                    fromSection.Select(Field.All);

                    fromSection.OrderBy(fromSection.OrderByClip.ReverseOrderByClip);

                    fromSection.Where(WhereClip.All);

                }
                else
                {
                    if (startIndex < count / 2)
                    {

                        fromSection.PrefixString = string.Concat(" TOP ", endIndex.ToString());

                        fromSection.TableName = string.Concat(" (", fromSection.SqlString, ") AS tempIntable ");

                        fromSection.PrefixString = string.Concat(" TOP ", pageSize.ToString());

                        fromSection.DistinctString = string.Empty;

                        fromSection.GroupBy(GroupByClip.None);

                        fromSection.Select(Field.All);

                        fromSection.OrderBy(fromSection.OrderByClip.ReverseOrderByClip);

                        fromSection.Where(WhereClip.All);

                        //

                        fromSection.TableName = string.Concat(" (", fromSection.SqlString, ") AS tempOuttable ");

                        fromSection.PrefixString = string.Empty;

                        fromSection.OrderBy(fromSection.OrderByClip.ReverseOrderByClip);
                    }
                    else
                    {
                        fromSection.PrefixString = string.Concat(" TOP ", (count - startIndex + 1).ToString());

                        fromSection.OrderBy(fromSection.OrderByClip.ReverseOrderByClip);

                        fromSection.TableName = string.Concat(" (", fromSection.SqlString, ") AS tempIntable ");

                        fromSection.PrefixString = string.Concat(" TOP ", pageSize.ToString());

                        fromSection.DistinctString = string.Empty;

                        fromSection.GroupBy(GroupByClip.None);

                        fromSection.Select(Field.All);

                        fromSection.OrderBy(fromSection.OrderByClip.ReverseOrderByClip);

                        fromSection.Where(WhereClip.All);
                    }

                }

                fromSection.Parameters = list;

            }

            return fromSection;
        }

        public virtual void PrepareCommand(DbCommand cmd)
        {
            bool isStoredProcedure = (cmd.CommandType == CommandType.StoredProcedure);
            if (!isStoredProcedure)
            {
                cmd.CommandText = DbUtils.FormatSQL(cmd.CommandText, this.LeftToken, this.RightToken);
            }

            foreach (DbParameter p in cmd.Parameters)
            {

                if (!isStoredProcedure)
                {
                    //TODO 这里可以继续优化
                    if (cmd.CommandText.IndexOf(p.ParameterName, StringComparison.Ordinal) == -1)
                    {
                        //2015-08-11修改
                        cmd.CommandText = cmd.CommandText.Replace("@" + p.ParameterName.Substring(1), p.ParameterName);
                        cmd.CommandText = cmd.CommandText.Replace("?" + p.ParameterName.Substring(1), p.ParameterName);
                        cmd.CommandText = cmd.CommandText.Replace(":" + p.ParameterName.Substring(1), p.ParameterName);
                        //if (p.ParameterName.Substring(0, 1) == "?" || p.ParameterName.Substring(0, 1) == ":"
                        //        || p.ParameterName.Substring(0, 1) == "@")
                        //    cmd.CommandText = cmd.CommandText.Replace(paramPrefixToken + p.ParameterName.Substring(1), p.ParameterName);
                        //else
                        //    cmd.CommandText = cmd.CommandText.Replace(p.ParameterName.Substring(1), p.ParameterName);
                    }
                }

                if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                {
                    continue;
                }

                object value = p.Value;
                DbType dbType = p.DbType;

                if (value == DBNull.Value)
                {
                    continue;
                }

                if (value == null)
                {
                    p.Value = DBNull.Value;
                    continue;
                }

                Type type = value.GetType();

                if (type.GetTypeInfo().IsEnum)
                {
                    p.DbType = DbType.Int32;
                    p.Value = Convert.ToInt32(value);
                    continue;
                }

                if (dbType == DbType.Guid && type != typeof(Guid))
                {
                    p.Value = new Guid(value.ToString());
                    continue;
                }
                #region 2015-09-08注释
                ////2015-09-07 写
                //var v = value.ToString();
                //if (DatabaseType == DatabaseType.MsAccess
                //    && (dbType == DbType.AnsiString || dbType == DbType.String)
                //    && !string.IsNullOrWhiteSpace(v)
                //    && cmd.CommandText.ToLower()
                //    .IndexOf("like " + p.ParameterName.ToLower(), StringComparison.Ordinal) > -1)
                //{
                //    if (v[0] == '%')
                //    {
                //        v = "*" + v.Substring(1);
                //    }
                //    if (v[v.Length-1] == '%')
                //    {
                //        v = v.TrimEnd('%') + "*";
                //    }
                //    p.Value = v;
                //}
                #endregion
                //if ((dbType == DbType.AnsiString || dbType == DbType.String ||
                //    dbType == DbType.AnsiStringFixedLength || dbType == DbType.StringFixedLength) && (!(value is string)))
                //{
                //    p.Value = SerializationManager.Serialize(value);
                //    continue;
                //}

                if (type == typeof(Boolean))
                {
                    p.Value = (((bool)value) ? 1 : 0);
                    continue;
                }
            }
        }
        
        public string InterpreterQueryBuilder(QueryBuilder queryBuilder)
        {
            if (queryBuilder.TableName == null || queryBuilder.TableName.Count <= 0)
            {
                throw new QueryInterpreterException("请至少添加一项FromTable！");
            }

            if (queryBuilder.TableName.Count > 1 && (queryBuilder.SelectColumns == null || queryBuilder.SelectColumns.Count <= 0))
            {
                throw new QueryInterpreterException("当Table达到两个及两个以上，必须显式指定查询的列！");
            }

            return this.DoInterpreter(queryBuilder);
        }


        protected abstract string DoInterpreter(QueryBuilder queryBuilder);
    }
}
