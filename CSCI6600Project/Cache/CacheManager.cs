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
            try
            {
                _cache.SetString(key,JsonConvert.SerializeObject(value),options);
            }
            catch
            {
                // If write fails, it'll try again next time
            }
        }

        public T GetCacheValue<T>(string key)
        {
            try
            {
                var value = _cache.GetString(key);
                if (value != null)
                    return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                // if cache times out, just return null   
            }
            return default(T);
        }
    }
}
