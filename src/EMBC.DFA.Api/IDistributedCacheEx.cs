using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace EMBC.DFA.Api
{
    public static class IDistributedCacheEx
    {
        public static async Task<T?> GetOrSet<T>(this IDistributedCache cache, string key, Func<Task<T>> factory, TimeSpan? expiry)
        {
            var obj = await Get<T>(cache, key);
            if (obj == null)
            {
                obj = await factory();
                await Set<T>(cache, key, obj, expiry);
            }

            return obj;
        }

        public static async Task<T?> Get<T>(this IDistributedCache cache, string key)
        {
            return Deserialize<T>(await cache.GetAsync(key));
        }

        public static async Task Set<T>(this IDistributedCache cache, string key, T obj, TimeSpan? expiry)
        {
            await cache.SetAsync(key, Serialize(obj), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiry });
        }

        public static async Task Remove(this IDistributedCache cache, string key)
        {
            await cache.RemoveAsync(key);
        }

        private static T? Deserialize<T>(byte[] data) => data == null || data.Length == 0 ? default(T?) : JsonSerializer.Deserialize<T?>(data);

        private static byte[] Serialize<T>(T obj) => obj == null ? Array.Empty<byte>() : JsonSerializer.SerializeToUtf8Bytes(obj);
    }

    public static class CacheConfiguration
    {
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            return services;
        }
    }
}
