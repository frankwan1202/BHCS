using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface ITeachingPlanQuery
    {
        IPaged<TeachingPlanReadModel> GetPage(int pageIndex, int pageSize,bool? isAccept=null);
    }
}
