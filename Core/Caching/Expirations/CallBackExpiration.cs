using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caching;

namespace System.Caching.Expirations
{
    public class CallBackExpiration : ICacheItemExpiration
    {
        public bool IsExpired = false;
        public bool HasExpired()
        {
            return IsExpired;
        }
        public void Notify()
        {
        }
        public void Initialize(CacheItem owningCacheItem)
        {
        }
    }
}
