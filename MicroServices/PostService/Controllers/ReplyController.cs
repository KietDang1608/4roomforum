using AutoMapper;
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
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ReplyController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReplyDTO>>> GetReplies()
        {
            var replies = await _context.Replies.ToListAsync();    
            return Ok(_mapper.Map<IEnumerable<ReplyDTO>>(replies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyDTO>> GetReply(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound("Reply" + id + " was not found!");
            }

            return Ok(_mapper.Map<ReplyDTO>(reply));
        }

        [HttpPost]
        public async Task<ActionResult<ReplyDTO>> CreateReplyForPost(CreateReplyDTO1 createReplyDTO1)
        {
            try
            {
                var reply = _mapper.Map<Reply>(createReplyDTO1);
                _context.Replies.Add(reply);
                await _context.SaveChangesAsync();

                var replyDTO = _mapper.Map<ReplyDTO>(reply);
                return CreatedAtAction(nameof(GetReply), new { id = reply.ReplyId }, replyDTO);
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

        [HttpPost("/forReply")]
        public async Task<ActionResult<ReplyDTO>> CreateReplyForReply(CreateReplyDTO2 createReplyDTO2)
        {
            try
            {
                var reply = _mapper.Map<Reply>(createReplyDTO2);
                _context.Replies.Add(reply);
                await _context.SaveChangesAsync();

                var replyDTO = _mapper.Map<ReplyDTO>(reply);
                return CreatedAtAction(nameof(GetReply), new { id = reply.ReplyId }, replyDTO);
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
                var reply = await _context.Replies.FindAsync(id);
                if (reply == null)
                {
                    return NotFound("Reply" + id + " was not found!");
                }

                _mapper.Map(updateReplyDTO, reply);
                await _context.SaveChangesAsync();

                return Ok("Reply got updated!");
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
                var reply = await _context.Replies.FindAsync(id);
                if (reply == null)
                {
                    return NotFound("Reply" + id + " was not found!");
                }
                if (option.Equals("upvote"))
                {
                    reply.UpvoteAmount++;
                }
                else
                {
                    reply.UpvoteAmount--;
                }

                await _context.SaveChangesAsync();

                return Ok("Reply got updated!");
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
                var reply = await _context.Replies.FindAsync(id);
                if (reply == null)
                {
                    return NotFound("Reply" + id + " was not found!");
                }

                _context.Replies.Remove(reply);
                await _context.SaveChangesAsync();

                return Ok("Reply is deleted!");
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
