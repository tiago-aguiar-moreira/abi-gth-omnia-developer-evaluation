using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Phone)
            .HasMaxLength(20);
        
        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.Property(u => u.Status)
            .HasConversion<short>()
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(u => u.Role)
            .HasConversion<short>()
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(u => u.City)
            .HasMaxLength(100);

        builder.Property(u => u.Street)
            .HasMaxLength(200);

        builder.Property(u => u.Number)
            .HasMaxLength(10);

        builder.Property(u => u.Zipcode)
            .HasMaxLength(10);

        builder.Property(u => u.Latitude)
            .HasColumnType("decimal(9,6)");

        builder.Property(u => u.Longitude)
            .HasColumnType("decimal(9,6)");

        builder.Property(u => u.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder.Property(u => u.UpdatedAt)
            .HasColumnType("timestamp with time zone");
    }
}
