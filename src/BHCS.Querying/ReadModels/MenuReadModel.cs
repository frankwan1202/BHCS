using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class MenuReadModel
    {
        public Guid MenuId { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }
    }
}
