using _4roomforum.Services.Interfaces;
using _4roomforum.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace _4roomforum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        
        private readonly ICategoryService _categoryService;
      
        public AdminCategoriesController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }
        public async Task<ActionResult>Index()
        {
            var categories = getAllCategory();
            return View("~/Views/Admin/categories.cshtml", categories);
        }
        private List<Category> getAllCategory()
        {
           try{
                var categories = _categoryService.getAllCategory();
                return categories;
           }catch(Exception ex){
               
               return new List<Category>();
           }
        }
        public IActionResult CreateCategory()
        {
            return View("~/Views/Admin/AddCategory.cshtml");
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                bool issSuccess = await _categoryService.AddCategory(category);
                if (issSuccess){
                    ViewBag.Message = "Category add successfully!";
                    var categories = getAllCategory();
                    return View("~/Views/Admin/categories.cshtml", categories);
                } else{
                    ViewBag.Message = "Add Category Errorr!";
                }
            }

            ViewBag.Message = "Error while adding Add Category!";
            return View("~/Views/Admin/AddCategory.cshtml");
        }

        public IActionResult UppdateCategory(int id)
        {
            var category = getCategoryById(id);
            return View("~/Views/Admin/UppdateCategory.cshtml",category);
        }
        private Category getCategoryById(int id)
        {
            try{
                var category = _categoryService.getCategoryById(id);
                return category;
            }catch(Exception ex){
                return new Category();
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                bool issSuccess = await _categoryService.EditCategory(category);
                if (issSuccess)
                {
                    ViewBag.Message = "Category edit successfully!";
                    var categories = getAllCategory();
                    return View("~/Views/Admin/categories.cshtml", categories);
                }
                else
                {
                    ViewBag.Message = "Edit Category Errorr!";
                    var categories = _categoryService.getAllCategory();
                    return View("~/Views/Admin/categories.cshtml", categories);
                }
            }  
            var categories1 = getAllCategory();
            ViewBag.Message = "Error while  edit Category!";
            return View("~/Views/Admin/categories.cshtml", categories1);
        }
    
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool isSuccess = await _categoryService.RemoveCategory(id);

            if (isSuccess)
            {
                ViewBag.Message = "Category deleted successfully!";
            }
            else
            {
                ViewBag.Message = "Error while deleting category!";
            }
            var categories = getAllCategory();
            return View("~/Views/Admin/categories.cshtml", categories);
        }
    
        
    }
}
