using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastCommon.Logging
{
    public class NullLoggerFactory : ILoggerFactory
    {
        public ILogger Create(string name)
        {
            return NullLogger.Empty;
        }

        public ILogger Create(Type type)
        {
            return NullLogger.Empty;
        }

        public ILogger Create<TService>()
        {
            return NullLogger.Empty;
        }
    }
}
