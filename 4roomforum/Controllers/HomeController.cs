using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _4roomforum.Models;
using System.Net.Http.Json;

namespace _4roomforum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public void GetAllUser()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5043/");
                var responseTask = client.GetAsync("api/user");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var users = result.Content.ReadFromJsonAsync<IList<User>>().Result;
                    ViewBag.Users = users;
                }
                else
                {
                    ViewBag.Users = new List<User>();
                    _logger.LogError("Server error. Please contact administrator.");
                }
            }
        }

        public async Task<IActionResult> Index()
        {
            // using (var client = new HttpClient())
            // {
            //     client.BaseAddress = new Uri("http://localhost:5000/");
            //     var responseTask = client.GetAsync("api/category");
            //     responseTask.Wait();

            //     var result = responseTask.Result;
            //     if (result.IsSuccessStatusCode)
            //     {
            //         var categories = result.Content.ReadFromJsonAsync<IList<Category>>().Result;
            //         ViewBag.Categories = categories;
            //     }
            //     else
            //     {
            //         ViewBag.Categories = new List<Category>();
            //         _logger.LogError("Server error. Please contact administrator.");
            //     }
            // }
            // GetAllUser();
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
    }
}
