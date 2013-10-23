using System;
using System.Caching;

namespace Caching
{
    public class CacheManager
    {
        private static Cache realCache;
        static CacheManager()
        {
            realCache = new Cache();
        }

        public static bool Contains(string key)
        {
            return realCache.Contains(key);
        }

        public static void Add(string key, object value)
        {
            Add(key, value, null);
        }
        public static void Add(string key, object value, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            realCache.Add(key, value, refreshAction, expirations);
        }
        public static void Remove(string key)
        {
            realCache.Remove(key);
        }
        public static object GetData(string key)
        {
            return realCache.GetData(key);
        }

        public static T GetData<T>(string key) where T : class
        {
            return realCache.GetData<T>(key);
        }
    }
}
