using _4roomforum.DTOs;
//using PostService.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAllPost();
        Task<PostDTO> GetPostById(int id);
    }
}
