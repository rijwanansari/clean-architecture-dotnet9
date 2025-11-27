namespace CleanArchitecture.Application.Abstractions.Common;

/// <summary>
/// Provides access to the current authenticated user information
/// </summary>
public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
    bool IsAuthenticated { get; }
}
