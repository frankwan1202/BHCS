using BHCS.Application.Commanding;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Querying.Querying;
using BHCS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Web.Areas.System.Controllers
{
    public class ClassRoomController:SystemBaseController
    {
        private readonly IClassRoomQuery _classRoomQuery;

        public ClassRoomController()
        {
            _classRoomQuery = ObjectContainer.Resolve<IClassRoomQuery>();
        }

        [HttpGet]
        [Priviledge(true, "/system/classroom/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1, "/system/classRoom/getroompage")]
        public IActionResult GetRoomPage(int pageIndex = 1, int pageSize = 20)
        {
            return Json(_classRoomQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2, "/system/classRoom/createnewroom")]
        public IActionResult CreateNewRoom(CreateNewClassRoomCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }
    }
}
