using System;

namespace Caching
{
    public class NeverExpiration : ICacheItemExpiration
    {
        public bool HasExpired()
        {
            return false;
        }
        public void Notify()
        {
        }
        public void Initialize(CacheItem owningCacheItem)
        {
        }
    }
}
