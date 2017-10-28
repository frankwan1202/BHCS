using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewTeachingPlanCommand:Command
    {
        public string Semester { get; set; }

        public string Remark { get; set; }

        public Guid MajorId { get; set; }

        public Guid GradeId { get; set; }

        public IList<Guid> CourseIds { get; set; }
    }
}
