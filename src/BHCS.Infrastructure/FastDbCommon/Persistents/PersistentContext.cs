using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using BHCS.Infrastructure.FastDbCommon.Persistents.Model;
using System.Data.Common;
using System.Data;
using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using BHCS.Infrastructure.FastDbCommon.Persistents.Transactions;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Infrastructure.FastDbCommon.Persistents
{
    public interface IPersistentContext:IDisposable
    {
        void Add<TEntity>(TEntity entity) where TEntity : IEntity;

        void Update<TEntity>(TEntity entity) where TEntity :  IEntity;

        void Delete<TEntity>(TEntity entity) where TEntity :  IEntity;

        TEntity GetById<TEntity>(object entity) where TEntity : IEntity;

        bool SaveChange();
    }

    public class PersistentContext : IPersistentContext
    {
        private ConcurrentQueue<EntitySet> _operQueue = new ConcurrentQueue<EntitySet>();
        private Database _database;
        private QueryEnvironment _query;
        private bool _isDispose = false;
 
        public PersistentContext(Database database)
        {
            _database = database;
        }        
        
        public void Add<TEntity>(TEntity entity) where TEntity : IEntity
        {
            _operQueue.Enqueue(new EntitySet(entity,OperationState.Insert));
        }

        public void Update<TEntity>(TEntity entity) where TEntity : IEntity
        {
            _operQueue.Enqueue(new EntitySet(entity, OperationState.Modify));
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : IEntity
        {
            _operQueue.Enqueue(new EntitySet(entity, OperationState.Delete));
        }

        public TEntity GetById<TEntity>(object id) where TEntity : IEntity
        {
            return _database.GetById<TEntity>(id);
        }

        public bool SaveChange()
        {
            using (ITransaction transaction =new Transaction(_database))
            {
                transaction.Begin();
                try
                {
                    while (_operQueue.Count > 0)
                    {
                        EntitySet entitySet;
                        if (!_operQueue.TryDequeue(out entitySet))
                        {
                            transaction.Roolback();
                            transaction.Dispose();
                            throw new PersistentException("从操作队列中取出数据失败，操作队列还有：" + _operQueue.Count + "项");
                        }

                        switch (entitySet.OperationState)
                        {
                            case OperationState.Insert:_database.Insert(entitySet.Entity,transaction.CurrentDbTransaction);break;
                            case OperationState.Modify:_database.Modify(entitySet.Entity, transaction.CurrentDbTransaction);break;
                            case OperationState.Delete: _database.Delete(entitySet.Entity, transaction.CurrentDbTransaction); break;
                        }
                    }

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Roolback();

                    throw ex;
                }
            }
        }

        public void Dispose()
        {
            if (_isDispose)
            {
                return;
            }

            _operQueue = null;
            _database = null;
            _query = null;

            _isDispose = true;
        }
    }
}
