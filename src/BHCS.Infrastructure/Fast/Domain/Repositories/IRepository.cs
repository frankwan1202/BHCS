using BHCS.Infrastructure.Fast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BHCS.Infrastructure.Fast.Domain.Repositories
{
    public interface IRepository<TAggregateRoot>where TAggregateRoot:class,IAggregateRoot
    {
        void Insert(TAggregateRoot aggregate);

        void Update(TAggregateRoot aggregate);

        void Delete(TAggregateRoot aggregate);

        TAggregateRoot GetById(object id);

        TAggregateRoot Get(Expression<Func<TAggregateRoot, bool>> predicate);

        IList<TAggregateRoot> GetAll(Expression<Func<TAggregateRoot, bool>> predicate);
    }
}
