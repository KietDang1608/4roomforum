using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using _4roomforum.DTOs;
//using PostService.DTOs;

namespace _4roomforum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController (IPostService postService)
        {
            _postService = postService;
        }
        // GET: PostController (api/post)
        public async Task<ActionResult> Index()
        {
            try
            {
                var posts = await _postService.GetAllPost();
                ViewBag.Posts = posts;
                return View();
            }
            catch (Exception ex)
            {
                return View(new List<PostDTO>());
            }
        }

    }
}
