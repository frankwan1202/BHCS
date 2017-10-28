using BHCS.Infrastructure.FastCommon.Utilities;
using BHCS.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Web.Filters
{
    public class PriviledgeAttribute : Attribute, IActionFilter
    {
        public PriviledgeAttribute(bool isJump)
        {
            IsJump = isJump;
        }

        public PriviledgeAttribute(int operationValue,string url):this(false)
        {
            OperationValue = operationValue;
            Url = url;
        }

        public PriviledgeAttribute(bool isPage, string url) : this(false)
        {
            IsPage = isPage;
            Url = url;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (IsJump) return;
            if (MvcContext.User == null)
            {
                if (context.HttpContext.Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
                {
                    context.Result = new JsonResult(new CommonResult(false, "未登录"));
                }
                else
                {
                    context.Result = new RedirectToActionResult("login", "account", new { area = "" });
                }
                return;
            }
            //else if (IsPage && MvcContext.User.Funcs.FirstOrDefault(p => p.Url == Url) != null) return;
            //else if (MvcContext.User.Funcs.FirstOrDefault(p => p.Url == Url && p.FuncValue == OperationValue) != null) return;
            //else
            //{
            //    if (context.HttpContext.Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            //    {
            //        context.Result = new JsonResult(new CommonResult(false, "无权访问"));
            //    }
            //    else
            //    {
            //        context.Result = new StatusCodeResult(403);
            //    }
            //    return;
            //}
        }

        public bool IsJump { get; set; }

        public int OperationValue { get; set; }

        public string Url { get; set; }

        public bool IsPage { get; set; }
    }
}
