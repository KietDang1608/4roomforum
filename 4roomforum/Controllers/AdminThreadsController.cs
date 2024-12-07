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
        
        public IActionResult UpdateThread(int id)
        {
            var thread = getThreadById(id);
            return View("~/Views/Admin/UpdateThread.cshtml",thread);
        }
        private Threads getThreadById(int id)
        {
            try{
                var thread = _threadService.getThreadById(id);
                return thread;
            }catch(Exception ex){
                return new Threads();
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateThread(Threads thread)
        {
            if (ModelState.IsValid)
            {
                bool issSuccess = await _threadService.EditThread(thread);
                if (issSuccess)
                {
                    ViewBag.Message = "Thread edit successfully!";
                    var threads = await _threadService.GetAllThreads();
                    return View("~/Views/Admin/threads.cshtml", threads);
                }
                else
                {
                    ViewBag.Message = "Edit Thread Errorr!";
                    var threads = await _threadService.GetAllThreads();
                    return View("~/Views/Admin/threads.cshtml", threads);
                }
            }  
            var threads1 = await _threadService.GetAllThreads();
            ViewBag.Message = "Error while  edit Thread!";
            return View("~/Views/Admin/threads.cshtml", threads1);
        }
    }
}

