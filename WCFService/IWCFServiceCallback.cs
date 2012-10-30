using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFService
{
    public interface IWCFServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void StateChanged(string State);
    }
}
