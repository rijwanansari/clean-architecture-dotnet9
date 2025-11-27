using CleanArchitecture.Application.Abstractions.Common;

namespace CleanArchitecture.Infrastructure.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    
    public DateTime Now => DateTime.Now;
    
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Now);
}
