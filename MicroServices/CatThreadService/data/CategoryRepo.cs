using System;
using MicroServices.CatThreadService.Data;

namespace CatThreadService.Data;

public class CategoryRepo : ICategoryRepo

{
    private readonly AppDBContext _context;

    public CategoryRepo(AppDBContext context)
    {
        _context = context;
    }
    public void CreateCategory(Category category)
    {
        if (category == null){
            throw new ArgumentNullException(nameof(category));

        }
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        if (category == null){
            throw new ArgumentNullException(nameof(category));
        }
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
    }

    public void UpdateCategory(Category category)
    {
     
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

      
        var existingCategory = _context.Categories.Find(category.CategoryId);

       
        if (existingCategory == null)
        {
            throw new InvalidOperationException("Category not found");
        }

       
        existingCategory.CategoryName = category.CategoryName;
        existingCategory.Description = category.Description;
        existingCategory.CreatedBy = category.CreatedBy;
        existingCategory.CreatedDate = category.CreatedDate;

      
        _context.SaveChanges();
    }

    public bool SaveChanges(){
        return(_context.SaveChanges() >= 0);
    }
}
