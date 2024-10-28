using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly IDbContextFactory _dbContextFactory;

        internal GenericRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<T> CreateAsync(T entity)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                var result = await dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task CreateRange(IEnumerable<T> entity)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext()) 
            {
                var dbSet = _dbContext.Set<T>();
                await dbSet.AddRangeAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(true);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                dbSet.RemoveRange(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                var DbList = await dbSet.ToListAsync();
                return DbList;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                IQueryable<T> query = dbSet;

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                IQueryable<T> query = dbSet;

                if (predicate != null) query = query.Where(predicate);
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.ToListAsync();
            }
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext()) {
                var dbSet = _dbContext.Set<T>();
                return await dbSet.FindAsync(id);
            }
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                DbSet<T> dbSet;
                IQueryable<T> query;

                dbSet = _dbContext.Set<T>();
                query = dbSet;

                if (predicate != null) query = query.Where(predicate);
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                var result = dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task UpdateRange(IEnumerable<T> entity)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbSet = _dbContext.Set<T>();
                dbSet.UpdateRange(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}