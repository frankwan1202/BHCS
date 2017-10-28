using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using SkyCreative.QuickApp.Data.Extensions;
using BHCS.Infrastructure.FastDbCommon.Persistents;
using BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.MySql
{
    public class MySqlDatabase : Database
    {
        private MySqlEntitySqlCache _entitySqlCache;

        public MySqlDatabase(string nameOrConnectString) : base(nameOrConnectString)
        {
        }

        public override IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectString);
        }

        public override IDbTransaction CreateTransaction(IDbConnection connection = null)
        {
            if (connection == null)
            {
                connection = new MySqlConnection(ConnectString);
            }

            return connection.BeginTransaction();
        }

        public override IDataReader ExecuteDataReader(DbCommand command)
        {
            MySqlConnection connection = new MySqlConnection(ConnectString);
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
            using (MySqlConnection connection = new MySqlConnection(ConnectString))
            {
                connection.Open();
                command.Connection = connection;
                return command.ExecuteScalar();
            }
        }

        public override TEntity GetById<TEntity>(object id)
        {
            MySqlCommand command = new MySqlCommand(GetEntitySql(typeof(TEntity)).SelectByIdSql);
            command.AddParam(id);
            return EntityUtils.ReaderToEnumerable<TEntity>(ExecuteDataReader(command)).FirstOrDefault();
        }

        protected override bool ExecuteSql(string executeSql, object[] parameters, IDbTransaction transaction = null)
        {
            MySqlConnection connection;
            if (transaction == null)
            {
                connection = new MySqlConnection(ConnectString);
                connection.Open();
            }
            else
            {
                connection =(MySqlConnection) transaction.Connection;
            }

            bool result;
            using (MySqlCommand command = new MySqlCommand(executeSql, connection))
            {
                command.AddParams(parameters);

                result= command.ExecuteNonQuery() > 0 ? true : false;
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
                _entitySqlCache = new MySqlEntitySqlCache(this);
            }

            return _entitySqlCache.GetEntitySql(entityType);
        }
    }
}
