using _4roomforum.DTOs;
using _4roomforum.Services.Interfaces;
using PostService.DTOs;
using System.Net.Http;

namespace _4roomforum.Services.Implements
{
    public class ReplyServiceImpl : IReplyService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ReplyServiceImpl> _logger;

        public ReplyServiceImpl(HttpClient httpClient, ILogger<ReplyServiceImpl> logger)
        {
            _client = httpClient;         
            _logger = logger;
            _client.BaseAddress = new Uri("http://localhost:5003/");
        }

        public async Task<IEnumerable<ReplyDTO>> GetAllReplies(int PostId)
        {
            try
            {
                var response = await _client.GetAsync($"api/reply/get-all-by-post/{PostId}");
                if (response.IsSuccessStatusCode)
                {
                    var replies = await response.Content.ReadFromJsonAsync<IEnumerable<ReplyDTO>>();
                    return replies;
                }
                else
                {
                    _logger.LogError($"Failed to get replies. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAllReplies: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAllReplies: {ex.Message}");
                return null;
            }

        }


    }
}
