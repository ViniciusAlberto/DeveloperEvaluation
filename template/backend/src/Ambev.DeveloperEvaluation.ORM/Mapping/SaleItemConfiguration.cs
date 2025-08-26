using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.UnitPrice).IsRequired();
            builder.Property(i => i.Discount).IsRequired();
            builder.Ignore(i => i.Total);

            // Configuração do Value Object ExternalProduct
            builder.OwnsOne(i => i.Product, p =>
            {
                p.Property(x => x.ExternalId)
                    .HasColumnName("ProductExternalId")
                    .IsRequired()
                    .HasMaxLength(50);

                p.Property(x => x.Name)
                    .HasColumnName("ProductName")
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Relacionamento com Sale
            builder.HasOne<Sale>()
                   .WithMany(s => s.Items)
                   .HasForeignKey("SaleId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
