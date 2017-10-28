using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Persistents.Model
{
    public enum OperationState
    {
        UnChanged=-1,
        Insert=0,
        Modify=1,
        Delete=2
    }
}
