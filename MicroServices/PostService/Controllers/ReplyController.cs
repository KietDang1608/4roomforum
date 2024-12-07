using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
    

        private readonly IBaseRepository<Reply, ReplyDTO, CreateReplyDTO, UpdateReplyDTO> _repository;
        private readonly IBaseRepository<LikeOfReply, LikeOfReplyDTO, CreateLikeOfReplyDTO, UpdateLikeOfReplyDTO> _likeOfReplyRepoBase;
        private readonly ILikeOfReplyRepo _likeOfReplyRepo;
        private readonly IReplyRepo _replyRepo;

        public ReplyController(
            IBaseRepository<Reply, ReplyDTO, CreateReplyDTO, UpdateReplyDTO> repository,
            ILikeOfReplyRepo likeOfReplyRepo,
            IReplyRepo replyRepo,
            IBaseRepository<LikeOfReply, LikeOfReplyDTO, CreateLikeOfReplyDTO, UpdateLikeOfReplyDTO> likeOfReplyRepoBase)
        {
            _repository = repository;
            _likeOfReplyRepo = likeOfReplyRepo;
            _replyRepo = replyRepo;
            _likeOfReplyRepoBase = likeOfReplyRepoBase;
        }


        [HttpGet("get-all-by-post/{id}")]
        public async Task<IActionResult> GetAllReplies(int id, int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _replyRepo.GetPagedAsync(pageNumber, pageSize, id);
            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyDTO>> GetReply(int id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReply(CreateReplyDTO createReplyDTO)
        {
            try
            {
                var newResponse = await _repository.AddAsync1(createReplyDTO);  
                if (newResponse != null)
                {
                    var replyDto = new ReplyDTO { ReplyId = 1 };

                    return CreatedAtAction(nameof(GetReply), new { id = newResponse }, replyDto);
                    //return Ok("Reply is created");
                }
                return BadRequest("Cannot create reply!");
            }
            catch (DbUpdateException dbEx)
            {
                // Xử lý các lỗi liên quan đến việc cập nhật cơ sở dữ liệu (vd: lỗi khóa, lỗi ràng buộc,...)
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Database update failed: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Bắt các lỗi chung khác
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReply(int id, UpdateReplyDTO updateReplyDTO)
        {
            try
            {
                if (await _repository.UpdateAsync(id, updateReplyDTO))
                {
                    return Ok($"Reply {id} is updated!");
                }
                return BadRequest("Cannot update reply");
            }
            catch (DbUpdateException dbEx)
            {
                // Xử lý các lỗi liên quan đến việc cập nhật cơ sở dữ liệu (vd: lỗi khóa, lỗi ràng buộc,...)
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Database update failed: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Bắt các lỗi chung khác
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            try
            {
                if (await _repository.DeleteAsync(id))
                {
                    return Ok($"Reply {id} is delete!");
                }
                return BadRequest("Cannot delete reply");
            }
            catch (DbUpdateException dbEx)
            {
                // Xử lý các lỗi liên quan đến việc cập nhật cơ sở dữ liệu (vd: lỗi khóa, lỗi ràng buộc,...)
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Database update failed: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Bắt các lỗi chung khác
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("likereply/{replyId}/{userId}/{vote}")]
        public async Task<ActionResult> LikeOrUnlike(int replyId, int userId, int vote)
        {
            try
            {
                var existingLike = await _likeOfReplyRepo.GetLikeOfReplyAndUser(replyId, userId);
                if (existingLike == null)
                {
                    await _likeOfReplyRepoBase.AddAsync(new CreateLikeOfReplyDTO(replyId, userId));
                    existingLike = await _likeOfReplyRepo.GetLikeOfReplyAndUser(replyId, userId);
                }
                if (vote == 0 && await _likeOfReplyRepoBase.DeleteAsync(existingLike.Id))
                {
                    return Ok($"Reset like selection reply {replyId}");
                }
                UpdateLikeOfReplyDTO likeOfReplyDTO = new UpdateLikeOfReplyDTO(userId, vote);
                if (await _likeOfReplyRepoBase.UpdateAsync(existingLike.Id, likeOfReplyDTO))
                    return Ok($"Liked reply {replyId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Failed to interact with reply.");
        }

        [HttpGet("get-all-react/{id}")]
        public async Task<IActionResult> GetAllReaction(int id)
        {
            try
            {
                var reaction = await _likeOfReplyRepo.LikeFromAReply(id);
                return Ok(reaction);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
