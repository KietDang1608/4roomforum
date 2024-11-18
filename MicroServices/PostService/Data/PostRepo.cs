using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PostService.DTOs;
using PostService.Models;
using System.Threading;

namespace PostService.Data
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public PostRepo(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync(int ThreadId) 
        {
            var List = await _context.Posts.Where(p => p.ThreadId == ThreadId).ToListAsync(); 
            return _mapper.Map<IEnumerable<PostDTO>>(List);
        }

        public async Task<PagedResult<PostDTO>> GetPagedAsync(int pageNumber, int pageSize, int ThreadId)
        {
            var count = await _context.Posts.Where(r => r.ThreadId == ThreadId).CountAsync();
            var items = await _context.Posts.Where(r => r.ThreadId == ThreadId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var List = _mapper.Map<IEnumerable<PostDTO>>(items);
            return new PagedResult<PostDTO>(List, count, pageNumber, pageSize);
        }
    }

}
