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

        public async Task<PagedResult<ReplyDTO>> GetAllReplies(int PostId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _client.GetAsync($"api/reply/get-all-by-post/{PostId}?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var replies = await response.Content.ReadFromJsonAsync<PagedResult<ReplyDTO>>();
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

        public async Task<ReplyDTO> GetAReply(int ReplyId)
        {
            try
            {
                var response = await _client.GetAsync($"api/reply/{ReplyId}");
                if (response.IsSuccessStatusCode)
                {
                    var reply = await response.Content.ReadFromJsonAsync<ReplyDTO>();
                    return reply;
                }
                else
                {
                    _logger.LogError($"Failed to get reply. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAReply: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAReply: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateReply(CreateReplyDTO createReplyDTO)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("api/reply", createReplyDTO);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    _logger.LogError($"Failed to create reply. Status Code: {response.StatusCode}");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in CreateReply: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAReply: {ex.Message}");
                return false;
            }
        }

    }
}
