namespace CleanArchitecture.Application.Abstractions.Common;

/// <summary>
/// Provides abstraction for DateTime operations to enable testability
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTime Now { get; }
    DateOnly Today { get; }
}
