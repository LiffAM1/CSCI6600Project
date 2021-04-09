using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Cache
{
    public interface ICacheManager
    {
        void WriteToCache<T>(string key, T value);

        T GetCacheValue<T>(string key);
    }
}
