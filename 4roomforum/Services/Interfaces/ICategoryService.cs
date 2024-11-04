using _4roomforum.DTOs;

namespace _4roomforum.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<IEnumerable<CategoryViewDTO>> GetHotCategories();
        
    }
}