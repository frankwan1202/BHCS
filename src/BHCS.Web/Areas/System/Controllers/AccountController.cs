using BHCS.Application.Commanding;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Querying.Querying;
using BHCS.Web.Controllers;
using BHCS.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BHCS.Web.Areas.System.Controllers
{
    public class AccountController : SystemBaseController
    {
        private readonly ITeacherQuery _teacherQuery;
        private readonly IStudentQuery _studentQuery;

        public AccountController()
        {
            _teacherQuery = ObjectContainer.Resolve<ITeacherQuery>();
            _studentQuery = ObjectContainer.Resolve<IStudentQuery>();
        }

        [HttpGet]
        [Priviledge(true,"/system/account/index")]
        public ActionResult Teacher()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(1, "/system/account/getteacherlist")]
        public IActionResult GetTeacherList(int pageIndex = 1, int pageSize = 20)
        {
            return Json(_teacherQuery.GetTeacherPage(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(2, "/system/account/createnewteacher")]
        public IActionResult CreateNewTeacher(CreateNewTeacherCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }

        [HttpGet]
        [Priviledge(true, "/system/account/student")]
        public ActionResult Student()
        {
            return View();
        }

        [HttpGet]
        [Priviledge(4, "/system/account/getstudenpage")]
        public IActionResult GetStudentPage(int pageIndex = 1, int pageSize = 20)
        {
            return Json(_studentQuery.GetPaged(pageIndex, pageSize));
        }

        [HttpPost]
        [Priviledge(8, "/system/account/createnewstudent")]
        public IActionResult CreateNewStudent(CreateNewStudentCommand command)
        {
            return Json(CommandBus.Send(command).ToResult());
        }
    }
}