using BHCS.Application.Commanding;
using BHCS.Infrastructure.FastCommon.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BHCS.Querying.Querying;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Infrastructure.Middlewares;
using BHCS.Application.CommandingResults;
using BHCS.Web.Filters;

namespace BHCS.Web.Controllers
{
    public class AccountController:BaseController
    {
        private readonly IUserQuery _userQuery;

        public AccountController()
        {
            _userQuery = ObjectContainer.Resolve<IUserQuery>();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginCommand command,string verifyCode)
        {
            try
            {
                //Ensure.NotNullOrWhiteSpace(verifyCode, "验证码不能为空！");
                //var sessionVerifyCode = HttpContext.Session.GetString(CommonController.LoginValidateCode_SessionName);
                //Ensure.MustBeEqual(verifyCode.ToLower(), sessionVerifyCode==null?"":sessionVerifyCode.ToLower(), "验证码不正确！");
                var result = (LoginCommandResult)CommandBus.Send(command);
                MvcContext.SetUser(_userQuery.GetLoginUserInfo(result.UserId));

                return Json(result.ToResult());
            }
            catch(EnsureException ex)
            {
                return Json(new CommonResult(false, ex.Message));
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            MvcContext.SetUser(null);
            return RedirectToAction("login");
        }
        
    }
}
