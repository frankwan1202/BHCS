using BHCS.Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.Middlewares
{
    public class MvcContextMiddleware
    {
        private readonly RequestDelegate _next;

        public MvcContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            MvcContext.Context = context;

            return _next.Invoke(context);
        }
    }

    public class MvcContext
    {
        public const string Member_CookieName = "_d_u_";
        private static ThreadLocal<HttpContext> _context = new ThreadLocal<HttpContext>();

        public static SignMember User
        {
            get
            {
                return HttpCookieHelper.Get<SignMember>(MvcContext.Member_CookieName, true);
            }
        }


        public static HttpContext Context { get { return _context.Value; } internal set { _context.Value = value; } }

        public static void SetUser(SignMember signMember)
        {
            if (signMember == null)
            {
                MvcContext.Context.Response.Cookies.Delete(Member_CookieName);
            }
            else
            {
                HttpCookieHelper.AddOrUpdate(Member_CookieName, signMember, encrypted: false);
            }
        }
    }

    public class SignMember
    {
        public SignMember()
        {
        }

        public SignMember(Guid userId, string account,string userName, Guid roleId, string roleName, IList<SignFunction> funcs)
        {
            UserId = userId;
            Account = account;
            UserName = userName;
            RoleId = roleId;
            RoleName = roleName;
            Funcs = funcs;
        }

        public Guid UserId { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public IList<SignFunction> Funcs { get; set; }
    }

    public class SignFunction
    {
        public SignFunction()
        {
        }

        public SignFunction(int funcValue, string url)
        {
            FuncValue = funcValue;
            Url = url;
        }

        public int FuncValue { get; set; }

        public string Url { get; set; }
    }
}
