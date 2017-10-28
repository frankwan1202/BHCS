using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class FunctionReadModel
    {
        public Guid FunctionId { get; set; }

        public Guid PageId { get; set; }

        public int OperationNum { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
