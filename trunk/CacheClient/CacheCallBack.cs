using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CacheBusiness;

namespace CacheClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CacheCallBack : ICacheRefreshCallback
    {
        public void CacheTimeOut(string TypeName)
        {
        }
    }
}
