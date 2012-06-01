using System;

namespace Caching
{
    public class SlidingTimeExpiration : ICacheItemExpiration
    {
        private DateTime timeLastUsed;
        private TimeSpan itemSlidingExpiration;
        public SlidingTimeExpiration(TimeSpan slidingExpiration)
        {
            if (!(slidingExpiration.TotalSeconds >= 1))
            {
                throw new ArgumentOutOfRangeException("slidingExpiration",
                                                      "Sliding time should be greater than or equal to 1s.");
            }
            this.itemSlidingExpiration = slidingExpiration;
        }
        public SlidingTimeExpiration(TimeSpan slidingExpiration, DateTime originalTimeStamp)
            : this(slidingExpiration)
        {
            timeLastUsed = originalTimeStamp;
        }
        public TimeSpan ItemSlidingExpiration
        {
            get { return itemSlidingExpiration; }
        }
        public DateTime TimeLastUsed
        {
            get { return timeLastUsed; }
        }
        public bool HasExpired()
        {
            bool expired = CheckSlidingExpiration(DateTime.Now, this.timeLastUsed, this.itemSlidingExpiration);
            return expired;
        }
        public void Notify()
        {
            this.timeLastUsed = DateTime.Now;
        }
        public void Initialize(CacheItem owningCacheItem)
        {
            if (owningCacheItem == null) throw new ArgumentNullException("owningCacheItem");
            timeLastUsed = owningCacheItem.LastAccessedTime;
        }
        private static bool CheckSlidingExpiration(DateTime nowDateTime, DateTime lastUsed, TimeSpan slidingExpiration)
        {
            // Convert to UTC in order to compensate for time zones
            DateTime tmpNowDateTime = nowDateTime.ToUniversalTime();
            // Convert to UTC in order to compensate for time zones
            DateTime tmpLastUsed = lastUsed.ToUniversalTime();
            long expirationTicks = tmpLastUsed.Ticks + slidingExpiration.Ticks;
            bool expired = (tmpNowDateTime.Ticks >= expirationTicks) ? true : false;
            return expired;
        }
    }
}
