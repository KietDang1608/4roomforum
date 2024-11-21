using Microsoft.EntityFrameworkCore;
using PostService.Models;

namespace PostService.Data
{
    public class LikeOfReplyRepo : ILikeOfReplyRepo
    {
        private readonly AppDBContext _context;

        public LikeOfReplyRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<LikeOfReply?> GetLikeOfReplyAndUser(int replyId, int userId)
        {
            return await _context.LikeOfReplies
                .Where(l => l.ReplyId == replyId && l.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LikeOfReply>> LikeFromAReply(int replyId)
        {
            return await _context.LikeOfReplies
                .Where(l => l.ReplyId == replyId).ToListAsync();
        }
    }
}
