
using _4roomforum.DTOs;
namespace _4roomforum.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Login(string username, string password);
        Task<UserDTO> GetUserProfile(int userId);
        Task<UserDTO?> RegisterUserAsync(UserDTO userDTO);
    }
}