using _4roomforum.DTOs;
using PostService.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IReplyService
    {
        Task<IEnumerable<ReplyDTO>?> GetAllReplies(int PostId);
        
    }
}