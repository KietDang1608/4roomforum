using Microsoft.AspNetCore.Mvc;
using PostService.Data;
using PostService.Models;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepo _postRepo;

        public PostController(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        // GET: api/post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            var posts = await _postRepo.GetAllPostsAsync();
            return Ok(posts);
        }

        // GET: api/post/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _postRepo.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST: api/post
        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _postRepo.CreatePostAsync(post);

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        // PUT: api/post/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (id != post.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPost = await _postRepo.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            await _postRepo.UpdatePostAsync(post);

            return Ok();
        }

        // DELETE: api/post/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await _postRepo.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            await _postRepo.DeletePostAsync(id);
            return NoContent();
        }
    }

}
