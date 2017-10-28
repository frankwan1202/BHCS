using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Expressions
{
    public class Evaluator : IEvaluator
    {
        public object Eval(System.Linq.Expressions.Expression exp)
        {
            if (exp.NodeType == ExpressionType.Constant)
            {
                return ((ConstantExpression)exp).Value;
            }

            LambdaExpression expression = System.Linq.Expressions.Expression.Lambda(exp);
            Delegate fn = expression.Compile();

            return fn.DynamicInvoke(null);
        }
    }
}
