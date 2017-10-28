using BHCS.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BHCS.Web.Controllers
{
    public class HomeController:Controller
    {
        [HttpGet]
        [Priviledge(true,"/home/index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}