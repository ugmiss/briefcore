using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Caching;

namespace System.Caching
{
    public class Cache
    {
        /// <summary>
        /// 并行缓存。
        /// </summary>
        ConcurrentDictionary<string, CacheItem> ConcurrentCache = new ConcurrentDictionary<string, CacheItem>();
      
        public bool Contains(string key)
        {
            ValidateKey(key);
            return ConcurrentCache.ContainsKey(key);
        }
        public void Add(string key, object value)
        {
            Add(key, value, null);
        }
        public void Add(string key, object value, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            ValidateKey(key);
            CacheItem cacheItem = null;
            if (ConcurrentCache.ContainsKey(key) == false)
            {
                cacheItem = new CacheItem(key,string.Empty, null);
                ConcurrentCache.TryAdd(key, cacheItem);
            }
            else
            {
                ConcurrentCache.TryGetValue(key, out cacheItem);
            }
            cacheItem.TouchedAction(true);
            //CacheItem newCacheItem = new CacheItem(key, value, refreshAction, expirations);
            try
            {
                cacheItem.Replace(value, refreshAction, expirations);
                ConcurrentCache[key] = cacheItem;
            }
            catch
            {
                CacheItem obj;
                ConcurrentCache.TryRemove(key, out obj);
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
            cacheItem = (CacheItem)ConcurrentCache[key];
            if (IsObjectInCache(cacheItem))
            {
                return;
            }
            cacheItem.TouchedAction(true);
            CacheItem obj;
            ConcurrentCache.TryRemove(key, out obj);
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
            ConcurrentCache.TryGetValue(key, out cacheItem);
            if (IsObjectInCache(cacheItem))
            {
                return null;
            }
            if (cacheItem.HasExpired())
            {
                cacheItem.TouchedAction(true);
                CacheItem ob;
                ConcurrentCache.TryRemove(key, out ob);
                RefreshActionInvoker.InvokeRefreshAction(cacheItem, EnumRemovedReason.Expired);
                return GetData(key);
            }
            cacheItem.TouchedAction(false);
            return cacheItem.Value;
        }

        public T GetData<T>(string key) where T : class
        {
            ValidateKey(key);
            CacheItem cacheItem = null;
            ConcurrentCache.TryGetValue(key, out cacheItem);
            if (IsObjectInCache(cacheItem))
            {
                return null;
            }
            if (cacheItem.HasExpired())
            {
                cacheItem.TouchedAction(true);
                CacheItem ob;
                ConcurrentCache.TryRemove(key, out ob);
                RefreshActionInvoker.InvokeRefreshAction(cacheItem, EnumRemovedReason.Expired);
                return (T)GetData(key);
            }
            cacheItem.TouchedAction(false);
            
            return (T)cacheItem.Value;
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
            return cacheItem == null || object.ReferenceEquals(cacheItem.Value, string.Empty);
        }
    }
}
