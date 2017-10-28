using BHCS.Application.Commanding;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Querying.Querying;
using BHCS.Web.Controllers;
using BHCS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BHCS.Web.Areas.System.Controllers
{
    public class RoleController:SystemBaseController
    {
        private readonly IRoleQuery _roleQuery;

        public RoleController()
        {
            _roleQuery = ObjectContainer.Resolve<IRoleQuery>();
        }

        [HttpGet]
        [Priviledge(true, "/system/role/index")]
        public IActionResult Index(int pageIndex=1,int pageSize=20)
        {
            return  View(_roleQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(1, "/system/role/createnewrole")]
        public IActionResult CreateNewRole(CreateNewRoleCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }

        [HttpGet]
        [Priviledge(2, "/system/role/authorize")]
        public IActionResult Authorize(Guid roleId)
        {
            return View();
        }
        
    }
}