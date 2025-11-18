using System;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(255);
        });

        // (Index on email removed to avoid design-time mapping conflicts with owned property)

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(20);

        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("AddressStreet").HasMaxLength(200);
            address.Property(a => a.City).HasColumnName("AddressCity").HasMaxLength(100);
            address.Property(a => a.State).HasColumnName("AddressState").HasMaxLength(100);
            address.Property(a => a.ZipCode).HasColumnName("AddressZipCode").HasMaxLength(20);
            address.Property(a => a.Country).HasColumnName("AddressCountry").HasMaxLength(100);
        });
    }
}
