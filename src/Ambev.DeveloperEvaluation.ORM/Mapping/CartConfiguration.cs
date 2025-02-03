using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.SaleNumber)
            .IsRequired();

        builder.Property(c => c.SaleDate)
            .IsRequired();

        builder.Property(c => c.Customer)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.Branch)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.IsCanceled)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt);

        builder.HasMany(c => c.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}