using System;
using System.Collections.Concurrent;
using System.Text.Json;
using CleanArchitecture.Application.Abstractions.Caching;

namespace CleanArchitecture.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly ConcurrentDictionary<string, CacheEntry> _cache = new();

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(key, out var entry))
        {
            if (entry.ExpiresAt == null || entry.ExpiresAt > DateTime.UtcNow)
            {
                return Task.FromResult(JsonSerializer.Deserialize<T>(entry.Value));
            }
            _cache.TryRemove(key, out _);
        }
        return Task.FromResult<T?>(default);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, 
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value);
        var entry = new CacheEntry
        {
            Value = json,
            ExpiresAt = expiration.HasValue ? DateTime.UtcNow.Add(expiration.Value) : null
        };
        _cache[key] = entry;
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _cache.TryRemove(key, out _);
        return Task.CompletedTask;
    }

    private class CacheEntry
    {
        public string Value { get; set; } = string.Empty;
        public DateTime? ExpiresAt { get; set; }
    }
}
