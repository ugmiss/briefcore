using System;

namespace Caching
{
    /// <summary>
    /// CacheItem
    /// 缓存项的作用是对键值对数据的一层包装。
    /// 目的是给键值对加上过期策略和刷新策略
    /// 过期策略 是去让【数据】过期
    /// 刷新策略 是主动刷新【数据】，这不是必要的因为对于已经过期的数据，被动访问时可以重新拉取。
    /// </summary>
    public class CacheItem
    {
        private string key;
        private object data;
        private ICacheItemRefreshAction refreshAction;
        private ICacheItemExpiration[] expirations;
        private DateTime lastAccessedTime;
        public CacheItem(string key, object value, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            Initialize(key, value, refreshAction, expirations);
            TouchedAction(false);
            InitializeExpirations();
        }
        public CacheItem(DateTime lastAccessedTime, string key, object value, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            Initialize(key, value, refreshAction, expirations);
            TouchedAction(false, lastAccessedTime);
            InitializeExpirations();
        }
        internal void Replace(object cacheItemData, ICacheItemRefreshAction cacheItemRefreshAction, params ICacheItemExpiration[] cacheItemExpirations)
        {

            Initialize(this.key, cacheItemData, cacheItemRefreshAction, cacheItemExpirations);
            TouchedAction(false);
        }
        public DateTime LastAccessedTime
        {
            get { return lastAccessedTime; }
        }
        public object Value
        {
            get { return data; }
        }
        public string Key
        {
            get { return key; }
        }
        public ICacheItemRefreshAction RefreshAction
        {
            get { return refreshAction; }
        }
        public ICacheItemExpiration[] GetExpirations()
        {
            return (ICacheItemExpiration[])expirations.Clone();
        }
        public bool HasExpired()
        {
            foreach (ICacheItemExpiration expiration in expirations)
            {
                if (expiration.HasExpired())
                {
                    return true;
                }
            }
            return false;
        }
        public void TouchedAction(bool objectRemovedFromCache)
        {
            TouchedAction(objectRemovedFromCache, DateTime.Now);
        }
        internal void TouchedAction(bool objectRemovedFromCache, DateTime timestamp)
        {
            lastAccessedTime = timestamp;
            foreach (ICacheItemExpiration expiration in expirations)
            {
                expiration.Notify();
            }
        }
        private void InitializeExpirations()
        {
            foreach (ICacheItemExpiration expiration in expirations)
            {
                expiration.Initialize(this);
            }
        }
        private void Initialize(string cacheItemKey, object cacheItemData, ICacheItemRefreshAction cacheItemRefreshAction, ICacheItemExpiration[] cacheItemExpirations)
        {
            key = cacheItemKey;
            data = cacheItemData;
            refreshAction = cacheItemRefreshAction;
            if (cacheItemExpirations == null)
            {
                expirations = new ICacheItemExpiration[1] { new NeverExpiration() };
            }
            else
            {
                expirations = cacheItemExpirations;
            }
        }
    }
    public enum EnumRemovedReason
    {
        Expired,
        Removed,
        Unknown = 9999
    }
}
