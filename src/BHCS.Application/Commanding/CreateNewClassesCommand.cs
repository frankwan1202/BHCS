using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewClassesCommand:Command
    {
        public string Name { get; set; }

        public string ClassesNo { get; set; }

        public Guid GradeId { get; set; }
    }
}
