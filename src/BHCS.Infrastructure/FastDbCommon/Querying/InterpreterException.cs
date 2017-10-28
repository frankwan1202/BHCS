using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying
{
    public class InterpreterException:System.Exception
    {
        public InterpreterException(String errorMessage)
            : base(errorMessage)
        { 
        }
    }
}
