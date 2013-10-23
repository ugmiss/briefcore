using System;

namespace Caching
{
    public class AbsoluteTimeExpiration : ICacheItemExpiration
    {
        private DateTime absoluteExpirationTime;
        public AbsoluteTimeExpiration(DateTime absoluteTime)
        {
            if (absoluteTime > DateTime.Now)
            {
                this.absoluteExpirationTime = absoluteTime.ToUniversalTime();
            }
            else
            {
                throw new ArgumentOutOfRangeException("absoluteTime", "绝对时间不能小于当前时间");
            }
        }
        public DateTime AbsoluteExpirationTime
        {
            get { return absoluteExpirationTime; }
        }
        public AbsoluteTimeExpiration(TimeSpan timeFromNow)
            : this(DateTime.Now + timeFromNow)
        {
        }
        public bool HasExpired()
        {
            DateTime nowDateTime = DateTime.Now.ToUniversalTime();
            return nowDateTime.Ticks >= this.absoluteExpirationTime.Ticks;
        }
        public void Notify()
        {
        }
        public void Initialize(CacheItem owningCacheItem)
        {
        }
    }
}
