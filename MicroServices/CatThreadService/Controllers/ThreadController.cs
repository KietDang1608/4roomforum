using AutoMapper;
using CatThreadService.Data;
using CatThreadService.DTOs;
using Microsoft.AspNetCore.Components.Web;
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
            var thread = _repo.GetThreadById(id);

            if (thread != null)
            {
                return Ok(_mapper.Map<ThreadDTO>(thread));
            }
            else
            {
                return NotFound();
            }
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


        [HttpGet("hotThread")]
        public ActionResult<IEnumerable<ThreadDTO>> GetHotThreads()
        {
            var threads = _repo.GetAllThread();

            var hotThreads = threads
                .OrderByDescending(thread => thread.ViewCount)
                .Take(4)
                .Select( thread => new ThreadDTO
                {
                    ThreadId = thread.ThreadId,
                    CategoryID = thread.CategoryID,
                    ThreadTitle = thread.ThreadTitle,
                    ThreadContent = thread.ThreadContent,
                    ViewCount = thread.ViewCount,
                    CreatedBy = thread.CreatedBy,
                    CreatedDate =  thread.CreatedDate
                })
                .ToList();

            return Ok(_mapper.Map<IEnumerable<ThreadDTO>>(hotThreads));
        }

        [HttpGet("getThreadByCategory/{id}")]
        public ActionResult<IEnumerable<ThreadDTO>> GetThreadByCategory(int id)
        {
            var threads = _repo.GetThreadsByCategoryId(id);

            return Ok(_mapper.Map<IEnumerable<ThreadDTO>>(threads));
        }

        [HttpPut("{id}")]
        public ActionResult<ThreadDTO> UpdateThread(int id,Threads thread)
        {
            try
            {
                
                var thr = _repo.GetThreadById(id);
                if (thr == null)
                {
                    return NotFound(new { message = "Thread not found" });
                }
                thr.CategoryID = thread.CategoryID;
                thr.ThreadTitle = thread.ThreadTitle;
                thr.ThreadContent = thread.ThreadContent;
                thr.CreatedBy = thread.CreatedBy;
                thr.CreatedDate = thread.CreatedDate;
                thr.ViewCount = thread.ViewCount;
                thr.IsPinned = thread.IsPinned;
                thr.IsClosed = thread.IsClosed;
                _repo.UpdateThread(thr);
                return Ok(new { message = "Thread Update successfully" });

            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = "Thread not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error while update thread" });
            }

        }
    }


}
