using _4roomforum.DTOs;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
                    ViewBag.Message = "Post not found.";
                    return View();
                }

                // Lấy người đăng bài viết
                var PostedBy = (UserDTO)await _userService.GetUserProfile(post.PostedBy);
                if (PostedBy == null)
                {
                    ViewBag.Message = "User who posted not found.";
                    return View();
                }

                var replies = await _replyService.GetAllReplies(PostId);
                if (replies == null)
                {
                    ViewBag.Message = "Replies not found.";
                    return View();
                }
                var TotalReplies = replies.Count();
                int TotalPages = (int)Math.Ceiling((double)TotalReplies / pageSize);
                var PaginationReplies = replies
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Lấy thông tin người dùng từ danh sách replies
                List<Task<UserDTO>> userTasks = PaginationReplies.Select(r => _userService.GetUserProfile(r.RepliedBy)).ToList();
                var UsersReply = await Task.WhenAll(userTasks);

                // Gán dữ liệu vào ViewBag
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = TotalPages;
                ViewBag.PaginationReplies = PaginationReplies;
                ViewBag.AllReplies = PaginationReplies;
                ViewBag.TotalReplies = TotalReplies;

                ViewBag.Post = post;
                ViewBag.Users = UsersReply;
                ViewBag.UserPost = PostedBy;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred while processing your request.";
                return View();
            }
        }

    }
}
