using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFContract;
using System.ServiceModel;

namespace WCFClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MessageServiceCallback : ICallback
    {
        public void BroadCast(string msg)
        {
            Environment.MessageForm.richTextBox1.AppendText(msg + "\n");
        }
    }
    public class MessageClient : DuplexClientBase<IMessageService>, IMessageService
    {
        public MessageClient(InstanceContext instance) : base(instance) { }

        public void SendMessage(Guid uid, string msg)
        {
            base.Channel.SendMessage(uid, msg);
        }
    }
}
