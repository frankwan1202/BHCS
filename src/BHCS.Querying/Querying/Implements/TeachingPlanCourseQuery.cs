using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models.TeachingPlans;

namespace BHCS.Querying.Querying.Implements
{
    public class TeachingPlanCourseQuery : ITeachingPlanCourseQuery
    {
        public IList<TeachingPlanCourseReadModel> GetList(Guid planId)
        {
            string strsql = $@"select tpc.*,c.Name CourseName from TeachingPlanCourse tpc inner join Course c on tpc.CourseId=c.CourseId 
                            where tpc.TeachingPlanId='{planId}' and tpc.ClassesId='{Guid.Empty}' ";
            return QueryEnvironment.Current.GetQuickSqlSection().ToList<TeachingPlanCourseReadModel>(strsql);
        }
    }
}
