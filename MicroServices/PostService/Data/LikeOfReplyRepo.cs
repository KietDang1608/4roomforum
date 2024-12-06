using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Data
{
    public class LikeOfReplyRepo : ILikeOfReplyRepo
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public LikeOfReplyRepo(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LikeOfReplyDTO?> GetLikeOfReplyAndUser(int replyId, int userId)
        {
            var reaction = await _context.LikeOfReplies
                .Where(l => l.ReplyId == replyId && l.UserId == userId).FirstOrDefaultAsync();
            return _mapper.Map<LikeOfReplyDTO>(reaction);
        }

        public async Task<IEnumerable<LikeOfReplyDTO>> LikeFromAReply(int replyId)
        {
            var reaction = await _context.LikeOfReplies
                .Where(l => l.ReplyId == replyId).ToListAsync();
            return _mapper.Map<IEnumerable<LikeOfReplyDTO>>(reaction);
        }
    }
}
