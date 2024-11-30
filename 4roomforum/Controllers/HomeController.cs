using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _4roomforum.Models;
using System.Net.Http.Json;
using _4roomforum.DTOs;
using _4roomforum.Services.Interfaces;

namespace _4roomforum.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryService _categoryService;
        private IPostService _postService;
        private IThreadService _threadService;

        public HomeController(ICategoryService categoryService, IPostService postService, IThreadService threadService)
        {
            _categoryService = categoryService;
            _postService = postService;
            _threadService = threadService;
        }
        
        
        // public void GetAllUser()
        // {
        //     using (var client = new HttpClient())
        //     {
        //         client.BaseAddress = new Uri("http://localhost:5043/");
        //         var responseTask = client.GetAsync("api/user");
        //         responseTask.Wait();

        //         var result = responseTask.Result;
        //         if (result.IsSuccessStatusCode)
        //         {
        //             var users = result.Content.ReadFromJsonAsync<IList<User>>().Result;
        //             ViewBag.Users = users;
        //         }
        //         else
        //         {
        //             ViewBag.Users = new List<User>();
        //             _logger.LogError("Server error. Please contact administrator.");
        //         }
        //     }
        // }

        public async Task<IActionResult> Index()
        {
            try{
                var categories = await _categoryService.GetAllCategory();
                ViewBag.Categories = categories;
                var hotThread = await _threadService.GetHotThreads();
                ViewBag.HotThreads = hotThread;
                var posts = await _postService.GetAllPostsAsync();
                ViewBag.Posts = posts;
            }
            catch (Exception ex)
            {
                return View(new List<CategoryDTO>());
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
