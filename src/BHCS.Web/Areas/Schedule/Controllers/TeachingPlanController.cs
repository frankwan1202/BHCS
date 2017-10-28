using BHCS.Application.Commanding;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Querying.Querying;
using BHCS.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Web.Areas.Schedule.Controllers
{
    public class TeachingPlanController:ScheduleBaseController
    {
        private readonly ITeachingPlanQuery _teachingPlanQuery;

        public TeachingPlanController()
        {
            _teachingPlanQuery = ObjectContainer.Resolve<ITeachingPlanQuery>();
        }

        [HttpGet]
        [Priviledge(true,"/schedule/teachingplan/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1,"/schedule/teachingplan/getteachingplanpage")]
        public IActionResult GetTeachingPlanPage(int pageIndex=1,int pageSize=20)
        {
            return Json(_teachingPlanQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2,"/schedule/teachingplan/createnewteachingplan")]
        public IActionResult CreateNewTeachingPlan(CreateNewTeachingPlanCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }

        [HttpGet]
        [Priviledge(true,"/schedule/teachingplan/accept")]
        public IActionResult Accept()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(4,"/schedule/teachingplan/getacceptpage")]
        public IActionResult GetAcceptPage(int pageIndex=1,int pageSize=20)
        {
            return Json(_teachingPlanQuery.GetPage(pageIndex, pageSize,false));
        }

        //[HttpGet]
        //[Priviledge(true, "/schedule/teachingplan/acceptcourse")]
        //public IActionResult AcceptCourse(Guid planId);

    }
}
