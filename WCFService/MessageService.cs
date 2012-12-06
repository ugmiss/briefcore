using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFContract;
using System.Threading;
using System.Collections.Concurrent;
using WCFEntity;
using GameModel;

namespace WCFService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MessageService : IMessageService
    {
        /// <summary>
        /// 通道缓存
        /// </summary>
        static ConcurrentDictionary<Guid, ICallback> CallList = new ConcurrentDictionary<Guid, ICallback>();
        public void SendClientMessage(Guid clientid, WcfMsg msg)
        {
            if (msg.MsgType == MsgType.Login)
            {
                Player p = msg.Data.XmlDeserialize<Player>();
                ICallback ICallback = OperationContext.Current.GetCallbackChannel<ICallback>();
                CallList.TryAdd(clientid, ICallback);
                ICallback.PollServerMessage(clientid, new WcfMsg() { MsgType = MsgType.LoginOK });
            }
            else
            {
                foreach (var item in CallList.ToArray())
                {
                    msg.MsgTime = DateTime.Now;
                    try
                    {
                        item.Value.PollServerMessage(clientid, msg);
                    }
                    catch
                    {
                        ThreadPool.QueueUserWorkItem(o =>
                        {
                            ICallback ICallback;
                            CallList.TryRemove(item.Key, out ICallback);
                        });
                    }
                }
            }
        }
    }
}
