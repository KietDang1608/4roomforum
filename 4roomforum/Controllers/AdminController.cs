using Microsoft.AspNetCore.Mvc;

namespace _4roomforum.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            return View("Dashboard");
        }
        public ActionResult Dashboard(){
            return View();
        }
        public ActionResult Users(){
            return View();
        }
    }
}
