using _4roomforum.DTOs;
using _4roomforum.Models;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
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
                    return View();
                }

                // Lấy người đăng bài viết
                var PostedBy = (UserDTO)await _userService.GetUserProfile(post.PostedBy);
                if (PostedBy == null)
                {
                    TempData["Message"] = "User who posted not found.";
                    return View();
                }

                var replies = (PagedResult<ReplyDTO>?)await _replyService.GetAllReplies(PostId, page, pageSize);
                if (replies == null)
                {
                    TempData["Message"] = "Replies not found.";
                    return View();
                }

                var isLiked = await _postService.CheckLikeStatus(PostId, int.Parse(userId)); 
                ViewBag.IsLikedByUser = isLiked;

                // Lấy thông tin người dùng 
                ViewBag.GetUserReply = new Func<int, Task<UserDTO>>(async x => await _userService.GetUserProfile(x));

                //Hàm lấy reply theo id
                ViewBag.MyFunction = new Func<int, Task<ReplyDTO>>(async x => await _replyService.GetAReply(x));

                //Hàm lấy số like theo reply id
                ViewBag.GetLikeReply = new Func<int, int>(x =>
                {
                    // Gọi bất đồng bộ trước và trả về số lượng phần tử
                    var reactions = _replyService.GetAllReaction(x).Result; // .Result để chờ kết quả
                    return reactions.Where(r => r.Vote == 1).Count();
                });

                //Hàm lấy số dislike theo reply id
                ViewBag.GetDisLikeReply = new Func<int, int>(x =>
                {
                    // Gọi bất đồng bộ trước và trả về số lượng phần tử
                    var reactions = _replyService.GetAllReaction(x).Result; // .Result để chờ kết quả
                    return reactions.Where(r => r.Vote == -1).Count();
                });

                //Hàm kiểm tra user đã like hay dislike bài viết
                ViewBag.CheckReaction = new Func<(int replyId, int userId), Task<String>>(async tuple =>
                {
                    var reactions = await _replyService.GetAllReaction(tuple.replyId);
                    if(reactions.Any(r => r.UserId == tuple.userId && r.Vote == 1))
                    {
                        return "Like";
                    }
                    if (reactions.Any(r => r.UserId == tuple.userId && r.Vote == -1))
                    {
                        return "Dislike";
                    }
                    return "No selection";
                });

                // Gán dữ liệu vào ViewBag
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = replies.TotalPages;
                ViewBag.PaginationReplies = replies.Items;
                ViewBag.TotalReplies = replies.TotalCount;
                ViewBag.PageSize = pageSize;

                ViewBag.Post = post;
                ViewBag.TotalLike = post.Like;
                ViewBag.UserPost = PostedBy;

                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                    var UserRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    ViewBag.UserId = int.Parse(userId);
                    ViewBag.UserRole = int.Parse(UserRole);
                    ViewBag.LoginStatus = true;
                }
                else
                {
                    ViewBag.UserId = null;
                    ViewBag.UserRole = null;
                    ViewBag.LoginStatus = false;
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while processing your request. " + ex.Message;
                return View();
            }
        }

        [Authorize]
        public async Task<ActionResult> Create(CreateReplyDTO createReplyDTO)
        {
            try
            {
                bool success = await _replyService.CreateReply(createReplyDTO);

                if (success)
                {
                    // Optionally, you can show a success message or redirect to another page
                    TempData["Message"] = "Reply created successfully!";

                    var replies = (PagedResult<ReplyDTO>?)await _replyService.GetAllReplies(createReplyDTO.PostId, 1, 1);
                    int TotalReplies = replies.TotalCount;
                    int PageSize = 5;
                    int newReplyPage = (int)Math.Ceiling(TotalReplies / (double)PageSize);
                    return RedirectToAction("Index", new { PostId = createReplyDTO.PostId, page = newReplyPage });
                }
                else
                {
                    // If reply creation fails, show an error message
                    TempData["Message"] = "Reply delete failed.";
                    return RedirectToAction("Index", new { PostId = createReplyDTO.PostId });
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while processing your request. " + ex.Message;
                return RedirectToAction("Index", new { PostId = createReplyDTO.PostId });
            }
        }


        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                //These codes is temporary, you can replace it when socket codes are available!!!
                var Reply = (ReplyDTO)await _replyService.GetAReply(id);
                var Post = (PostDTO)await _postService.GetPostById(Reply.PostId);

                bool success = await _replyService.DeleteReply(id);
                if (success)
                {
                    // Optionally, you can show a success message or redirect to another page
                    TempData["Message"] = "Reply delete successfully!";
                    return RedirectToAction("Index", new { PostId = Post.Id });
                }
                else
                {
                    // If reply creation fails, show an error message
                    TempData["Message"] = "Reply delete failed.";
                    return RedirectToAction("Index", new { PostId = Post.Id });
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while processing your request. " + ex.Message;
                return RedirectToAction("Index", new { PostId = 1 });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateReplyDTO updateReplyDTO)
        {
            try
            {
                bool check = await _replyService.UpdateReply(id, updateReplyDTO);
                if (!check) {
                    return Json(new { success = false, message = "Invalid data." });
                }
                return Json(new { success = true, message = "Reply updated successfully."
                    , updatedContent = updateReplyDTO.ReplyContent, isEdited = true
                });               
            }
            catch (Exception ex) {
                return Json(new { success = false, message = $"Something went wrong! Info: {ex.Message}" });
            }
        }

        public async Task<ActionResult> React(int replyId, int userId, int vote)
        {
            try
            {
                bool check = await _replyService.ReactToReply(replyId, userId, vote);
                if (!check)
                {
                    return Json(new { success = false, message = "Invalid data." });
                }

                var AllReaction = (IEnumerable<LikeOfReplyDTO>)await _replyService.GetAllReaction(replyId);
                int CountLike = AllReaction.Where(r => r.Vote == 1).Count();
                Console.WriteLine("so like: " + CountLike);
                int CountDislike = AllReaction.Where(r => r.Vote == -1).Count();
                Console.WriteLine("so dislike: " + CountDislike);
                return Json(new
                {
                    success = true,
                    react_type = vote,
                    like = CountLike,
                    dis_like = CountDislike,
                    message = "React to reply successfully!"
                });
            }
            catch (Exception ex) {
                return Json(new { success = false, message = $"Something went wrong! Info: {ex.Message}" });
            }
        }
    }
}
