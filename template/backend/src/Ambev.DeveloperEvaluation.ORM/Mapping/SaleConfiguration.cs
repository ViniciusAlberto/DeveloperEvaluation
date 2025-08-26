using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Date).IsRequired();
            builder.Property(s => s.TotalAmount).IsRequired();
            builder.Property(s => s.Cancelled).IsRequired();

            // Configuração do Value Object ExternalCustomer
            builder.OwnsOne(s => s.Customer, c =>
            {
                c.Property(x => x.ExternalId)
                 .HasColumnName("CustomerExternalId")
                 .IsRequired()
                 .HasMaxLength(50);

                c.Property(x => x.Name)
                 .HasColumnName("CustomerName")
                 .IsRequired()
                 .HasMaxLength(100);
            });

            // Configuração do Value Object ExternalBranch
            builder.OwnsOne(s => s.Branch, b =>
            {
                b.Property(x => x.ExternalId)
                 .HasColumnName("BranchExternalId")
                 .IsRequired()
                 .HasMaxLength(50);

                b.Property(x => x.Name)
                 .HasColumnName("BranchName")
                 .IsRequired()
                 .HasMaxLength(100);
            });

            // Relacionamento 1:N com SaleItem
            builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey("SaleId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
