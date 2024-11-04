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

        private readonly IReplyRepo _replyRepo;

        public ReplyController(IReplyRepo replyRepo)
        {
            _replyRepo = replyRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReplyDTO>>> GetReplies()
        {    
            return Ok(await _replyRepo.GetAllRepliesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyDTO>> GetReply(int id)
        {
            return Ok(await _replyRepo.GetReplyByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReplyForPost(CreateReplyDTO1 createReplyDTO1)
        {
            try
            {
                if (await _replyRepo.CreateReplyAsync(createReplyDTO1))
                {
                    return Ok("Reply is created");
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

        [HttpPost("/to-reply")]
        public async Task<IActionResult> CreateReplyForReply(CreateReplyDTO2 createReplyDTO2)
        {
            try
            {
                if (await _replyRepo.CreateReplyToReplyAsync(createReplyDTO2))
                {
                    return Ok($"Reply to reply {createReplyDTO2.ReplyToReply} is created");
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
                if (await _replyRepo.UpdateReplyAsync(id, updateReplyDTO))
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

        [HttpPut("/{option}/{id}")]
        public async Task<IActionResult> ChangeVoteReply(int id, string option)
        {
            try
            {
                if (await _replyRepo.ChangeVoteReply(id, option))
                {
                    return Ok($"Reply {id} is updated!");
                }
                return BadRequest("Cannot change vote reply");
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
                if (await _replyRepo.DeleteReplyAsync(id))
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
    }
}
