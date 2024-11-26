using _4roomforum.DTOs;
using PostService.DTOs;
//using PostService.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IPostService
    {
        Task<PagedResult<PostDTO>> GetPostsByThreadId(int id, int page, int pageSize);
        Task<PostDTO> GetPostById(int id);
    }
}
