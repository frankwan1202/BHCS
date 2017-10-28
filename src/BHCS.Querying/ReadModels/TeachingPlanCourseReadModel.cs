using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class TeachingPlanCourseReadModel
    {
        public Guid PlanCourseId { get; set; }

        public Guid TeachingPlanId { get; set; }

        public Guid CourseId { get; set;}

        public string CourseName { get; set; }
    }
}
