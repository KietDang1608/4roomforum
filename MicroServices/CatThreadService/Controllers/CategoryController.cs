using System.Text.RegularExpressions;
using AutoMapper;
using CatThreadService.Data;
using CatThreadService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CatThreadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        private readonly IThreadRepo _threadRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepo repo,IThreadRepo threadRepo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _threadRepo = threadRepo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            System.Console.WriteLine("Getting categories");

            var categories = _repo.GetAllCategories();

            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }
        [HttpGet("{id}", Name ="GetCategoryById")]
        public ActionResult<CategoryDTO> GetCategoryById(int id){
            var cat = _repo.GetCategoryById(id);
            if (cat != null){
                return Ok(_mapper.Map<CategoryDTO>(cat));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<CategoryDTO> AddCategory(CategoryDTO category){
            var catModel = _mapper.Map<Category>(category);

            _repo.CreateCategory(catModel);

            var catDto = _mapper.Map<CategoryDTO>(catModel);

            return CreatedAtRoute(nameof(GetCategoryById), new {id = catDto.CategoryId}, catDto);
        }
        [HttpDelete("{id}")]
        public ActionResult<CategoryDTO> DeleteCategory(int id){
            try{
                _repo.DeleteCategory(id);
                return Ok(new {message = "Category delete successfully"});

            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {message = "Category not found"});
            }
            catch (Exception e){
                return StatusCode(500, new {message = "An error while deleting category"});
            }

        }
        [HttpGet("hot-categories")]
        public ActionResult<IEnumerable<CategoryViewDTO>> GetHotCategories(){
            var threads = _threadRepo.GetAllThread();

            var hotCategories = threads.GroupBy(thread => thread.CategoryID)
                .Select(group => new 
                    {
                        CategoryId = group.Key, 
                        TotalViewCount = group.Sum(thread => thread.ViewCount)
                    })
                .OrderByDescending(g => g.TotalViewCount)
                .ToList();
            var categoriesDto = new List<CategoryViewDTO>();
            foreach (var hotCategory in hotCategories)
            {
                var category = _repo.GetCategoryById(hotCategory.CategoryId);
                if (category != null)
                {
                    CategoryViewDTO categoryDto = new CategoryViewDTO();
                    categoryDto.CategoryId = category.CategoryId;
                    categoryDto.CategoryName = category.CategoryName;
                    categoryDto.Description = category.Description;
                    categoryDto.CreatedBy = category.CreatedBy;
                    categoryDto.ViewCount = hotCategory.TotalViewCount;
                    categoriesDto.Add(categoryDto);
                }
            }
            
            return Ok(categoriesDto)    ;
        }
    }
}
