using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon
{
    public class FastDbCommonRepository<TAggregateRoot> : Repository<TAggregateRoot, IFastDbCommonRepositoryContext> where TAggregateRoot : class, IAggregateRoot
    {
        protected override TAggregateRoot DoGet(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return QueryEnvironment.Current.GetFromSection<TAggregateRoot>().Where(predicate).ToFirst();
        }

        protected override IList<TAggregateRoot> DoGetAll(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return QueryEnvironment.Current.GetFromSection<TAggregateRoot>().Where(predicate).ToList();
        }

        protected override TAggregateRoot DoGetById(object id)
        {
            return Context.DbContext.GetById<TAggregateRoot>(id);
        }
    }
}
