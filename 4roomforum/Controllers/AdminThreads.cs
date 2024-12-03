using _4roomforum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace _4roomforum.Controllers
{
    [Authorize(Roles = "1")]
    public class AdminThreads : Controller
    {
        private ILogger<HomeController> _logger;

        public AdminThreads(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<ActionResult> Index()
        {
            var thread = getAllThread();
            return View("~/Views/Admin/threads.cshtml", thread);
        }
        private List<Threads> getAllThread()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    var reponseTask = client.GetAsync("api/thread");
                    reponseTask.Wait();

                    var result = reponseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var threads = result.Content.ReadFromJsonAsync<IList<Threads>>().Result;
                        if (threads == null)
                        {
                            ViewBag.Message = "No thread";
                        }
                        return threads?.ToList() ?? new List<Threads>();
                    }
                    else
                    {
                        _logger.LogError("Server error. Please contact administrator.");
                        return new List<Threads>();
                    }
                }
                catch (AggregateException ex)
                {
                    ViewBag.Message = "Thread service is not available";
                    return new List<Threads>();
                }
                catch (HttpRequestException ex)
                {
                    ViewBag.Message = "Thread service is not available";
                    return new List<Threads>();
                }
            }
        }
    }
}

