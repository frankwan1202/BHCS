using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewCourseCommand:Command
    {
        public string Name { get; set; }

        public Guid NatureId { get; set; }

        public double Hours { get; set; }

        public Guid MajorId { get; set; }
    }
}
