using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFEntity;

namespace WCFContract
{
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void PollServerMessage(Guid clientid, WcfMsg msg);
    }
}
