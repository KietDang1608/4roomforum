using Microsoft.AspNetCore.Mvc;
using _4roomforum.Models;

namespace _4roomforum.Controllers
{
    [Route("admin/[controller]")]
    public class AdminUsersController : Controller
    
    {
        private readonly ILogger<HomeController> _logger;

        public AdminUsersController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: AdminUsersController
        public async Task<ActionResult> Index()
        {
            var users = GetAllUser();
            
            return View("~/Views/Admin/Users/Index.cshtml",users);
        }
        private List<User> GetAllUser(){
           
            using (var client = new HttpClient())
            {   
                try{
                    client.BaseAddress = new Uri("http://localhost:5001/");
                    var responseTask = client.GetAsync("api/user");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var users = result.Content.ReadFromJsonAsync<IList<User>>().Result;
                        if (users == null){
                            ViewBag.Message = "No user found";
                        }
                        return users?.ToList() ?? new List<User>();
                    }
                    else
                    {
                        _logger.LogError("Server error. Please contact administrator.");
                        return new List<User>();
                    }
                }catch(AggregateException ex){
                    ViewBag.Message = "User service is not available";
                    return new List<User>();
                }catch(HttpRequestException ex){
                    ViewBag.Message = "User service is not available";
                    return new List<User>();
                }
            }
        }
    }
    
}
