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
        private readonly ITeachingPlanCourseQuery _teachingPlanCourseQuery;

        public TeachingPlanController()
        {
            _teachingPlanQuery = ObjectContainer.Resolve<ITeachingPlanQuery>();
            _teachingPlanCourseQuery = ObjectContainer.Resolve<ITeachingPlanCourseQuery>();
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
        [Priviledge(true, "/schedule/teachingplan/acceptcourse")]
        public IActionResult AcceptCourse(Guid planId)
        {
            ViewBag.TeachingPlan = _teachingPlanQuery.Get(planId);
            ViewBag.CourseList = _teachingPlanCourseQuery.GetList(planId);
            return View();
        }

        [HttpGet]
        [Priviledge(4,"/schedule/teachingplan/getacceptcourselist")]
        public IActionResult GetAcceptCourseList(Guid planId)
        {
            return Json(_teachingPlanCourseQuery.GetList(planId));
        }
    }
}
