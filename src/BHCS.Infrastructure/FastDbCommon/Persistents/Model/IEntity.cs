using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Persistents.Model
{
    public interface IEntity:IEntity<Guid>
    {
    }

    public interface IEntity<TKey>
    {
    }
}
