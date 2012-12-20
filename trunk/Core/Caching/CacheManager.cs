using System;
using System.Caching;

namespace Caching
{
    public class CacheManager : IDisposable
    {
        private ConcurrentCache realCache;
        public CacheManager(ConcurrentCache realCache)
        {
            this.realCache = realCache;
        }
        public int Count
        {
            get { return realCache.Count; }
        }
        public bool Contains(string key)
        {
            return realCache.Contains(key);
        }
        public object this[string key]
        {
            get { return realCache.GetData(key); }
        }
        public void Add(string key, object value)
        {
            Add(key, value, null);
        }
        public void Add(string key, object value, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            realCache.Add(key, value, refreshAction, expirations);
        }
        public void Remove(string key)
        {
            realCache.Remove(key);
        }
        public object GetData(string key)
        {
            return realCache.GetData(key);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (realCache != null)
            {
                realCache.Dispose();
                realCache = null;
            }
        }
    }
}
