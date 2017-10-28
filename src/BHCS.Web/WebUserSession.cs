using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BHCS.Infrastructure.Middlewares;

namespace BHCS.Web
{
    public class WebUserSession : IUserSession
    {
        public WebUserSession()
        {

        }

        public Guid UserId =>MvcContext.User.UserId;

        public string Account => MvcContext.User.Account;

        public string UserName => MvcContext.User.UserName;

        public HttpContext HttpContext => MvcContext.Context;
    }
}
