using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _4roomforum.Services.Interfaces;
using _4roomforum.DTOs;

namespace _4roomforum.Controllers
{
    [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        // GET: AdminController
        private readonly IPostService _postService;
        public AdminController (IPostService postService)
        {
            _postService=postService;
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                if (posts == null)
                {
                    ViewBag.ChartLabels = new List<string>();
                    ViewBag.ChartData = new List<int>();
                    ViewBag.ChartTitles = new List<string>();
                    return View(new List<PostDTO>());
                }

            var topPostsByMonth = posts
                    .GroupBy(post => post.PostDate.ToString("MM/yyyy")) // Nhóm theo tháng/năm
                    .Select(group => group.OrderByDescending(p => p.Like).FirstOrDefault()) // Lấy bài viết có like cao nhất mỗi nhóm
                    .Where(post => post != null)
                    .OrderBy(post => DateTime.ParseExact(post.PostDate.ToString("MM/yyyy"), "MM/yyyy", null)) // Sắp xếp theo thời gian
                    .ToList();

                ViewBag.ChartLabels = topPostsByMonth.Select(p => p.PostDate.ToString("MM/yyyy")).ToList();
                ViewBag.ChartData = topPostsByMonth.Select(p => p.Like).ToList();
                ViewBag.ChartTitles = topPostsByMonth.Select(p => p.PostTitle).ToList();

                return View("~/Views/Admin/Dashboard/Index.cshtml",topPostsByMonth);
            }
            catch (Exception ex)
            {
                ViewBag.ChartLabels = new List<string>();
                ViewBag.ChartData = new List<int>();
                ViewBag.ChartTitles = new List<string>();

                return View("~/Views/Admin/Dashboard/Index.cshtml",new List<PostDTO>());
            }
            // return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
        public ActionResult Dashboard(){
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
        public ActionResult Users(){
            return View();
        }
        
        //  [HttpGet("Admin/ThongKe")]
        // [AllowAnonymous]
        // public async Task<ActionResult> GetAllPostsAsync()
        // {
            
        // }

    }
}
