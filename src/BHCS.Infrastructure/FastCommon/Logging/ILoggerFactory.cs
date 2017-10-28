using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastCommon.Logging
{
    public interface ILoggerFactory
    {
        ILogger Create(string name);

        ILogger Create(Type type);

        ILogger Create<TService>();
    }
}
