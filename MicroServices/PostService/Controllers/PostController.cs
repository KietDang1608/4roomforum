using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PostService.Data;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IBaseRepository<Post, PostDTO, CreatePostDTO, UpdatePostDTO> _basePostRepo;
        private readonly IBaseRepository<LikeOfPost, LikeOfPostDTO, CreateLikeOfPostDTO, UpdateLikeOfPostDTO> _likeOfPostRepoBase;
        private readonly IPostRepo _postRepo;
        private readonly ILikeOfPostRepo _likeOfPostRepo;
        private readonly IMapper _mapper;

        public PostController(
            IBaseRepository<Post, PostDTO, CreatePostDTO, UpdatePostDTO> basePostRepo,
            IPostRepo postRepo,
            ILikeOfPostRepo likeOfPostRepo,
            IBaseRepository<LikeOfPost, LikeOfPostDTO, CreateLikeOfPostDTO, UpdateLikeOfPostDTO> likeOfPostRepoBase,
            IMapper mapper)
        {
            _basePostRepo = basePostRepo;
            _postRepo = postRepo;
            _likeOfPostRepo = likeOfPostRepo;
            _likeOfPostRepoBase = likeOfPostRepoBase;
            _mapper = mapper;
        }

        //GET: api/post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
        {
            var posts = await _basePostRepo.GetAllAsync();
            return Ok(posts);
        }

        // GET: api/post/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _basePostRepo.GetByIdAsync(id);
            return Ok(post);
        }

        [HttpGet("with_thread/{threadId}/{userId}/{page}")]
        public async Task<ActionResult<PagedResult<PostDTO>>> GetPostsByThreadId(
            int threadId,
            int userId,
            int page,
            int pageSize = 5)
        {
            //if (page <= 0 || pageSize <= 0)
            //{
            //    return BadRequest("Page and pageSize must be greater than 0.");
            //}

            var pagedPosts = await _postRepo.getPostsByThreadIdAsync(threadId, page, userId, pageSize);

            var postDTOs = _mapper.Map<IEnumerable<PostDTO>>(pagedPosts.Items);


            var pagedResult = new PagedResult<PostDTO>(postDTOs, pagedPosts.TotalCount, pagedPosts.CurrentPage, pageSize);


            return Ok(pagedResult);
        }

        // POST: api/post
        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _basePostRepo.AddAsync(createPostDTO))
            {
                return Ok("Post is created");
            }

            return BadRequest("Cannot create reply!");
        }

        // PUT: api/post/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] UpdatePostDTO? updatePostDTO)
        {
            if (await _basePostRepo.UpdateAsync(id, updatePostDTO, CustomUpdate: null))
            {
                return Ok($"Post {id} is updated!");
            }
            return BadRequest("Cannot create post!");
        }

        // DELETE: api/post/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if (await _basePostRepo.DeleteAsync(id))
            {
                return Ok($"Post {id} is delete!");
            }
            return BadRequest("Cannot delete post");
        }

        // http://localhost:5003/api/post/like/{post_id}/{user_id}
        [HttpPut("like/{postId}/{userId}")]
        public async Task<ActionResult> LikeOrUnlike(int postId, int userId)
        {
            try
            {
                var existingLike = await _likeOfPostRepo.GetFromPostAndUser(postId, userId);
                if (existingLike == null)
                {
                    if (await _likeOfPostRepoBase.AddAsync(new CreateLikeOfPostDTO(postId, userId)))
                    {
                        var isUpdated = await _postRepo.IncreaseLikeCountAsync(postId);
                        if (isUpdated)
                        {
                            var post = await _postRepo.GetPostByIdAsync(postId);
                            if (post != null)
                            {
                                return Ok(new { message = $"Liked post {postId}", likeCount = post.Like });
                            }
                            return BadRequest("Post not found after like.");
                        }
                        return BadRequest("Failed to update like count.");
                    }
                }
                else
                {
                    if (await _likeOfPostRepoBase.DeleteAsync(existingLike.Id))
                    {
                        var isUpdated = await _postRepo.DecreaseLikeCountAsync(postId);
                        if (isUpdated)
                        {
                            var post = await _postRepo.GetPostByIdAsync(postId);
                            if (post != null)
                            {
                                return Ok(new { message = $"Unliked post {postId}", likeCount = post.Like });
                            }
                            return BadRequest("Post not found after unlike.");
                        }
                        return BadRequest("Failed to update like count.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Failed to interact with post.");
        }
        [HttpGet("checkLike/{postId}/{userId}")]
        public async Task<ActionResult<bool>> CheckLike(int postId, int userId)
        {
            try
            {
                var isLiked = await _likeOfPostRepo.GetFromPostAndUser(postId, userId);

                if (isLiked != null)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }


    }

}