using PostService.Models;

namespace PostService.Data
{
    public interface ILikeOfPostRepo
    {
        Task<IEnumerable<LikeOfPost>> LikeFromAPost(int postId);
        Task<LikeOfPost?> GetFromPostAndUser(int postId, int userId);
    }
}
