using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFContract;
using System.Threading;
using System.Collections.Concurrent;

namespace WCFService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MessageService : IMessageService
    {
        static ConcurrentDictionary<Guid, ICallback> CallList = new ConcurrentDictionary<Guid, ICallback>();
        public void SendMessage(Guid uid, string msg)
        {
            if (msg == "Login")
            {
                CallList.TryAdd(uid, OperationContext.Current.GetCallbackChannel<ICallback>());
            }
            else
            {
                foreach (var item in CallList.ToArray())
                {
                    item.Value.BroadCast(msg);
                }
            }
        }
    }
}
