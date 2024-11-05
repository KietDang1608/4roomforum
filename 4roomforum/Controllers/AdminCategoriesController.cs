
using _4roomforum.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace _4roomforum.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private ILogger<HomeController> _logger;
      
        public AdminCategoriesController(ILogger<HomeController> logger) {
            _logger = logger;
        }
        public async Task<ActionResult>Index()
        {
            var categories = getAllCategory();
            return View("~/Views/Admin/categories.cshtml", categories);
        }
        private List<Category> getAllCategory()
        {
            using (var client = new HttpClient()) 
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    var reponseTask = client.GetAsync("api/category");
                    reponseTask.Wait();

                    var result = reponseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var categories = result.Content.ReadFromJsonAsync<IList<Category>>().Result;
                        if (categories == null)
                        {
                            ViewBag.Message = "No category";
                        }
                        return categories?.ToList() ?? new List<Category>();
                    }
                    else
                    {
                        _logger.LogError("Server error. Please contact administrator.");
                        return new List<Category>();
                    }
                }
                catch (AggregateException ex)
                {
                    ViewBag.Message = "Category service is not available";
                    return new List<Category>();
                }
                catch (HttpRequestException ex)
                {
                    ViewBag.Message = "category service is not available";
                    return new List<Category>();
                }
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
                bool issSuccess = await AddCategory(category);
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
        private async Task<bool> AddCategory(Category newCategory)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    var response = await client.PostAsJsonAsync("api/category", newCategory);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogError("Error while adding Category");
                        return false;
                    }
                   
                }
                catch (HttpRequestException ex)
                {   
                    _logger.LogError(ex,"Error in AddCategory");
                    return false;
                }
            }
        }
        public IActionResult UppdateCategory(int id)
        {
            var category = getCategoryById(id);
            return View("~/Views/Admin/UppdateCategory.cshtml",category);
        }
        private Category getCategoryById(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    var responseTask = client.GetAsync($"api/category/{id}"); //truyền id qua bên kia ok? 
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var category = result.Content.ReadFromJsonAsync<Category>().Result;
                        return category;
                    }
                    else
                    {
                        _logger.LogError($"Error fetching category with ID {id}."); 
                        return null;
                    }
                }
                catch (AggregateException ex)
                {
                    ViewBag.Message = "Category service is not available"; 
                    return null;
                }
                catch (HttpRequestException ex)
                {
                    ViewBag.Message = "Category service is not available"; 
                    return null;
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                bool issSuccess = await EditCategory(category);
                if (issSuccess)
                {
                    ViewBag.Message = "Category edit successfully!";
                    var categories = getAllCategory();
                    return View("~/Views/Admin/categories.cshtml", categories);
                }
                else
                {
                    ViewBag.Message = "Edit Category Errorr!";
                    var categories = getAllCategory();
                    return View("~/Views/Admin/categories.cshtml", categories);
                }
            }  
            var categories1 = getAllCategory();
            ViewBag.Message = "Error while  edit Category!";
            return View("~/Views/Admin/categories.cshtml", categories1);
        }
        private async Task<bool> EditCategory(Category updateCategory)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    var response = await client.PutAsJsonAsync($"api/category/{updateCategory.CategoryId}", updateCategory);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogError("Error while Edit Category");
                        return false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "Error in EditCategory");
                    return false;
                }

            }
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool isSuccess = await RemoveCategory(id);

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

        private async Task<bool> RemoveCategory(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    var response = await client.DeleteAsync($"api/category/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogError($"Error while deleting category with ID {id}");
                        return false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, $"Error in RemoveCategory for ID {id}");
                    return false;
                }
            }
        }



    }
}
