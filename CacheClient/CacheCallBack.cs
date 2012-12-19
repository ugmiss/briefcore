using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Caching.Expirations;
using System.ServiceModel;
using CacheBusiness;


namespace CacheClient
{
    [System.ServiceModel.CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CacheCallBack : ICacheRefreshCallback
    {
        public void CacheTimeOut(string TypeName)
        {
            CallBackExpiration.TimeOut[TypeName] = true;
        }
    }
}
