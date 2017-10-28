using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class CourseReadModel
    {
        public Guid CourseId { get; set; }

        public string Name { get; set; }

        public Guid NatureId { get; set; }

        public string NatureName { get; set; }

        public double Hours { get; set; }

        public Guid MajorId { get; set; }

        public string MajorName { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateTimeStr => CreateTime.ToString("yyyy/MM/dd HH:mm:ss");

        public Guid CreateBy { get; set; }

        public string CreateByName { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateTimeStr => UpdateTime.HasValue ? UpdateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";

        public Guid UpdateBy { get; set; }

        public string UpdateByName { get; set; }
    }
}
