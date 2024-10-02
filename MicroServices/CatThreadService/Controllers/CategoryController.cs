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
    }
}
