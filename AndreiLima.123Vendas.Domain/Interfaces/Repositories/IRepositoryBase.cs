using AndreiLima._123Vendas.Domain.Entities;
using System.Linq.Expressions;


namespace AndreiLima._123Vendas.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<Guid> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    }
}
