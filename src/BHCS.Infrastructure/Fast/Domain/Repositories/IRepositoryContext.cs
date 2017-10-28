using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.Fast.Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Domain.Repositories
{
    public interface IRepositoryContext:IUnitOfWork
    {
        void RegisterInsert<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class,IAggregateRoot;

        void RegisterUpdate<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot;

        void RegisterDelete<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : class, IAggregateRoot;
    }
}
