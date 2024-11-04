using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using PostService.DTOs;
using PostService.ExceptionHandler;
using PostService.Models;

namespace PostService.Data
{
    public class ReplyRepo : IReplyRepo
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ReplyRepo(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReplyDTO>> GetAllRepliesAsync()
        {
            var replies = await _context.Replies.ToListAsync();
            return _mapper.Map<IEnumerable<ReplyDTO>>(replies);
        }

        public async Task<ReplyDTO> GetReplyByIdAsync(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                throw new DataNotFoundException($"Data with ID {id} was not found.");
            }
            return _mapper.Map<ReplyDTO>(reply);
        }

        public async Task<bool> CreateReplyAsync(CreateReplyDTO1 createReplyDTO1)
        {
            var reply = _mapper.Map<Reply>(createReplyDTO1);
            await _context.Replies.AddAsync(reply);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateReplyToReplyAsync(CreateReplyDTO2 createReplyDTO2)
        {
            var reply = _mapper.Map<Reply>(createReplyDTO2);
            await _context.Replies.AddAsync(reply);

            return await _context.SaveChangesAsync() > 0;
        }       

        public async Task<bool> UpdateReplyAsync(int id, UpdateReplyDTO updateReplyDTO)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                throw new DataNotFoundException("Reply" + id + " was not found!");
            }

            _mapper.Map(updateReplyDTO, reply);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeVoteReply(int id, string option)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                throw new DataNotFoundException("Reply" + id + " was not found!");
            }
            if (option.Equals("upvote"))
            {
                reply.UpvoteAmount++;
            }
            else
            {
                reply.UpvoteAmount--;
            }

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteReplyAsync(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                throw new DataNotFoundException("Reply" + id + " was not found!");
            }

            _context.Replies.Remove(reply);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
