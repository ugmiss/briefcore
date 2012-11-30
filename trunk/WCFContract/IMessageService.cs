using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFContract
{
    [ServiceContract(Namespace = "http://www.hujao.com/", SessionMode = SessionMode.Required, CallbackContract = typeof(ICallback))]
    public interface IMessageService
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(Guid uid,string msg);
    }
}
