using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface IFunctionQuery
    {
        IPaged<FunctionReadModel> GetPage(Guid pageId, int pageIndex, int pageSize);
    }
}
