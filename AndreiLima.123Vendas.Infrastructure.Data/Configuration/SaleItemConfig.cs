using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AndreiLima._123Vendas.Domain.Entities;


namespace AndreiLima._123Vendas.Infrastructure.Data.Configuration
{
    public class SaleItemConfig : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItem");
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.ProductCode)
                .IsRequired();

            builder
                .Property(x => x.Quantity)
                .IsRequired();

            builder
                .Property(x => x.UnitValue)
                .IsRequired();

            builder
                .Property(x => x.Discount)
                .IsRequired();
        }
    }
}
