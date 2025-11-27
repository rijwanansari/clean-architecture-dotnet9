using CleanArchitecture.Application.DTOs.Common;

namespace CleanArchitecture.Application.DTOs.Customers;

/// <summary>
/// Detailed customer DTO
/// </summary>
public record CustomerDetailsDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    AddressDto Address,
    int TotalOrders,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
