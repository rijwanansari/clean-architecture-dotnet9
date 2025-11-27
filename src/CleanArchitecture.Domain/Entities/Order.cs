using System;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.DomainEvents;
using CleanArchitecture.Domain.Enumerators;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; }
    public Customer? Customer { get; init; }
    public OrderStatus Status { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public Address ShippingAddress { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order() { }

    private Order(Guid id, string orderNumber, Guid customerId, 
        PaymentMethod paymentMethod, Address shippingAddress) : base(id)
    {
        OrderNumber = orderNumber;
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        PaymentMethod = paymentMethod;
        ShippingAddress = shippingAddress;
    }

    public static Order Create(Guid customerId, PaymentMethod paymentMethod, Address shippingAddress)
    {
        var order = new Order(
            Guid.NewGuid(),
            GenerateOrderNumber(),
            customerId,
            paymentMethod,
            shippingAddress
        );

        order.RaiseDomainEvent(new OrderCreatedEvent(order.Id, order.OrderNumber));
        return order;
    }

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero");

        if (product.StockQuantity < quantity)
            throw new InsufficientStockException($"Insufficient stock for product {product.Name}");

        var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            _items.Add(OrderItem.Create(Id, product.Id, product.Name, product.Price, quantity));
        }

        SetUpdatedAt();
    }

    public Money GetTotalAmount()
    {
        if (!_items.Any())
            return Money.Zero;

        return _items.Select(i => i.GetSubtotal())
            .Aggregate((a, b) => a.Add(b));
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Cancelled || Status == OrderStatus.Delivered)
            throw new DomainException($"Cannot change status of {Status} order");

        Status = newStatus;
        SetUpdatedAt();

        if (newStatus == OrderStatus.Delivered)
        {
            CompletedAt = DateTime.UtcNow;
            RaiseDomainEvent(new OrderCompletedEvent(Id, OrderNumber));
        }
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new DomainException("Cannot cancel delivered order");

        Status = OrderStatus.Cancelled;
        SetUpdatedAt();
    }

    private static string GenerateOrderNumber()
        => $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";

}
