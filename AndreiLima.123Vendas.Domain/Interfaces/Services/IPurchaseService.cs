using AndreiLima._123Vendas.Domain.Entities;

namespace AndreiLima._123Vendas.Domain.Interfaces.Services
{
    public interface IPurchaseService : IServiceBase<Purchase>
    {
        Task UpdateAsync(Guid id, Purchase purchase);
        Task<IEnumerable<Purchase>> GetAllAsync();
    }
}
