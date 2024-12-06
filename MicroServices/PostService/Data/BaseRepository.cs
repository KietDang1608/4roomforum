
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostService.DTOs;
using PostService.ExceptionHandler;
using PostService.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace PostService.Data
{
    public class BaseRepository<T, D1, D2, D3> : IBaseRepository<T, D1, D2, D3>
        where T : class
        where D1 : class
        where D2 : class
        where D3 : class
    {

        private readonly AppDBContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public BaseRepository(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<D1>> GetAllAsync()
        {
            var List = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<D1>>(List);
        }

        public async Task<D1> GetByIdAsync(int id)
        {
            var Item = await _dbSet.FindAsync(id);
            if (Item == null)
            {
                throw new DataNotFoundException($"Data with ID {id} was not found.");
            }
            return _mapper.Map<D1>(Item);
        }

        public async Task<bool> AddAsync(D2 DTOs)
        {
            var Item = _mapper.Map<T>(DTOs);
            await _dbSet.AddAsync(Item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Item = await _dbSet.FindAsync(id);
            if (Item == null)
            {
                throw new DataNotFoundException($"Data with ID {id} was not found.");
            }
            _dbSet.Remove(Item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, D3 DTOs)
        {
            var Item = await _dbSet.FindAsync(id);
            if (Item == null)
            {
                throw new DataNotFoundException($"Data with ID {id} was not found.");
            }
            if (DTOs != null)
            {
                _mapper.Map(DTOs, Item);
            }
            _dbSet.Update(Item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
