using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewGradeCommand:Command
    {
        public string Name { get; set; }
    }
}
