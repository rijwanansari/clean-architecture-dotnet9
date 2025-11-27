namespace CleanArchitecture.Application.DTOs.Common;

/// <summary>
/// Money data transfer object
/// </summary>
public record MoneyDto(
    decimal Amount,
    string Currency
);
