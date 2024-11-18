using _4roomforum.DTOs;
using _4roomforum.Services.Implements;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _4roomforum.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        // GET: CategoryController
        public CategoryController(ICategoryService categoryService, IUserService userService)
        {
            _categoryService = categoryService;
            _userService = userService;
        }
        
       
        public async Task<ActionResult> Index()
        {
            try
            {
                var hotCategories = await _categoryService.GetHotCategories();
                ViewBag.HotCategories = hotCategories;
                var categories = await _categoryService.GetAllCategory();
                var categoryDetails = new List<CategoryDetailDTO>();
                for (int i = 0; i < categories.Count(); i++)
                {
                    CategoryDetailDTO categoryDetail = new CategoryDetailDTO();
                    UserDTO user = await _userService.GetUserById(categories.ElementAt(i).CreatedBy);

                    categoryDetail.CategoryId = categories.ElementAt(i).CategoryId;
                    categoryDetail.CategoryName = categories.ElementAt(i).CategoryName;
                    categoryDetail.Description = categories.ElementAt(i).Description;
                    categoryDetail.CreatedBy = user;
                    categoryDetail.CreatedDate = categories.ElementAt(i).CreatedDate;
                    categoryDetails.Add(categoryDetail);
                }
                ViewBag.Categories = categoryDetails;
                return View();
            }
            catch (Exception ex)
            {
                return View(new List<CategoryDetailDTO>());
            }
        }
    }
}
