using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewMenuCommand:Command
    {
        public string Name { get; set; }

        public int Sort { get; set; }
    }
}
