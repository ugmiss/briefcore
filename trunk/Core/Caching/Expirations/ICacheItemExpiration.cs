namespace Caching
{
    public interface ICacheItemExpiration
    {
        bool HasExpired();
        void Notify();
        void Initialize(CacheItem owningCacheItem);
    }
}
