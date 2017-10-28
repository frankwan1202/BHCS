using System;
using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;

namespace BHCS.Domain.Models
{
    public class Course : AggregateRoot, ICreateAudit, IUpdateAudit, ISoftDelete
    {
        public Course()
        {
        }

        public Course(string name, Guid natureId, double hours, Guid majorId)
        {
            Ensure.NotNullOrWhiteSpace(name, "课程名不能为空！");
            Ensure.NotNull(natureId, "课程性质不能为空！");
            Ensure.NotNull(majorId, "所属专业不能为空！");

            CourseId = Guid.NewGuid();
            Name = name;
            NatureId = natureId;
            Hours = hours;
            MajorId = majorId;
        }

        public Guid CourseId { get; set; }

        public string Name { get; set; }

        public Guid NatureId { get; set; }

        public double Hours { get; set; }

        public Guid MajorId { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsDelete { get; set; }

        public Guid CreateBy { get; set; }

        public string CreateByName { get; set; }

        public DateTime UpdateTime { get; set; }

        public Guid UpdateBy { get; set; }

        public string UpdateByName { get; set; }
    }
}