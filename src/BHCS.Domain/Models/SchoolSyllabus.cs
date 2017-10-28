using BHCS.Infrastructure.Fast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models
{
    public class SchoolSyllabus : AggregateRoot, ICreateAudit
    {
        

        public DateTime CreateTime { get; set; }

        public Guid CreateBy { get; set; }

        public string CreateByName { get; set; }
    }
}
