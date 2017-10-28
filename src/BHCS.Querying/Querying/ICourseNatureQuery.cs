using BHCS.Domain.Models;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface ICourseNatureQuery
    {
        IPaged<CourseNatureReadModel> GetPage(int pageIndex, int pageSize);

        IList<CourseNatureReadModel> GetList(bool isLoadDeleted=false);
    }
}
