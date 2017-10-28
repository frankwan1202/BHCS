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
    public class CourseController:SystemBaseController
    {
        private readonly ICourseQuery _courseQuery;
        private readonly ICourseNatureQuery _courseNatureQuery;

        public CourseController()
        {
            _courseQuery = ObjectContainer.Resolve<ICourseQuery>();
            _courseNatureQuery = ObjectContainer.Resolve<ICourseNatureQuery>();
        }

        [HttpGet]
        [Priviledge(true, "/system/course/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1, "/system/course/getcoursepage")]
        public IActionResult GetCoursePage(int pageIndex=1,int pageSize=20)
        {
            return Json(_courseQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2, "/system/course/createnewcourse")]
        public IActionResult CreateNewCourse(CreateNewCourseCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }

        [HttpGet]
        [Priviledge(true, "/system/course/nature")]
        public IActionResult Nature()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(4, "/system/course/getnaturepage")]
        public IActionResult GetNaturePage(int pageIndex=1,int pageSize=20)
        {
            return Json(_courseNatureQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(8, "/system/course/createnewcoursenature")]
        public IActionResult CreateNewCourseNature(CreateNewCourseNatureCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }
    }
}
