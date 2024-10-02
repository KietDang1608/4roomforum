using System;

namespace CatThreadService.Data;

public interface  ICategoryRepo
{
    IEnumerable<Category> GetAllCategories();
    Category GetCategoryById(int id);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
    bool SaveChanges();
}