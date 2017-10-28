using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface IClassesQuery
    {
        IList<ClassesReadModel> GetList(bool isLoadDeletedData = false);

        IPaged<ClassesReadModel> GetPage(int pageIndex, int pageSize);
    }
}
