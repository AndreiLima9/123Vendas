using AndreiLima._123Vendas.Domain.Entities;
using AndreiLima._123Vendas.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AndreiLima._123Vendas.Infrastructure.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly RepositoryContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            await _context.Entry(entity).Context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            var list = await GetAsync(filter, tracked);
            return list.FirstOrDefault();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
