using System;

namespace CleanArchitecture.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    private Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        
        Amount = amount;
        Currency = currency ?? "USD";
    }

    public static Money Of(decimal amount, string currency = "USD") 
        => new(amount, currency);

    public static Money Zero => new(0, "USD");

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot add money with different currencies");
        
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot subtract money with different currencies");
        
        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(decimal multiplier) 
        => new(Amount * multiplier, Currency);
}
