using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Persistents.Transactions
{
    public class Transaction : ITransaction
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private bool _isCommited=false;
        private bool _isDispose=false;
        private readonly Database _database;

        public Transaction(Database database)
        {
            _database = database;
            _dbConnection = database.CreateConnection();
        }

        public bool IsCommited
        {
            get
            {
                return _isCommited;
            }
        }

        public IDbTransaction CurrentDbTransaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        public void Begin()
        {
            _dbConnection.Open();

            _dbTransaction = _database.CreateTransaction(_dbConnection);
        }

        public void Commit()
        {
            _dbTransaction.Commit();

            _isCommited = true;
        }

        public void Dispose()
        {
            if(_isDispose)
            {
                return;
            }

            _dbTransaction.Dispose();
            _dbConnection.Close();
            _dbConnection.Dispose();

            _isDispose = true;
        }

        public virtual void Roolback()
        {
            _dbTransaction.Rollback();
        }
    }
}
