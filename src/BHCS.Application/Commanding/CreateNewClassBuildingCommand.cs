using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewClassBuildingCommand:Command
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
