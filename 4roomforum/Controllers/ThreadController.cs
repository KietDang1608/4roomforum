using _4roomforum.DTOs;
using _4roomforum.Models;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _4roomforum.Controllers
{
    public class ThreadController : Controller
    {

        private readonly IThreadService _threadService;

        public ThreadController(IThreadService threadService)
        {
            _threadService = threadService;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var hotThread = await _threadService.GetHotThreads();
                ViewBag.HotThreads = hotThread;
                var threads = await _threadService.GetAllThreads();
                ViewBag.Threads = threads;


                return View();
            }
            catch (Exception ex)
            {
                return View(new List<ThreadDTO>());
            }
        }
        [HttpGet("Thread/{id}")]
        public async Task<ActionResult> ThreadDetail(int id)
        {
            var thread = await _threadService.GetThreadById(id);
            ViewBag.Thread = thread;
            if (thread != null)
            {
                return View(thread);
            }
            return NotFound();


        }

    }
}