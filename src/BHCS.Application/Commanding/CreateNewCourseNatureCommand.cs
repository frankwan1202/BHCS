using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewCourseNatureCommand:Command
    {
        public string NatureName { get; set; }
    }
}
