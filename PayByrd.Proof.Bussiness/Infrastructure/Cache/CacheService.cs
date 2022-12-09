using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;

namespace PayByrd.Proof.Bussiness.Infrastructure.Cache;
public static class CacheService
{
    private static readonly ObjectCache ObjCache = MemoryCache.Default;

    private static bool Exists(string key)
    {
        return ObjCache.Get(key) != null;
    }

    public static T Get<T>(string key)
    {
        try
        {
            if (ObjCache[key] is T)
            {
                return (T)ObjCache[key];
            }
            else
            {
                throw new ApplicationException("create new exception");
            }
        }
        catch
        {
            throw;
        }
    }
    public static T? UpdateOrAdd<T>(string key, T obj, DateTime time)
    {
        var value = Get<T>(key);
        if (value != null)
        {
            return value;
        }
      
        ObjCache.Add(key, obj, DateTime.Now.AddHours(1));
        return obj;
    }

}
