using PostService.DTOs;
using PostService.Models;

namespace PostService.Data
{
    public interface ILikeOfReplyRepo
    {
        Task<LikeOfReplyDTO?> GetLikeOfReplyAndUser(int replyId, int userId);
        Task<IEnumerable<LikeOfReplyDTO>> LikeFromAReply(int replyId);
    }
}
