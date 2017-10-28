using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.Fast.Domain.UnitOfWorks;

namespace BHCS.Infrastructure.Fast.Domain.Repositories
{
    public abstract class RepositoryContext :UnitOfWork, IRepositoryContext
    {
        public virtual void RegisterDelete<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterInsert<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterUpdate<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}
