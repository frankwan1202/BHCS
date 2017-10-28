using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure
{
    public class DataException:System.Exception
    {
        public DataException(string message,Exception innerException) : base(message, innerException)
        { }
    }
}
