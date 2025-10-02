using System;

namespace CleanArchitecture.Domain.ValueObjects;

public class Address
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string Country { get; init; }

    private Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }

    public static Address Create(string street, string city, string state, string zipCode, string country)
        => new(street, city, state, zipCode, country);

    public override string ToString()
        => $"{Street}, {City}, {State} {ZipCode}, {Country}";

}
