using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Expressions
{
    public interface IEvaluator
    {
        object Eval(System.Linq.Expressions.Expression exp);
    }
}
