using PostService.DTOs;
using PostService.Models;

namespace PostService.Data
{
    public interface IReplyRepo
    {
        Task<IEnumerable<ReplyDTO>> GetAllRepliesAsync();
        Task<ReplyDTO> GetReplyByIdAsync(int id);
        Task<bool> CreateReplyAsync(CreateReplyDTO1 createReplyDTO1);
        Task<bool> CreateReplyToReplyAsync(CreateReplyDTO2 createReplyDTO2);
        Task<bool> UpdateReplyAsync(int id, UpdateReplyDTO updateReplyDTO);
        Task<bool> ChangeVoteReply(int id, string option);
        Task<bool> DeleteReplyAsync(int id);
    }
}
