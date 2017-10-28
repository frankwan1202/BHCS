using BHCS.Infrastructure.Common;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Querying.Querying;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Web.Controllers
{
    public class CommonController:BaseController
    {
        public const string LoginValidateCode_SessionName = "LoginValidateCode";

        private readonly IRoleQuery _roleQuery;
        private readonly IMajorQuery _majorQuery;
        private readonly IGradeQuery _gradeQuery;
        private readonly IClassesQuery _classesQuery;
        private readonly IMenuQuery _menuQuery;
        private readonly ICourseNatureQuery _courseNatureQuery;
        private readonly IClassBuildingQuery _classBuildingQuery;

        public CommonController()
        {
            _roleQuery = ObjectContainer.Resolve<IRoleQuery>();
            _majorQuery = ObjectContainer.Resolve<IMajorQuery>();
            _gradeQuery = ObjectContainer.Resolve<IGradeQuery>();
            _classesQuery = ObjectContainer.Resolve<IClassesQuery>();
            _menuQuery = ObjectContainer.Resolve<IMenuQuery>();
            _courseNatureQuery = ObjectContainer.Resolve<ICourseNatureQuery>();
            _classBuildingQuery = ObjectContainer.Resolve<IClassBuildingQuery>();
        }

        [HttpGet]
        public IActionResult GetRoleList()
        {
            return Json(_roleQuery.GetRoleList());
        }

        [HttpGet]
        public IActionResult GetMajorList()
        {
            return Json(_majorQuery.GetMajorList());
        }

        [HttpGet]
        public IActionResult GetGradeList()
        {
            return Json(_gradeQuery.GetList());
        }

        [HttpGet]
        public IActionResult GetClassesList()
        {
            return Json(_classesQuery.GetList());
        }

        [HttpGet]
        public IActionResult GetVierificationCode()
        {
            string code = "";
            System.IO.MemoryStream ms = VierificationCodeServices.Create(out code);
            HttpContext.Session.SetString(LoginValidateCode_SessionName, code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }

        [HttpGet]
        public IActionResult GetMenuList()
        {
            return Json(_menuQuery.GetList());
        }

        [HttpGet]
        public IActionResult GetCourseNatureList()
        {
            return Json(_courseNatureQuery.GetList());
        }

        [HttpGet]
        public IActionResult GetClassBuildingList()
        {
            return Json(_classBuildingQuery.GetList());
        }
    }
}
