using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models.TeachingPlans
{
    public class TeachingPlanCourse:AggregateRoot
    {
        public TeachingPlanCourse()
        {
        }

        public TeachingPlanCourse(Guid teachingPlanId, Guid courseId)
        {
            Ensure.NotNull(teachingPlanId, "教学计划信息不能为空！");
            Ensure.NotNull(courseId, "课程信息不能为空！");

            PlanCourseId = Guid.NewGuid();
            TeachingPlanId = teachingPlanId;
            CourseId = courseId;
        }

        public Guid PlanCourseId { get; set; }

        public Guid TeachingPlanId { get; set; }

        public Guid CourseId { get; set; }

        public Guid ClassesId { get; set; }

        public Guid MainTeacherId { get; set; }
        
        public int CourseNumber { get; set; }

        public Guid BuildingId { get; set; }
    }
}
