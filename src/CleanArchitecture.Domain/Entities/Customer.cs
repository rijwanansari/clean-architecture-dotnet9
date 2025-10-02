using System;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }

    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private Customer() { }

    private Customer(Guid id, string firstName, string lastName, 
        Email email, string phoneNumber, Address address) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public static Customer Create(string firstName, string lastName, 
        Email email, string phoneNumber, Address address)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));

        return new Customer(Guid.NewGuid(), firstName, lastName, email, phoneNumber, address);
    }

    public void UpdateInfo(string firstName, string lastName, string phoneNumber, Address address)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public string GetFullName() => $"{FirstName} {LastName}";

}
