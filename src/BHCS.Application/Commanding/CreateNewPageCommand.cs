using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewPageCommand:Command
    {
        public Guid MenuId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
