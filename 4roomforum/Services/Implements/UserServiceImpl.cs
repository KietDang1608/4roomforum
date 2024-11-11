using _4roomforum.DTOs;
using _4roomforum.Services.Interfaces;

namespace _4roomforum.Services.Implements
{
    public class UserServiceImpl: IUserService
    {
        private readonly ILogger<UserServiceImpl> _logger;
        private readonly HttpClient _client;

        public UserServiceImpl(HttpClient httpClient, ILogger<UserServiceImpl> logger)
        {
            _logger = logger;
            _client = httpClient;
            _client.BaseAddress = new Uri("http://localhost:5002/");
        }

        public async Task<UserDTO> Login(string email, string password)
        {
            try
            {
                var loginData = new { Email = email, Password = password };
                var response = await _client.PostAsJsonAsync("api/user/login", loginData);
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserDTO>();
                    return user;
                }
                else
                {
                    _logger.LogError($"Login failed. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in Login: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in Login: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDTO> GetUserProfile(int userId)
        {
            try
            {
                var response = await _client.GetAsync($"api/user/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var userProfile = await response.Content.ReadFromJsonAsync<UserDTO>();
                    return userProfile;
                }
                else
                {
                    _logger.LogError($"Failed to get user profile. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetUserProfile: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetUserProfile: {ex.Message}");
                return null;
            }
        }
        public async Task<UserDTO?> RegisterUserAsync(UserDTO userDTO)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("api/user/register", userDTO);

                if (response.IsSuccessStatusCode)
                {
                    var newUser = await response.Content.ReadFromJsonAsync<UserDTO>();
                    return newUser;
                }
                else
                {
                    _logger.LogError($"Register failed. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in RegisterUserAsync: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in RegisterUserAsync: {ex.Message}");
                return null;
            }
        }

    }
}