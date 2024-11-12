using _4roomforum.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IReplyService
    {
        Task<IEnumerable<ReplyDTO>> GetAllReplies();
    }
}