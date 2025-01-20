using AndreiLima._123Vendas.Domain.Entities;

namespace AndreiLima._123Vendas.Domain.Interfaces.Repositories
{
    public interface ISaleItemRepository : IRepositoryBase<SaleItem>
    {
        Task RemoveRangeAsync(IEnumerable<SaleItem> items);
        Task AddRangeAsync(IEnumerable<SaleItem> items);
    }
}
