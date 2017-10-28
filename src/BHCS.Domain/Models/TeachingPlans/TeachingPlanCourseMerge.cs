using BHCS.Infrastructure.Fast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models.TeachingPlans
{
    public class TeachingPlanCourseMerge:AggregateRoot
    {
        public Guid MergeId { get; set; }

        public string MergeCode { get; set; }

        public Guid PlanCourseId { get; set; }

        public Guid ClassesId { get; set; }
    }
}
