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
        private readonly IUserService _userService;
        public PostController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }
        // GET: PostController (api/post)
        [HttpGet("Post/{id}/{userId}")]
        public async Task<ActionResult> Index(int id, int userId, int page = 1)
        {
            try
            {
                var posts = await _postService.GetPostsByThreadId(id, userId, page);

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
                    return RedirectToAction("Index", new {Id = postDTO.ThreadId, userId = postDTO.PostedBy});
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

        [HttpPut("Post/Create")]
        public void create()
        {

        }

        [HttpPut("Post/{postId}/{threadId}/{userId}")]
        public async Task<ActionResult> UpdatePost(int postId, int threadId, int userId, [FromBody] UpdatePostDTO postDTO)
        {
            try
            {
                bool check = await _postService.UpdatePostAsync(postId, postDTO);
                if (check)
                {
                    return Json(new { success = true, message = "Cập nhật thành công!", title = postDTO.PostTitle, content = postDTO.PostContent });
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

        [HttpPost("Post/{id}/{threadId}/{userId}")]
        public async Task<ActionResult> DeletePost(int id, int threadId, int userId)
        {
            try
            {
                bool check = await _postService.DeletePostAsync(id);
                if (check)
                {
                    TempData["SuccessMessage"] = "Xóa bài thành công :3";
                    return RedirectToAction("Index", new { Id = threadId, userId = userId });
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
