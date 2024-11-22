using _4roomforum.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface IThreadService
    {
        Task<IEnumerable<ThreadDTO>> GetAllThreads();
        Task<IEnumerable<ThreadDTO>> GetHotThreads();
        Task<ThreadDTO> GetThreadById(int threadID);
        
    }
}
