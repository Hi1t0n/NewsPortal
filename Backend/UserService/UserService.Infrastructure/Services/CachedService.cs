using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.Services;

/// <summary>
/// <see cref="ICachedService"/>
/// </summary>
public class CachedService : ICachedService
{
    private readonly IDistributedCache _cache;
    
    public CachedService(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    /// <inheritdoc />
    public async Task<T?> GetCacheAsync<T>(string key, CancellationToken cancellationToken) where T : class
    {
         var data = await _cache.GetStringAsync(key, cancellationToken);
         if (data is null)
         {
             return default(T);
         }

         var result = JsonSerializer.Deserialize<T>(data);

         return result;
    }
    
    /// <inheritdoc />
    public async Task AddCacheAsync<T>(string key, T? data, CancellationToken cancellationToken) where T : class
    {
        if (data is null)
        {
            return;
        }

        await _cache.SetStringAsync(key, JsonSerializer.Serialize(data), new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        }, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task RemoveCacheAsync(string key, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(key, cancellationToken);
    }
}