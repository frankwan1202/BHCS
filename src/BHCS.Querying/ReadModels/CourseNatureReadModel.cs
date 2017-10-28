using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class CourseNatureReadModel
    {
        public Guid NatureId { get; set; }

        public string Name { get; set; }

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
