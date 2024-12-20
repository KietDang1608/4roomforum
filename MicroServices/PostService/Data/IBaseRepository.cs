﻿using Microsoft.EntityFrameworkCore;

namespace PostService.Data
{
    public interface IBaseRepository<T, D1, D2, D3>
        where T : class
        where D1 : class
        where D2 : class
        where D3 : class
    {
        Task<IEnumerable<D1>> GetAllAsync();
        Task<D1> GetByIdAsync(int id);
        Task<bool> AddAsync(D2 DTOs);
        Task<int?> AddAsync1(D2 DTOs);
        Task<bool> UpdateAsync(int id, D3 DTOs);
        Task<bool> DeleteAsync(int id);
    }

}
