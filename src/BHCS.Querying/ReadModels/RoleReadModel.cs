using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class RoleReadModel
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid CreateBy { get; set; }

        public string CreateByName { get; set; }

        public DateTime UpdateTime { get; set; }

        public Guid UpdateBy { get; set; }

        public string UpdateByName { get; set; }

        public bool IsDelete { get; set; }
    }
}
