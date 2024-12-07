using _4roomforum.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IReplyService
    {
        Task<PagedResult<ReplyDTO>?> GetAllReplies(int PostId, int pageNumber, int pageSize);
        Task<ReplyDTO> GetAReply(int id);   
        Task<bool> CreateReply(CreateReplyDTO createReplyDTO);
        Task<int?> CreateReply1(CreateReplyDTO createReplyDTO);
        Task<bool> DeleteReply(int id);
        Task<bool> UpdateReply(int id, UpdateReplyDTO updateReplyDTO);
        Task<bool> ReactToReply(int replyId, int userId, int vote);
        Task<IEnumerable<LikeOfReplyDTO>> GetAllReaction(int ReplyId);
    }
}