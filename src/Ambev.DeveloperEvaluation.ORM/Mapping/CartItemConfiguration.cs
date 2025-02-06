using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;
public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(ci => ci.Quantity)
            .IsRequired();

        builder.Property(ci => ci.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.Discount)
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.CreatedAt)
            .IsRequired();

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}