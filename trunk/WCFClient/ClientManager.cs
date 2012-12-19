using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFClient.WCFService;

namespace WCFClient
{
    public class ClientManager
    {
         

        public static readonly Guid ClientID = Guid.NewGuid();
        public event OnReceiveFromServer OnReceiveFromServer;
        public void SendClientMessage(WcfMsg msg)
        {
            InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
            MessageServiceClient client = new MessageServiceClient(instanceContext);
            client.SendClientMessage(ClientID, msg);
        }
        public void SendClientMessage(MsgType msgType, string Content, string Data, Type dataType)
        {
            InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
            MessageServiceClient client = new MessageServiceClient(instanceContext);
            WcfMsg msg = new WcfMsg() { ID = Guid.NewGuid(), MsgType = msgType, Content = Content, Data = Data, DataType = dataType.ToString() };
            client.SendClientMessage(ClientID, msg);
        }


        public void ReceiveFromServer(Guid clientid, WcfMsg msg)
        {
            if (OnReceiveFromServer != null)
            {
                OnReceiveFromServer(clientid, msg);
            }
        }
    }

    public delegate void OnReceiveFromServer(Guid clientid, WcfMsg msg);
}
