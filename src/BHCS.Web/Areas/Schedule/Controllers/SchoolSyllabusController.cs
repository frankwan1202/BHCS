using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Web.Areas.Schedule.Controllers
{
    public class SchoolSyllabusController:ScheduleBaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSyllabusPage(int pageIndex=1,int pageSize=100)
        {
            return View();
        }
    }
}
