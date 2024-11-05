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
    public class ThreadController : ControllerBase
    {
        private readonly IThreadRepo _repo;
        private readonly IMapper _mapper;
        public ThreadController(IThreadRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ThreadDTO>> GetAllThread()
        {
            System.Console.WriteLine("Getting threads");

            var threads = _repo.GetAllThread();

            return Ok(_mapper.Map<IEnumerable<ThreadDTO>>(threads));
        }
        [HttpGet("{id}", Name = "GetThreadById")]
        public ActionResult<ThreadDTO> GetThreadById(int id)
        {
            var cat = _repo.GetThreadById(id);
            if (cat != null)
            {
                return Ok(_mapper.Map<ThreadDTO>(cat));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<ThreadDTO> CreateThread(ThreadDTO thread)
        {
            var catModel = _mapper.Map<Threads>(thread);

            _repo.CreateThread(catModel);

            var catDto = _mapper.Map<ThreadDTO>(catModel);

            return CreatedAtRoute(nameof(GetThreadById), new { id = catDto.ThreadId }, catDto);
        }
        [HttpDelete("{id}")]
        public ActionResult<ThreadDTO> DeleteThread(int id)
        {
            try
            {
                _repo.DeleteThread(id);
                return Ok(new { message = "Thread delete successfully" });

            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = "Thread not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error while deleting Thread" });
            }

        }
    }
}
