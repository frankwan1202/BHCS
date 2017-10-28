using BHCS.Infrastructure.Fast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using BHCS.Infrastructure.Fast.Domain.UnitOfWorks;
using BHCS.Infrastructure.FastCommon.Components;

namespace BHCS.Infrastructure.Fast.Domain.Repositories
{
    public abstract class Repository<TAggregateRoot,TRepositoryContext> : IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot where TRepositoryContext:IRepositoryContext
    {
        private readonly ICurrentRepositoryContextProvider _provider;

        public Repository()
        {
            _provider = ObjectContainer.Resolve<ICurrentRepositoryContextProvider>();
        }

        protected TRepositoryContext Context => (TRepositoryContext)_provider.Current;

        public void Delete(TAggregateRoot aggregate)
        {
            Context.RegisterDelete(aggregate);
        }

        public TAggregateRoot Get(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return DoGet(predicate);       
        }

        public IList<TAggregateRoot> GetAll(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return DoGetAll(predicate);
        }

        public TAggregateRoot GetById(object id)
        {
            return DoGetById(id);
        }

        public void Insert(TAggregateRoot aggregate)
        {
            Context.RegisterInsert(aggregate);
        }

        public void Update(TAggregateRoot aggregate)
        {
            Context.RegisterUpdate(aggregate);
        }

        protected abstract TAggregateRoot DoGet(Expression<Func<TAggregateRoot, bool>> predicate);

        protected abstract TAggregateRoot DoGetById(object id);

        protected abstract IList<TAggregateRoot> DoGetAll(Expression<Func<TAggregateRoot, bool>> predicate);

    }
}
