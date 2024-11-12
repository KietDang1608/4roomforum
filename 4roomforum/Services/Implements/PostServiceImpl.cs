using _4roomforum.Services.Interfaces;
using _4roomforum.DTOs;
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
        public async Task<IEnumerable<PostDTO>> GetAllPost()
        {
            try
            {
                var response = await _client.GetAsync("api/post");
                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadFromJsonAsync<IEnumerable<PostDTO>>();
                    return posts ?? new List<PostDTO>();
                }
                else
                {
                    _logger.LogError($"Failed to get posts. Status Code: {response.StatusCode}");
                    return new List<PostDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAllPost: {ex.Message}");
                return new List<PostDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAllPost: {ex.Message}");
                return new List<PostDTO>();
            }
        }
    }
}
