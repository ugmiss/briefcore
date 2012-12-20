using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Caching;

namespace System.Caching
{
    public class ConcurrentCache
    {
        ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();
        public int Count
        {
            get { return Cache.Count; }
        }

        public bool Contains(string key)
        {
            ValidateKey(key);
            return Cache.ContainsKey(key);
        }
        public void Add(string key, object value)
        {
            Add(key, value, null);
        }
        public void Add(string key, object value, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            ValidateKey(key);
            CacheItem cacheItem = null;
            if (Cache.ContainsKey(key) == false)
            {
                cacheItem = new CacheItem(key, "", null);
                Cache[key] = cacheItem;
            }
            else
            {
                cacheItem = (CacheItem)Cache[key];
            }




            cacheItem.TouchedAction(true);
            CacheItem newCacheItem = new CacheItem(key, value, refreshAction, expirations);
            try
            {
                cacheItem.Replace(value, refreshAction, expirations);
                Cache[key] = cacheItem;
            }
            catch
            {
                object obj;
                Cache.TryRemove(key, out obj);
                throw;
            }

        }
        public void Remove(string key)
        {
            Remove(key, EnumRemovedReason.Removed);
        }
        public void Remove(string key, EnumRemovedReason removalReason)
        {
            ValidateKey(key);
            CacheItem cacheItem = null;
            cacheItem = (CacheItem)Cache[key];

            if (IsObjectInCache(cacheItem))
            {
                return;
            }


            cacheItem.TouchedAction(true);
            object obj;
            Cache.TryRemove(key, out obj);
            RefreshActionInvoker.InvokeRefreshAction(cacheItem, removalReason);
        }
        public void RemoveItemFromCache(string key, EnumRemovedReason removalReason)
        {
            Remove(key, removalReason);
        }
        public object GetData(string key)
        {
            ValidateKey(key);
            CacheItem cacheItem = null;
            object obj;
            Cache.TryGetValue(key, out obj);
            cacheItem = (CacheItem)obj;
            if (IsObjectInCache(cacheItem))
            {
                return null;
            }
            if (cacheItem.HasExpired())
            {
                cacheItem.TouchedAction(true);
                object ob;
                Cache.TryRemove(key, out ob);
                RefreshActionInvoker.InvokeRefreshAction(cacheItem, EnumRemovedReason.Expired);
                return GetData(key);
            }
            cacheItem.TouchedAction(false);
            return cacheItem.Value;
        }
        private static void ValidateKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("EmptyParameterName", "key");
            }
        }
        private static bool IsObjectInCache(CacheItem cacheItem)
        {
            return cacheItem == null || Object.ReferenceEquals(cacheItem.Value, "");
        }
        ~ConcurrentCache()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
