using PostService.Models;

namespace PostService.Data
{
    public interface ILikeOfReplyRepo
    {
        Task<LikeOfReply?> GetLikeOfReplyAndUser(int replyId, int userId);
        Task<IEnumerable<LikeOfReply>> LikeFromAReply(int replyId);
    }
}
