using _4roomforum.DTOs;
using _4roomforum.Models;
namespace _4roomforum.Services.Interfaces
{
    public interface IThreadService
    {
        Task<IEnumerable<ThreadDTO>> GetAllThreads();
        Task<IEnumerable<ThreadDTO>> GetHotThreads();
        Task<ThreadDTO> GetThreadById(int threadID);
        
        Task<IEnumerable<ThreadDTO>> GetThreadsByCategoryId(int categoryId);
        Task<bool> EditThread(Threads thread);
        Threads getThreadById(int threadID);
    }
}
