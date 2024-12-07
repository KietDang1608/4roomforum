using _4roomforum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using _4roomforum.Services.Interfaces;

namespace _4roomforum.Controllers
{
    [Authorize(Roles = "1")]
    public class AdminThreadsController : Controller
    {
        private readonly IThreadService _threadService;

        public AdminThreadsController(IThreadService threadService)
        {
            _threadService = threadService;
        }
        public async Task<ActionResult> Index()
        {
            var thread = await _threadService.GetAllThreads();
            return View("~/Views/Admin/threads.cshtml", thread);
        }
        
    }
}

