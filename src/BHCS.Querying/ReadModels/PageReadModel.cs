using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class PageReadModel
    {
        public Guid PageId { get; set; }

        public Guid MenuId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
