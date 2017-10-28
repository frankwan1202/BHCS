using BHCS.Infrastructure.Fast.ThirdParty.Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;
using BHCS.Infrastructure.Fast.Configurations;

namespace BHCS.Repositories.Dapper.MySql
{
    public class MySqlDapperHelper : IDapperDbHelper
    {
        private readonly MySqlConnection _connection;

        public MySqlDapperHelper()
        {
            _connection = new MySqlConnection(FastConfiguration.Instance.Setting.BussinessConnectString);
        }

        public void Delete<T>(T t)
        {
            //_connection.
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(string strsql)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T SingleOrDefault<T>(string strsql)
        {
            throw new NotImplementedException();
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
