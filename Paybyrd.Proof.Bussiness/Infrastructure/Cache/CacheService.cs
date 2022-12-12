using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using Paybyrd.Proof.Bussiness.Constants;

namespace Paybyrd.Proof.Bussiness.Infrastructure.Cache;

/// <summary>
///  This Class implement the memory cache.
///  In a scenario with many requests and a need of high-throughput, I'd use Redis or another in-memory data structure store. 
/// </summary>
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
