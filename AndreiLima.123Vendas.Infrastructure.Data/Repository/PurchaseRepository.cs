using AndreiLima._123Vendas.Domain.Entities;
using AndreiLima._123Vendas.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AndreiLima._123Vendas.Infrastructure.Data.Repository
{
    public class PurchaseRepository : RepositoryBase<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(RepositoryContext context) : base(context)
        {
        }

        public override async Task<Purchase> GetByIdAsync(Guid id)
        {
            return await _context.Purchases.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
