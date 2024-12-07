using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostService.DTOs;
using PostService.Models;
using System.Collections.Generic;

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

        public async Task<IEnumerable<ReplyDTO>> GetAllRepliesAsync(int PostId)
        {
            var List = await _context.Replies.Where(r => r.PostId == PostId).ToListAsync();
            return _mapper.Map<IEnumerable<ReplyDTO>>(List);
        }

        public async Task<PagedResult<ReplyDTO>> GetPagedAsync(int pageNumber, int pageSize, int PostId)
        {
            var count = await _context.Replies.Where(r => r.PostId == PostId).CountAsync();
            var items = await _context.Replies.Where(r => r.PostId == PostId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var List = _mapper.Map<IEnumerable<ReplyDTO>>(items);
            return new PagedResult<ReplyDTO>(List, count, pageNumber, pageSize);
        }


    }
}
