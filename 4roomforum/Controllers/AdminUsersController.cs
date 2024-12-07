using Microsoft.AspNetCore.Mvc;
using _4roomforum.DTOs;
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
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            ViewBag.Users = users;

            return View("~/Views/Admin/Users/Index.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Search(string searchData)
        {
            var users = await _userService.GetAllUsers();
            var searchUsers = new List<UserDTO>();
            foreach (var user in users)
            {
                try
                {
                    if (user.Email.Contains(searchData))
                    {
                        searchUsers.Add(user);
                    }
                    else if (user.UserName.Contains(searchData))
                    {
                        searchUsers.Add(user);
                    }
                    else if (user.UserId.ToString().Contains(searchData))
                    {
                        searchUsers.Add(user);
                    }
                    else if (user.RoleId.ToString().Contains(searchData))
                    {
                        searchUsers.Add(user);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    return RedirectToAction("Index");
                }

            }
            ViewBag.Users = searchUsers;
            return View("~/Views/Admin/Users/Index.cshtml");
        }

    }
    
}
