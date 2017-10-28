using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BHCS.Querying.Querying;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Application.Commanding;
using BHCS.Web.Controllers;
using BHCS.Web.Filters;

namespace BHCS.Web.Areas.System.Controllers
{
    public class ClassesController : SystemBaseController
    {
        private readonly IClassesQuery _classesQuery;

        public ClassesController()
        {
            _classesQuery = ObjectContainer.Resolve<IClassesQuery>();
        }

        [HttpGet]
        [Priviledge(true, "/system/classes/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1, "/system/classes/getclassespage")]
        public IActionResult GetClassesPage(int pageIndex = 1, int pageSize = 20)
        {
            return Json(_classesQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2, "/system/classes/createnewclasses")]
        public IActionResult CreateNewClasses(CreateNewClassesCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }
    }
}