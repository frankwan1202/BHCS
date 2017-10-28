using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface IGradeQuery
    {
        IList<GradeReadModel> GetList(bool isLoadDeletedData = false);

        IPaged<GradeReadModel> GetPage(int pageIndex, int pageSize);
    }
}
