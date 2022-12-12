using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using PayByrd.Proof.Bussiness.Constants;

namespace PayByrd.Proof.Bussiness.Infrastructure.Cache;
public static class MemCache
{
    private static readonly MemoryCache Cache = new MemoryCache(Global.MEMCACHE);
    private static readonly CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
    {
        AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
    };

    public static bool Exists(string key)
    {
        return Cache.Get(key) != null;
    }

    public static T Get<T>(string key)
    {
        return (T)Cache.Get(key);
    }
    public static void UpdateOrAdd<T>(string key, T obj)
    {
        if (Exists(key))
            Cache.Remove(key);

        Cache.Add(new CacheItem(key, obj), cacheItemPolicy);
    }

}
