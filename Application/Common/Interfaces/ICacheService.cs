﻿namespace Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value, TimeSpan? expiration = null);
        Task<string?> GetAsync(string key);
        Task RemoveAsync(string key);
    }
}
