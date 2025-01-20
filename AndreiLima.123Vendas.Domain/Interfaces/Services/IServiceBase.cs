using AndreiLima._123Vendas.Domain.Entities;

namespace AndreiLima._123Vendas.Domain.Interfaces.Services
{
    public interface IServiceBase<T> where T : EntityBase
    {
        Task<Guid> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
    }
}
