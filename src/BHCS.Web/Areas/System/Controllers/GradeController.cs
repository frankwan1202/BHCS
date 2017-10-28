using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BHCS.Querying.Querying;
using BHCS.Application.Commanding;
using BHCS.Web.Controllers;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Web.Filters;

namespace BHCS.Web.Areas.System.Controllers
{
    public class GradeController : SystemBaseController
    {
        private readonly IGradeQuery _gradeQuery;

        public GradeController()
        {
            _gradeQuery = ObjectContainer.Resolve< IGradeQuery>();
        }

        [HttpGet]
        [Priviledge(true, "/system/grade/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1, "/system/grade/getgradepage")]
        public IActionResult GetGradePage(int pageIndex = 1,int pageSize=20)
        {
            return Json(_gradeQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2, "/system/grade/createnewgrade")]
        public IActionResult CreateNewGrade(CreateNewGradeCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }
    }
}