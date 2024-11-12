using PostService.DTOs;
using PostService.Models;

namespace PostService.Data
{
    public interface IPostRepo
    {
        Task<IEnumerable<PostDTO>> GetAllPostsAsync(int ThreadId);
        Task<PagedResult<PostDTO>> GetPagedAsync(int pageNumber, int pageSize, int ThreadId);
    }
}
