using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Cache
{
    public interface ICacheManager
    {
        void WriteToCache(string key, List<Guid> value);

        List<Guid> GetCacheValue(string key);
    }
}
