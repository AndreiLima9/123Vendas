using AndreiLima._123Vendas.Domain.Entities;
using AndreiLima._123Vendas.Domain.Interfaces.Repositories;


namespace AndreiLima._123Vendas.Infrastructure.Data.Repository
{
    public class SaleItemRepository : RepositoryBase<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(IEnumerable<SaleItem> items)
        {
            _dbSet.AddRange(items);
            _context.SaveChanges();
        }

        public async Task RemoveRangeAsync(IEnumerable<SaleItem> items)
        {
            _dbSet.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
