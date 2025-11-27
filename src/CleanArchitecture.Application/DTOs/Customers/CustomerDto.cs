using CleanArchitecture.Application.DTOs.Common;

namespace CleanArchitecture.Application.DTOs.Customers;

/// <summary>
/// Customer list item DTO
/// </summary>
public record CustomerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime CreatedAt
);
