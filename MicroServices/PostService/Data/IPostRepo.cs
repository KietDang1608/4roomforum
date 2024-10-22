using PostService.Models;

namespace PostService.Data
{
    public interface IPostRepo
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }
}
