using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class RoleAuthorizeCommand:Command
    {
        public Guid RoleId { get; set; }
        
        public IList<PageCommandMessage> FuncIds { get; set; }
    }

    public class PageCommandMessage
    {
        public int PageId { get; set; }

        public IList<int> FuncIds { get; set; }
    }
}
