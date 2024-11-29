using _4roomforum.DTOs;
using Microsoft.AspNetCore.Mvc;
using PostService.DTOs;
//using PostService.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IPostService
    {
        Task<PagedResult<PostDTO>> GetPostsByThreadId(int threadId, int userId, int page);
        Task<PostDTO> GetPostById(int id);
        Task<bool> CreatePostAsync(CreatePostDTO postDTO);

        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
        Task<bool> UpdatePostAsync(int id, UpdatePostDTO postDTO);
        Task<bool> DeletePostAsync(int id);

    }
}
