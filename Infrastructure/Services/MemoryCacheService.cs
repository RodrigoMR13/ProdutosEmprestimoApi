using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public MemoryCacheService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions();

            if (expiration.HasValue)
                options.AbsoluteExpirationRelativeToNow = expiration;

            await _cache.SetStringAsync(key, value, options);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
