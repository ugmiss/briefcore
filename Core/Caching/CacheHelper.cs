using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caching;

namespace System
{
    public static class CacheHelper
    {
        public static CacheManager MemCache = new CacheManager(new Cache());
    }
}
