using Microsoft.AspNetCore.Mvc;
using _4roomforum.Models;
using Microsoft.AspNetCore.Authorization;
using _4roomforum.Services.Interfaces;

namespace _4roomforum.Controllers
{
    [Route("admin/[controller]")]
    [Authorize(Roles="1")]
    public class AdminUsersController : Controller
    
    {
        private readonly IUserService _userService;

        public AdminUsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: AdminUsersController
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            ViewBag.Users = users;

            return View("~/Views/Admin/Users/Index.cshtml");
        }
        

        
    }
    
}
