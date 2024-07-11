﻿using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
