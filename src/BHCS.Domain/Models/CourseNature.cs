using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;

namespace BHCS.Domain.Models
{
    public class CourseNature : AggregateRoot, ICreateAudit, IUpdateAudit, ISoftDelete
    {
        public CourseNature()
        {
        }

        public CourseNature(string name)
        {
            Ensure.NotNullOrWhiteSpace(name, "课程性质名称不能为空！");

            NatureId = Guid.NewGuid();
            Name = name;
            IsDelete = false;
        }

        public Guid NatureId { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid CreateBy { get; set; }

        public string CreateByName { get; set; }

        public DateTime UpdateTime { get; set; }

        public Guid UpdateBy { get; set; }

        public string UpdateByName { get; set; }

        public bool IsDelete { get; set; }
    }
}
