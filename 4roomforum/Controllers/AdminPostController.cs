using _4roomforum.DTOs;
using _4roomforum.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostService.DTOs;

namespace _4roomforum.Controllers
{
    public class AdminPostController(IPostService postService, IUserService userService, IReplyService replyService) : Controller
    {

        readonly IPostService _postService = postService;
        readonly IUserService _userService = userService;
        readonly IReplyService _replyService = replyService;

        // GET: AdminPostController
        public async Task<IActionResult> Index()
        {
            //Get all posts
            var posts = await _postService.GetAllPostsAsync();
            List<PostDetailDTO> postDetails = new List<PostDetailDTO>();
            foreach (var post in posts)
            {
                PostDetailDTO postDetail = new PostDetailDTO();
                postDetail.ThreadId = post.ThreadId;
                postDetail.PostId = post.Id;
                postDetail.Content = post.PostContent;
                postDetail.CreatedAt = post.PostDate;
                postDetail.Author = await _userService.GetUserProfile(post.PostedBy);
                var replies = await _replyService.GetAllReplies(post.Id, 1, 10);
                if (replies == null || !replies.Items.Any())
                {
                    postDetail.Comments = new List<ReplyDTO>();
                    postDetails.Add(postDetail);
                    continue;
                }
                else{
                //Get all replies of post
                foreach(var reply in replies.Items)
                {
                    ReplyDTO rep = new ReplyDTO();
                    rep.ReplyId = reply.ReplyId;
                    rep.ReplyContent = reply.ReplyContent;
                    var userProfile = await _userService.GetUserProfile(reply.RepliedBy);
                    rep.RepliedBy = userProfile.UserId;
                    
                    postDetail.Comments.Add(rep);
                }
                postDetails.Add(postDetail);
                }
            }

            if (posts == null || !posts.Any())
            {
                ViewBag.ListPostDetails = new List<PostDetailDTO>();
                return View("/Views/Admin/Posts/Index.cshtml");
            }
            ViewBag.ListPostDetails = postDetails;
            return View("/Views/Admin/Posts/Index.cshtml");
        }


        // GET: AdminPostController/Details/
        

    }
}
