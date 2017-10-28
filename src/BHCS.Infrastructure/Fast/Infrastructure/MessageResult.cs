using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Infrastructure
{
    public interface IMessageResult
    {
        ResultCode Code { get; }

        string Message { get; }
    }

    public class MessageResult : IMessageResult
    {
        public MessageResult()
        { }

        public MessageResult(ResultCode code,string message)
        {
            Code = code;
            Message = message;
        }

        public ResultCode Code { get; protected set; }

        public string Message { get; protected set; }
    }

    public enum ResultCode
    {
        Ok=0,
        BussinessError=-1,
        Exception=-2
    }
}
