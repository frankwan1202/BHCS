using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastCommon.Logging
{
    public class NullLogger : ILogger
    {
        public readonly static NullLogger Empty = new NullLogger();

        public bool IsEnableDebugger { get { return false; } }

        public void Debug(string message)
        {
        }

        public void Debug(string message, Exception ex)
        {
        }

        public void DebugFormat(string message, params object[] formats)
        {
        }
        
        public void Error(string message)
        {
        }

        public void Error(string message, Exception ex)
        {
        }

        public void ErrorFormat(string message, params object[] formats)
        {
        }

        public void ErrorFormat(string message, params string[] formats)
        {
        }

        public void Fatal(string message)
        {
        }

        public void Fatal(string message, Exception ex)
        {
        }

        public void FatalFormat(string message, params object[] formats)
        {
        }

        public void FatalFormat(string message, params string[] formats)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, Exception ex)
        {
        }

        public void InfoFormat(string message, params object[] formats)
        {
        }

        public void InfoFormat(string message, params string[] formats)
        {
        }

        public void Warn(string message)
        {
        }

        public void Warn(string message, Exception ex)
        {
        }

        public void WarnFormat(string message, params object[] formats)
        {
        }

        public void WarnFormat(string message, params string[] formats)
        {
        }
    }
}
