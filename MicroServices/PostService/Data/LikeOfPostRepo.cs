using Microsoft.EntityFrameworkCore;
using PostService.Models;

namespace PostService.Data
{
    public class LikeOfPostRepo : ILikeOfPostRepo
    {
        private readonly AppDBContext _context;

        public LikeOfPostRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<LikeOfPost?> GetFromPostAndUser(int postId, int userId)
        {
            return await _context.LikeOfPosts
                .Where(l => l.PostId == postId && l.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LikeOfPost>> LikeFromAPost(int postId)
        {
            return await _context.LikeOfPosts
                         .Where(l => l.PostId == postId)
                         .ToListAsync();
        }
    }
}
