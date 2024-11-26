using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace _4roomforum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
        public ActionResult Dashboard(){
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
        public ActionResult Users(){
            return View();
        }
    }
}
