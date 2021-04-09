using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CSCI6600Project.Cache
{
    public class CacheManager : ICacheManager
    {
        IDistributedCache _cache;
        DistributedCacheEntryOptions options;
        public CacheManager(IDistributedCache cache)
        {
            _cache = cache;
            options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddMinutes(30));
        }

        public void WriteToCache<T>(string key, T value)
        {
            _cache.SetString(key,JsonConvert.SerializeObject(value),options);
        }

        public T GetCacheValue<T>(string key)
        {
            var value = _cache.GetString(key);
            if (value != null)
                return JsonConvert.DeserializeObject<T>(value);
            return default(T);
        }
    }
}
