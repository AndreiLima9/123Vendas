using AndreiLima._123Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AndreiLima._123Vendas.Infrastructure.Data.Configuration
{

    public class PurchaseConfig : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.SaleNumber)
                .IsRequired();

            builder
                .Property(x => x.SaleDate)
                .IsRequired();

            builder
                .Property(x => x.TotalValue)
                .IsRequired();

            builder
                .Property(x => x.StoreCode)
                .IsRequired();

            builder
                .Property(x => x.Canceled)
                .IsRequired();

            builder
                .Property(x => x.ClientId)
                .IsRequired();

            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Purchase);
        }
    }
}
