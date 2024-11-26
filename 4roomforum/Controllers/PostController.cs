using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using _4roomforum.DTOs;
using Microsoft.AspNetCore.Authorization;
//using PostService.DTOs;

namespace _4roomforum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        // GET: PostController (api/post)
        [HttpGet("Post/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Index(int id, int page = 1, int pageSize = 5)
        {
            try
            {
                var posts = await _postService.GetPostsByThreadId(id, page, pageSize);


                if (posts == null || !posts.Items.Any())
                {
                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = 0;
                    ViewBag.PaginationPosts = new List<PostDTO>();
                    ViewBag.TotalPosts = 0;

                    return View(new List<PostDTO>());
                }

                ViewBag.CurrentPage = posts.CurrentPage;
                ViewBag.TotalPages = (int)Math.Ceiling((double)posts.TotalCount / posts.PageSize);
                ViewBag.PaginationPosts = posts.Items;
                ViewBag.TotalPosts = posts.TotalCount;

                return View(posts.Items);
            }
            catch (Exception ex)
            {
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = 0;
                ViewBag.PaginationPosts = new List<PostDTO>();
                ViewBag.TotalPosts = 0;

                return View(new List<PostDTO>());
            }
        }
        [HttpGet("Post/CreatePost")]
        [Authorize]
        public IActionResult CreatePost()
        {
            return View();
        }
    }
}
