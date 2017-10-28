using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.FastCommon.Utilities
{
    public class CommonResult
    {
        public CommonResult()
        { }

        public CommonResult(bool state,string message)
        {
            State = state;
            Message = message;
        }

        public bool State { get; set; }

        public string Message { get; set; }
    }
}
