using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastCommon.Logging
{
    public interface ILogger
    {
        bool IsEnableDebugger { get;  }

        void Debug(string message);

        void Debug(string message, Exception ex);

        void DebugFormat(string message, params object[] formats);

        void Info(string message);

        void Info(string message, Exception ex);

        void InfoFormat(string message, params object[] formats);

        void Warn(string message);

        void Warn(string message, Exception ex);

        void WarnFormat(string message, params object[] formats);

        void Error(string message);

        void Error(string message, Exception ex);

        void ErrorFormat(string message, params object[] formats);

        void Fatal(string message);

        void Fatal(string message, Exception ex);

        void FatalFormat(string message, params object[] formats);
    }
}
