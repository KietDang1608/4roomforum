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
                ViewBag.ThreadId = id;
                ViewBag.TotalPages = posts.TotalPages;
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
        [HttpPost]
        public async Task<ActionResult> AddPost(CreatePostDTO postDTO)
        {
            try
            {
                bool check = await _postService.CreatePostAsync(postDTO);
                if (check)
                {
                    TempData["SuccessMessage"] = "Đăng bài thành công :3";
                    return RedirectToAction("Index", new {Id = postDTO.ThreadId});
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra. Vui lòng thử lại.";
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi: " + ex.Message;
                return RedirectToAction("Create");
            }
        }
    }
}
