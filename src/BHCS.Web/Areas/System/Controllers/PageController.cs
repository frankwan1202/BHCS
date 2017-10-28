using BHCS.Application.Commanding;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Querying.Querying;
using BHCS.Web.Controllers;
using BHCS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Web.Areas.System.Controllers
{
    public class PageController:SystemBaseController
    {
        private readonly IMenuQuery _menuQuery;
        private readonly IPageQuery _pageQuery;
        private readonly IFunctionQuery _functionQuery;

        public PageController()
        {
            _menuQuery = ObjectContainer.Resolve<IMenuQuery>();
            _pageQuery = ObjectContainer.Resolve<IPageQuery>();
            _functionQuery = ObjectContainer.Resolve<IFunctionQuery>();
        }


        [HttpGet]
        [Priviledge(true, "/system/page/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1, "/system/page/getmenupage")]
        public IActionResult GetMenuPage(int pageIndex=1,int pageSize=20)
        {
            return Json(_menuQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2, "/system/page/createnewmenu")]
        public IActionResult CreateNewMenu(CreateNewMenuCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }

        [HttpGet]
        [Priviledge(true, "/system/page/page")]
        public IActionResult Page()
        {
            ViewBag.MenuId=HttpContext.Request.Query["menuId"];
            return View();
        }

        [HttpGet]
        [Priviledge(4, "/system/page/getpagepage")]
        public IActionResult GetPagePage(Guid menuId, int pageIndex = 1, int pageSize = 20)
        {
            return Json(_pageQuery.GetPage(menuId,pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(8, "/system/page/createnewpage")]
        public IActionResult CreateNewPage(CreateNewPageCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }

        [HttpGet]
        [Priviledge(true, "/system/page/function")]
        public IActionResult Function()
        {
            ViewBag.PageId = HttpContext.Request.Query["pageId"];
            return View();
        }

        [HttpGet]
        [Priviledge(16, "/system/page/getfunctionpage")]
        public IActionResult GetFunctionPage(Guid pageId,int pageIndex=1,int pageSize=20)
        {
            return Json(_functionQuery.GetPage(pageId, pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(32, "/system/page/createnewfunction")]
        public IActionResult CreateNewFunction(CreateNewFunctionCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }
    }
}
