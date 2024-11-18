using _4roomforum.DTOs;
using _4roomforum.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4roomforum.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        List<Category> getAllCategory();
        Task<IEnumerable<CategoryViewDTO>> GetHotCategories();
        
        Task<bool> AddCategory(Category category);
        Task<bool> EditCategory(Category category);
        Task<bool> RemoveCategory(int id);
        Category getCategoryById(int id);

    }
}