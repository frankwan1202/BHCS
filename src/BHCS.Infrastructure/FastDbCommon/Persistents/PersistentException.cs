using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Persistents
{
    public class PersistentException:System.Exception
    {
        public PersistentException(string message) : base(message) { }
    }
}
