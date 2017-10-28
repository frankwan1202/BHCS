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
    public class ClassBuildingController:SystemBaseController
    {
        private readonly IClassBuildingQuery _classBuildingQuery;

        public ClassBuildingController()
        {
            _classBuildingQuery = ObjectContainer.Resolve<IClassBuildingQuery>();
        }

        [HttpGet]
        [Priviledge(true,"/system/classbuilding/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1,"/system/classbuilding/getbuildingpage")]
        public IActionResult GetBuildingPage(int pageIndex=1,int pageSize=20)
        {
            return Json(_classBuildingQuery.GetPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2,"/system/classbuilding/createnewbuilding")]
        public IActionResult CreateNewBuilding(CreateNewClassBuildingCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }
    }
}
