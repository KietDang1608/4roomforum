
using _4roomforum.DTOs;
using _4roomforum.Services.Interfaces;
namespace _4roomforum.Services.Implements
{
    public class CategoryServiceImpl : ICategoryService
    {
        private readonly ILogger<CategoryServiceImpl> _logger;
        private readonly HttpClient _client;
        public CategoryServiceImpl(HttpClient httpClient,ILogger<CategoryServiceImpl> logger)
        {
            _logger = logger;
            _client = httpClient;
            _client.BaseAddress = new Uri("http://localhost:5001/");
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            try
            {
                var response = await _client.GetAsync("api/category");
                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDTO>>();
                    return categories ?? new List<CategoryDTO>();
                }
                else
                {
                    _logger.LogError($"Failed to get categories. Status Code: {response.StatusCode}");
                    return new List<CategoryDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAllCategory: {ex.Message}");
                return new List<CategoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAllCategory: {ex.Message}");
                return new List<CategoryDTO>();
            }
        }
        public async Task<IEnumerable<CategoryViewDTO>> GetHotCategories()
        {
            try
            {
                var response = await _client.GetAsync("api/category/hot-categories");
                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryViewDTO>>();
                    return categories ?? new List<CategoryViewDTO>();
                }
                else
                {
                    _logger.LogError($"Failed to get hot categories. Status Code: {response.StatusCode}");
                    return new List<CategoryViewDTO>();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetHotCategories: {ex.Message}");
                return new List<CategoryViewDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetHotCategories: {ex.Message}");
                return new List<CategoryViewDTO>();
            }
        }

    }
}