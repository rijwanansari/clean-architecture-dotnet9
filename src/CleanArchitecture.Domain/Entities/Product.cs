using System;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } 
    public int StockQuantity { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private Product() { } // EF Constructor

    private Product(Guid id, string name, string description, Money price,
        int stockQuantity, string category) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        Category = category;
        IsActive = true;
    }

    public static Product Create(string name, string description, Money price,
        int stockQuantity, string category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required");

        if (stockQuantity < 0)
            throw new DomainException("Stock quantity cannot be negative");

        return new Product(Guid.NewGuid(), name, description, price, stockQuantity, category);
    }

    public void UpdateDetails(string name, string description, Money price, string category)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        SetUpdatedAt();
    }

    public void UpdateStock(int quantity)
    {
        if (StockQuantity + quantity < 0)
            throw new InsufficientStockException($"Insufficient stock for product {Name}");

        StockQuantity += quantity;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdatedAt();
    }

}
