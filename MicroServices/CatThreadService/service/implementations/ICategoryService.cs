using System;

namespace CatThreadService.service.implementations;

public interface ICategoryService
{
    Task<Category> GetCategoryById(int id);
    Task<Category> CreateCategory(Category category);
    Task<Category> UpdateCategory(Category category);
    Task<Category> DeleteCategory(int id);
    Task<List<Category>> GetAllCategories();
}
