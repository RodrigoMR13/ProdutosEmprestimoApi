using Application.Common.Interfaces;
using Infrastructure.Services;

namespace WebApi.Configuration
{
    public static class CacheConfiguration
    {
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();

            return services;
        }
    }
}
