using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CacheBusiness
{
    public interface ICacheRefreshCallback
    {
        [OperationContract(IsOneWay = true)]
        void CacheTimeOut(string TypeName);
    }
}
