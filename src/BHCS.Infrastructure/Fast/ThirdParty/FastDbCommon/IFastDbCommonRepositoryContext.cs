using BHCS.Infrastructure.Fast.Domain.Repositories;
using BHCS.Infrastructure.FastDbCommon.Persistents;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon
{
    public interface IFastDbCommonRepositoryContext:IRepositoryContext
    {
        IPersistentContext DbContext { get; }
    }
}
