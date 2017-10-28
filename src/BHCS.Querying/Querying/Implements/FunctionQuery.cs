using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models.Menus;

namespace BHCS.Querying.Querying.Implements
{
    public class FunctionQuery : IFunctionQuery
    {
        public IPaged<FunctionReadModel> GetPage(Guid pageId, int pageIndex, int pageSize)
        {
            return QueryEnvironment.Current.GetFromSection<Function>().Where(p => p.PageId == pageId).ToPage<FunctionReadModel>(pageSize, pageIndex);
        }
    }
}
