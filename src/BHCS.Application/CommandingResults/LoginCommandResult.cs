using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.Fast.Infrastructure;

namespace BHCS.Application.CommandingResults
{
    public class LoginCommandResult : CommandResult
    {
        public LoginCommandResult()
        {
        }

        public LoginCommandResult(Guid userId):base(ResultCode.Ok,"")
        {
            UserId = userId;
        }

        public LoginCommandResult(ResultCode code, string message) : base(code, message)
        {
        }

        public Guid UserId { get; set; }
    }
}
