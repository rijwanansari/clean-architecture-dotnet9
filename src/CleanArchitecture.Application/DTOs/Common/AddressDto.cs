namespace CleanArchitecture.Application.DTOs.Common;

/// <summary>
/// Address data transfer object
/// </summary>
public record AddressDto(
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country
);
