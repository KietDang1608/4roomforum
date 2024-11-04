using System;

namespace CatThreadService.Data;

public interface  ICategoryRepo
{
    IEnumerable<Category> GetAllCategories();
    IEnumerable<Category> GetCategoriesById(int id);
    Category GetCategoryById(int id);

    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
    
    bool SaveChanges();
}
