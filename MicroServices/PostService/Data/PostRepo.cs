using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Data
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDBContext _context;

        public PostRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreatePostAsync(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync() 
        {
            return await _context.Posts.ToListAsync(); 
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<PagedResult<Post>> getPostsByThreadIdAsync(int threadId, int page, int userId, int pageSize)
        {
            int totalCount = await _context.Posts
                .Where(p => p.ThreadId == threadId)
                .CountAsync();

            var posts = await _context.Posts
                .Where(p => p.ThreadId == threadId)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            if (userId > 0)
            {
                posts = await _context.Posts
                .Where(p => p.ThreadId == threadId)
                .OrderBy(p => p.PostedBy == userId ? 0 : 1)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }

            return new PagedResult<Post>(posts, totalCount, page, pageSize);
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task UpdatePostAsync(Post post)
        {
            var existingPost = await _context.Posts.FindAsync(post.Id);
            if (existingPost != null)
            {
                existingPost.PostContent = post.PostContent;
                existingPost.IsEdited = post.IsEdited;

                _context.Posts.Update(existingPost);
                await _context.SaveChangesAsync();
            }
        }
    }

}
