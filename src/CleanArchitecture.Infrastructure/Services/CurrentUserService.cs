using System.Security.Claims;
using CleanArchitecture.Application.Abstractions.Common;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

    public string? UserName => _httpContextAccessor.HttpContext?.User?.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value 
        ?? _httpContextAccessor.HttpContext?.User?.Identity?.Name;

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
