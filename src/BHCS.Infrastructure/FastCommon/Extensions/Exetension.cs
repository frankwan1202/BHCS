using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.Fast.Infrastructure;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS
{
    public static class Exetension
    {
        public static CommonResult ToResult(this ICommandResult result)
        {
            return new CommonResult() { State = result.Code == ResultCode.Ok ? true : false, Message = result.Message };
        }
    }
}
