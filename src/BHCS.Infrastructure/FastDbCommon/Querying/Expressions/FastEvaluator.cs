﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Expressions
{
    public class FastEvaluator : IEvaluator
    {
        private static IExpressionCache<Func<List<object>, object>> s_cache =
            new HashedListCache<Func<List<object>, object>>();

        private IExpressionCache<Func<List<object>, object>> m_cache;
        private Func<System.Linq.Expressions.Expression, Func<List<object>, object>> m_creatorDelegate;

        public FastEvaluator()
            : this(s_cache)
        { }

        public FastEvaluator(IExpressionCache<Func<List<object>, object>> cache)
        {
            this.m_cache = cache;
            this.m_creatorDelegate = (key) => new DelegateGenerator().Generate(key);
        }

        public object Eval(System.Linq.Expressions.Expression exp)
        {
            if (exp.NodeType == ExpressionType.Constant)
            {
                return ((ConstantExpression)exp).Value;
            }

            var parameters = new ConstantExtractor().Extract(exp);
            var func = this.m_cache.Get(exp, this.m_creatorDelegate);
            return func(parameters);
        }
    }        
}
