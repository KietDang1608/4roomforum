using MicroServices.CatThreadService.data;
using CatThreadService.service.implementations;
using System.Reflection.Metadata.Ecma335;
namespace MicroServices.CatThreadService.service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDBContext _context;

        public CategoryService(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Category> GetCategoryById(int id){
            return await _context.Categories.Find(id);
        }
    }
}