using _4roomforum.DTOs;
using PostService.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IReplyService
    {
        Task<PagedResult<ReplyDTO>?> GetAllReplies(int PostId, int pageNumber, int pageSize);
        Task<ReplyDTO> GetAReply(int id);
        Task<bool> CreateReply(CreateReplyDTO createReplyDTO);
    }
}