using BHCS.Infrastructure.Fast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models
{
    public class RoleMenu:AggregateRoot
    {
        public Guid RoleMenuId { get; set; }

        public Guid RoleId { get; set; }

        public Guid MenuId { get; set; }

        public Guid PageId { get; set; }

        public Guid FuncId { get; set; }
    }
}
