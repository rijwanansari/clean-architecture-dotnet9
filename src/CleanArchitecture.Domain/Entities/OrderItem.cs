using System;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }


    private OrderItem() { }

    private OrderItem(Guid id, Guid orderId, Guid productId, 
        string productName, Money unitPrice, int quantity) : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static OrderItem Create(Guid orderId, Guid productId, 
        string productName, Money unitPrice, int quantity)
    {
        return new OrderItem(Guid.NewGuid(), orderId, productId, productName, unitPrice, quantity);
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        Quantity = quantity;
        SetUpdatedAt();
    }

    public Money GetSubtotal() => UnitPrice.Multiply(Quantity);
}
