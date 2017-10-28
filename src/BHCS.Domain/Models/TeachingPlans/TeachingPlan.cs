using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models.TeachingPlans
{
    public class TeachingPlan : AggregateRoot, ICreateAudit, IUpdateAudit
    {
        public TeachingPlan()
        {
        }

        public TeachingPlan(string semester, string remark, Guid majorId, Guid gradeId)
        {
            Ensure.NotNullOrWhiteSpace(semester, "学期不能为空！");
            Ensure.NotNull(majorId, "专业不能为空！");
            Ensure.NotNull(gradeId, "年级不能为空！");

            PlanId = Guid.NewGuid();
            Semester = semester;
            Remark = remark;
            MajorId = majorId;
            GradeId = gradeId;
            IsAccept = false;
        }

        public Guid PlanId { get; set; }

        public string Semester { get; set; }

        public string Remark { get; set; }

        public Guid MajorId { get; set; }

        public Guid GradeId { get; set; }
        
        public DateTime UpdateTime { get; set; }

        public Guid UpdateBy { get; set; }

        public string UpdateByName { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid CreateBy { get; set; }

        public string CreateByName { get; set; }

        public bool IsAccept { get; set; }
    }
}
