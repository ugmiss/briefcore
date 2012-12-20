using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Caching.Expirations;
using System.ServiceModel;
using CacheBusiness;
using CacheClient.ServiceReference1;


namespace CacheClient
{

    public delegate void OutTime(string typeName);
    [System.ServiceModel.CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CacheCallBack : ICacheWCFCallback
    {
        public static event OutTime OnOutTime;
        public void CacheTimeOut(string TypeName)
        {
            CallBackExpiration.TimeOut[TypeName] = true;
            if (OnOutTime != null)
            {
                OnOutTime(TypeName);
            }
        }
    }
}
