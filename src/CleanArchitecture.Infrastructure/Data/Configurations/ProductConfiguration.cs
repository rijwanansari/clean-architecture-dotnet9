using System;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Category)
            .IsRequired()
            .HasMaxLength(100);

        // builder.OwnsOne(p => p.Price, price =>
        // {
        //     price.Property(m => m.Amount)
        //         .HasColumnName("Price")
        //         .HasPrecision(18, 2);

        //     price.Property(m => m.Currency)
        //         .HasColumnName("Currency")
        //         .HasMaxLength(3);
        // });

        builder.Property(p => p.StockQuantity)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired();

        builder.HasIndex(p => p.Category);
        builder.HasIndex(p => p.IsActive);
    }
}
