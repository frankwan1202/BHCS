using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models;

namespace BHCS.Querying.Querying.Implements
{
    public class CourseNatureQuery : ICourseNatureQuery
    {
        public IList<CourseNatureReadModel> GetList(bool isLoadDeleted = false)
        {
            if (isLoadDeleted) return QueryEnvironment.Current.GetFromSection<CourseNature>().ToList<CourseNatureReadModel>();
            else return QueryEnvironment.Current.GetFromSection<CourseNature>().Where(p => !p.IsDelete).ToList<CourseNatureReadModel>();
        }

        public IPaged<CourseNatureReadModel> GetPage(int pageIndex, int pageSize)
        {
            return QueryEnvironment.Current.GetFromSection<CourseNature>().OrderByDescending(p => p.CreateTime).ToPage<CourseNatureReadModel>(pageSize, pageIndex);
        }
    }
}
