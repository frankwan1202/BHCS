using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Model
{
    public class QueryInterpreterException : Exception
    {
        public QueryInterpreterException(String errorMessage)
            : base(errorMessage)
        { 
        }
    }
}
