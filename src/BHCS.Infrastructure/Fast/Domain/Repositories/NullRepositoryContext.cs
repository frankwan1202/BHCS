using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHCS.Infrastructure.Fast.Domain.Models;

namespace BHCS.Infrastructure.Fast.Domain.Repositories
{
    public class NullRepositoryContext : IRepositoryContext
    {
        public bool IsCommited =>false;

        public bool IsDisposed => false;

        public void Commit()
        {
        }

        public void Dispose()
        {
        }

        public void RegisterDelete<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot
        {
        }

        public void RegisterInsert<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot
        {
        }

        public void RegisterUpdate<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot
        {
        }

        public void Rollback()
        {
        }
    }
}
