using _4roomforum.Services.Interfaces;
using _4roomforum.DTOs;
using PostService.DTOs;
using System.Diagnostics;
//using PostService.DTOs;
namespace _4roomforum.Services.Implements
{
    public class PostServiceImpl : IPostService
    {
        private readonly ILogger<PostServiceImpl> _logger;
        private readonly HttpClient _client;

        public PostServiceImpl(HttpClient httpClient, ILogger<PostServiceImpl> logger)
        {
            _logger = logger;
            _client = httpClient;
            _client.BaseAddress = new Uri("http://localhost:5003/");
        }
        public async Task<PagedResult<PostDTO>> GetPostsByThreadId(int id, int page, int pageSize)
        {
            try
            {
                var response = await _client.GetAsync($"api/post/with_thread/{id}/{page}");
                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadFromJsonAsync<PagedResult<PostDTO>>();
                    return posts ?? new PagedResult<PostDTO>();
                }
                else
                {
                    _logger.LogError($"Failed to get posts. Status Code: {response.StatusCode}");
                    return new PagedResult<PostDTO>();

                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAllPost: {ex.Message}");
                return new PagedResult<PostDTO>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAllPost: {ex.Message}");
                return new PagedResult<PostDTO>();
            }
        }

        public async Task<PostDTO> GetPostById(int id)
        {
            try
            {
                var response = await _client.GetAsync($"api/post/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var post = await response.Content.ReadFromJsonAsync<PostDTO>();
                    return post;
                }
                else
                {
                    _logger.LogError($"Failed to get post. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in getapost: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in getapost: {ex.Message}");
                return null;
            }
        }
    }
}
