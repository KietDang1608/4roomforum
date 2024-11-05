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
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
        [HttpPut("{id}")]
        public ActionResult<CategoryDTO> UpdateCategory(int id,Category category)
        {
            try
            {
                
                var cat = _repo.GetCategoryById(id);
                if (cat == null)
                {
                    return NotFound(new { message = "Category not found" });
                }
                cat.CategoryName = category.CategoryName;
                cat.Description = category.Description;
                cat.CreatedBy = category.CreatedBy;
                cat.CreatedDate = category.CreatedDate;
                _repo.UpdateCategory(cat);
                return Ok(new { message = "Category Update successfully" });

            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = "Category not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error while update category" });
            }

        }


    }
}
