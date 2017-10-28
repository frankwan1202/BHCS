using BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure;
using SkyCreative.QuickApp.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.SqlServer
{
    public class SqlServerDatabase : Database
    {
        private SqlServerEntitySqlCache _entitySqlCache;

        public SqlServerDatabase(string nameOrConnectString) : base(nameOrConnectString)
        {
        }

        public override IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectString);
        }

        public override IDbTransaction CreateTransaction(IDbConnection connection = null)
        {
            if (connection == null)
            {
                connection = new SqlConnection(ConnectString);
            }

            return connection.BeginTransaction();
        }

        public override IDataReader ExecuteDataReader(DbCommand command)
        {
            SqlConnection connection = new SqlConnection(ConnectString);
            connection.Open();
            command.Connection = connection;
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public override DataTable ExecuteDataTable(DbCommand command)
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar(DbCommand command)
        {
            using (SqlConnection connection = new SqlConnection(ConnectString))
            {
                connection.Open();
                command.Connection = connection;
                return command.ExecuteScalar();
            }
        }

        public override TEntity GetById<TEntity>(object id)
        {
            SqlCommand command = new SqlCommand(GetEntitySql(typeof(TEntity)).SelectByIdSql);
            command.AddParam(id);
            return EntityUtils.ReaderToEnumerable<TEntity>(ExecuteDataReader(command)).FirstOrDefault();
        }

        protected override bool ExecuteSql(string executeSql, object[] parameters, IDbTransaction transaction = null)
        {
            SqlConnection connection;
            if (transaction == null)
            {
                connection = new SqlConnection(ConnectString);
                connection.Open();
            }
            else
            {
                connection = (SqlConnection)transaction.Connection;
            }

            bool result;
            using (SqlCommand command = new SqlCommand(executeSql, connection))
            {
                command.Transaction = (SqlTransaction)transaction;
                command.AddParams(parameters);

                result = command.ExecuteNonQuery() > 0 ? true : false;
            }

            if (transaction == null)
            {
                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        protected override EntitySql GetEntitySql(Type entityType)
        {
            if (_entitySqlCache == null)
            {
                _entitySqlCache = new SqlServerEntitySqlCache(this);
            }

            return _entitySqlCache.GetEntitySql(entityType);
        }
    }
}
