using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caching;

namespace System.Caching.Expirations
{
    public class CallBackExpiration : ICacheItemExpiration
    {
        public CallBackExpiration(string typname)
        {
            Typename = typname;
        }
        public static Dictionary<string, bool> TimeOut = new Dictionary<string, bool>();
        public string Typename { get; set; }
        public bool HasExpired()
        {
            return TimeOut[Typename];
        }
        public void Notify()
        {
        }
        public void Initialize(CacheItem owningCacheItem)
        {
        }
    }
}
