using BHCS.Infrastructure.Fast.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Commanding
{
    public interface ICommandResult:IMessageResult
    {
    }

    public class CommandResult : MessageResult, ICommandResult
    {
        public CommandResult()
        {
        }

        public CommandResult(ResultCode code, string message) : base(code, message)
        {
        }
    }
}
