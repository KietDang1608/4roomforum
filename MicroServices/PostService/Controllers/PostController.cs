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
        //private readonly IPostRepo _postRepo;

        //public PostController(IPostRepo postRepo)
        //{
        //    _postRepo = postRepo;
        //}

        //// GET: api/post
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        //{
        //    var posts = await _postRepo.GetAllPostsAsync();
        //    return Ok(posts);
        //}

        //// GET: api/post/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Post>> GetPostById(int id)
        //{
        //    var post = await _postRepo.GetPostByIdAsync(id);

        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(post);
        //}

        //// POST: api/post
        //[HttpPost]
        //public async Task<ActionResult> CreatePost([FromBody] Post post)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    await _postRepo.CreatePostAsync(post);

        //    return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        //}

        //// PUT: api/post/{id}
        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdatePost(int id, [FromBody] Post post)
        //{
        //    if (id != post.Id || !ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var existingPost = await _postRepo.GetPostByIdAsync(id);
        //    if (existingPost == null)
        //    {
        //        return NotFound();
        //    }

        //    await _postRepo.UpdatePostAsync(post);

        //    return Ok();
        //}

        //// DELETE: api/post/{id}
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeletePost(int id)
        //{
        //    var post = await _postRepo.GetPostByIdAsync(id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    await _postRepo.DeletePostAsync(id);
        //    return NoContent();
        //}

        private readonly IBaseRepository<Post, PostDTO, CreatePostDTO, UpdatePostDTO> _postRepo;

        public PostController(IBaseRepository<Post, PostDTO, CreatePostDTO, UpdatePostDTO> postRepo)
        {
            _postRepo = postRepo;
        }

        // GET: api/post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
        {
            var posts = await _postRepo.GetAllAsync();
            return Ok(posts);
        }

        // GET: api/post/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _postRepo.GetByIdAsync(id);
            return Ok(post);
        }

        // POST: api/post
        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _postRepo.AddAsync(createPostDTO))
            {
                return Ok("Post is created");
            }

            return BadRequest("Cannot create reply!");
        }

        // PUT: api/post/{id}
        [HttpPut("{id}-{option}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] UpdatePostDTO? updatePostDTO, string option)
        //updatePostDTO bo trong khi chon option la like hoac unlike
        {
            if (option.Equals("edit"))
            {
                if(await _postRepo.UpdateAsync(id, updatePostDTO, CustomUpdate: null))
                {
                    return Ok($"Post {id} is updated!");
                }               
            }
            else if (option.Equals("like"))
            {
                if(await _postRepo.UpdateAsync(id, DTOs: null, CustomUpdate: Item => Item.Like++))
                {
                    return Ok($"Post {id} is updated!");
                }               

            }
            else
            {
                if(await _postRepo.UpdateAsync(id, DTOs: null, CustomUpdate: Item => Item.Like--))
                {
                    return Ok($"Post {id} is updated!");
                }              
            }
            return BadRequest("Cannot create post!");
        }

        // DELETE: api/post/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if (await _postRepo.DeleteAsync(id))
            {
                return Ok($"Post {id} is delete!");
            }
            return BadRequest("Cannot delete post");
        }
    }

}
