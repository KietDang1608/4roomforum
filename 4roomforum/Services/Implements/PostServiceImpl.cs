using _4roomforum.Services.Interfaces;
using _4roomforum.DTOs;
using PostService.DTOs;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
                var response = await _client.GetAsync($"api/post/with_thread/{id}/{page}/{pageSize}");
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

        public async Task<bool> CreatePostAsync(CreatePostDTO postDTO)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("api/post", postDTO);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Success: {message}");
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Bad Request: {error}");
                }
                else
                {
                    _logger.LogError($"Failed to create post. Status Code: {response.StatusCode}");
                }

                return false;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in CreatePostAsync: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in CreatePostAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
{
    try
    {
        // Gọi API "api/post"
        var response = await _client.GetAsync("api/post");

        if (response.IsSuccessStatusCode)
        {
            // Deserialize dữ liệu từ response
            var posts = await response.Content.ReadFromJsonAsync<IEnumerable<PostDTO>>();
            return posts ?? Enumerable.Empty<PostDTO>();
        }
        else
        {
            _logger.LogError($"Failed to get all posts. Status Code: {response.StatusCode}");
            return Enumerable.Empty<PostDTO>();
        }
    }
    catch (HttpRequestException ex)
    {
        _logger.LogError($"Request error in GetAllPosts: {ex.Message}");
        return Enumerable.Empty<PostDTO>();
    }
    catch (Exception ex)
    {
        _logger.LogError($"Unexpected error in GetAllPosts: {ex.Message}");
        return Enumerable.Empty<PostDTO>();
    }
}

    }
}
