using _4roomforum.DTOs;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostService.DTOs;
namespace _4roomforum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IReplyService _replyService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public ReplyController(IReplyService replyService, IUserService userService, IPostService postService)
        {
            _replyService = replyService;
            _userService = userService;
            _postService = postService;
        }

        public async Task<ActionResult> Index(int PostId, int page = 1, int pageSize = 5)
        {
            try
            {
                var post = (PostDTO)await _postService.GetPostById(PostId);
                if (post == null)
                {
                    TempData["Message"] = "Post not found.";
                    return View("~/Views/Shared/Error.cshtml");
                }

                var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                // Lấy người đăng bài viết
                var PostedBy = (UserDTO)await _userService.GetUserProfile(post.PostedBy);
                if (PostedBy == null)
                {
                    TempData["Message"] = "User who posted not found.";
                    return View("~/Views/Shared/Error.cshtml");
                }

                var replies = (PagedResult<ReplyDTO>?)await _replyService.GetAllReplies(PostId, page, pageSize);
                if (replies == null)
                {
                    TempData["Message"] = "Replies not found.";
                    return View("~/Views/Shared/Error.cshtml");
                }

                var isLiked = await _postService.CheckLikeStatus(PostId, int.Parse(userId)); 
                ViewBag.IsLikedByUser = isLiked;
                // Lấy thông tin người dùng 
                ViewBag.GetUserReply = new Func<int, Task<UserDTO>>(async x => await _userService.GetUserProfile(x));

                //Hàm lấy reply theo id
                ViewBag.MyFunction = new Func<int, Task<ReplyDTO>>(async x => await _replyService.GetAReply(x));

                // Gán dữ liệu vào ViewBag
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = replies.TotalPages;
                ViewBag.PaginationReplies = replies.Items;
                ViewBag.TotalReplies = replies.TotalCount;
                ViewBag.PageSize = pageSize;

                ViewBag.Post = post;
                ViewBag.TotalLike = post.Like;
                ViewBag.UserPost = PostedBy;

                ViewBag.UserId = userId;

                return View();
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while processing your request. " + ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public async Task<ActionResult> Create(CreateReplyDTO createReplyDTO)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    bool success = await _replyService.CreateReply(createReplyDTO);

                    if (success)
                    {
                        // Optionally, you can show a success message or redirect to another page
                        TempData["Message"] = "Reply created successfully!";
                        return RedirectToAction("Index", new { PostId = createReplyDTO.PostId });
                    }
                    else
                    {
                        // If reply creation fails, show an error message
                        TempData["Message"] = "An error occurred while processing your request.";
                        return RedirectToAction("Index", new { PostId = createReplyDTO.PostId });
                    }
                }
                return RedirectToAction("SignIn", "Login");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while processing your request. " + ex.Message;
                return RedirectToAction("Index", new { PostId = createReplyDTO.PostId });
            }
        }

    }
}
