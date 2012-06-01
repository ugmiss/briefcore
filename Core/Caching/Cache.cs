using System;
using System.Collections;
using System.Threading;
using System.Data.SqlClient;

namespace Caching
{
    public class Cache : IDisposable
    {
        private Hashtable memCache;
        private const string AddingFlag = "添加缓存项时的标识";
        public Cache()
        {
            memCache = Hashtable.Synchronized(new Hashtable());
        }
        public int Count
        {
            get { return memCache.Count; }
        }
        public Hashtable CurrentCacheClone
        {
            get { return (Hashtable)memCache.Clone(); }
        }
        public bool Contains(string key)
        {
            ValidateKey(key);
            return memCache.Contains(key);
        }
        public void Add(string key, object value)
        {
            Add(key, value, null);
        }
        public void Add(string key, object value, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            ValidateKey(key);
            CacheItem cacheItem = null;
            bool lockSuccess = false;
            do
            {
                lock (memCache.SyncRoot)
                {
                    if (memCache.Contains(key) == false)
                    {
                        cacheItem = new CacheItem(key, AddingFlag, null);
                        memCache[key] = cacheItem;
                    }
                    else
                    {
                        cacheItem = (CacheItem)memCache[key];
                    }

                    lockSuccess = Monitor.TryEnter(cacheItem);
                }

                if (lockSuccess == false)
                {
                    Thread.Sleep(0);
                }
            } while (lockSuccess == false);

            try
            {
                cacheItem.TouchedAction(true);
                CacheItem newCacheItem = new CacheItem(key, value, refreshAction, expirations);
                try
                {
                    cacheItem.Replace(value, refreshAction, expirations);
                    memCache[key] = cacheItem;
                }
                catch
                {
                    memCache.Remove(key);
                    throw;
                }
            }
            finally
            {
                Monitor.Exit(cacheItem);
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
            bool lockSuccess;
            do
            {
                lock (memCache.SyncRoot)
                {
                    cacheItem = (CacheItem)memCache[key];

                    if (IsObjectInCache(cacheItem))
                    {
                        return;
                    }

                    lockSuccess = Monitor.TryEnter(cacheItem);
                }

                if (lockSuccess == false)
                {
                    Thread.Sleep(0);
                }
            } while (lockSuccess == false);

            try
            {
                cacheItem.TouchedAction(true);
                memCache.Remove(key);
                RefreshActionInvoker.InvokeRefreshAction(cacheItem, removalReason);
            }
            finally
            {
                Monitor.Exit(cacheItem);
            }
        }
        public void RemoveItemFromCache(string key, EnumRemovedReason removalReason)
        {
            Remove(key, removalReason);
        }
        public object GetData(string key)
        {
            ValidateKey(key);
            CacheItem cacheItem = null;
            bool lockSuccess = false;
            do
            {
                lock (memCache.SyncRoot)
                {
                    cacheItem = (CacheItem)memCache[key];
                    if (IsObjectInCache(cacheItem))
                    {
                        return null;
                    }
                    lockSuccess = Monitor.TryEnter(cacheItem);
                }
                if (lockSuccess == false)
                {
                    Thread.Sleep(0);
                }
            } while (lockSuccess == false);
            try
            {
                if (cacheItem.HasExpired())
                {
                    cacheItem.TouchedAction(true);
                    memCache.Remove(key);
                    RefreshActionInvoker.InvokeRefreshAction(cacheItem, EnumRemovedReason.Expired);
                    return GetData(key);
                }
                cacheItem.TouchedAction(false);
                return cacheItem.Value;
            }
            finally
            {
                Monitor.Exit(cacheItem);
            }
        }
        public void Flush()
        {
        RestartFlushAlgorithm:
            lock (memCache.SyncRoot)
            {
                foreach (string key in memCache.Keys)
                {
                    bool lockSuccess = false;
                    CacheItem itemToRemove = (CacheItem)memCache[key];
                    try
                    {
                        if (lockSuccess = Monitor.TryEnter(itemToRemove))
                        {
                            itemToRemove.TouchedAction(true);
                        }
                        else
                        {
                            goto RestartFlushAlgorithm;
                        }
                    }
                    finally
                    {
                        if (lockSuccess) Monitor.Exit(itemToRemove);
                    }
                }
                int countBeforeFlushing = memCache.Count;
                memCache.Clear();
            }
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
            return cacheItem == null || Object.ReferenceEquals(cacheItem.Value, AddingFlag);
        }
        ~Cache()
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
    public enum EnumRemovedReason
    {
        Expired,
        Removed,
        Unknown = 9999
    }
}
