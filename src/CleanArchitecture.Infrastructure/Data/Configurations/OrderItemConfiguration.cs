using System;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.ProductName)
            .IsRequired()
            .HasMaxLength(200);

        // builder.OwnsOne(oi => oi.UnitPrice, price =>
        //     {
        //         price.Property(p => p.Amount)
        //             .HasColumnName("UnitPrice")
        //             .HasPrecision(18, 2);

        //         price.Property(p => p.Currency)
        //             .HasColumnName("Currency")
        //             .HasMaxLength(3);
        //     });

        builder.Property(oi => oi.Quantity)
            .IsRequired();
    }
}