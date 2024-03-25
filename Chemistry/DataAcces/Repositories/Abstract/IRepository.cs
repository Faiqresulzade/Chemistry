﻿using Core.Entities.Base;
using System.Linq.Expressions;

namespace DataAcces.Repositories.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
        Task<T> FirstorDefaultAsync();
    }
}
