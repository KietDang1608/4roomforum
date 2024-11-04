using _4roomforum.DTOs;
using _4roomforum.Services.Implements;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _4roomforum.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        // GET: CategoryController
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
       
        public async Task<ActionResult> Index()
        {
            try
            {
                var hotCategories = await _categoryService.GetHotCategories();
                ViewBag.HotCategories = hotCategories;
                var categories = await _categoryService.GetAllCategory();
                ViewBag.Categories = categories;
                return View();
            }
            catch (Exception ex)
            {
                return View(new List<CategoryDTO>());
            }
        }
    }
}
