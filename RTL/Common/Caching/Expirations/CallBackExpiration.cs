using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caching;
using System.Collections.Concurrent;

namespace System.Caching.Expirations
{
    public class CallBackExpiration : ICacheItemExpiration
    {
        public CallBackExpiration(string typname)
        {
            Typename = typname;
            bool a;
            if (TimeOut.ContainsKey(typname))
                TimeOut.TryRemove(typname, out a);
            TimeOut.TryAdd(typname, false);
        }
        public static ConcurrentDictionary<string, bool> TimeOut = new ConcurrentDictionary<string, bool>();
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
