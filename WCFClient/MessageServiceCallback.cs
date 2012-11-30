using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFContract;
using System.ServiceModel;
using WCFClient.WCFService;

namespace WCFClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MessageServiceCallback : WCFClient.WCFService.IMessageServiceCallback
    {
        public void PollServerMessage(Guid clientid, WcfMsg msg)
        {
            Environment.MessageForm.richTextBox1.AppendText(msg.MsgTime.ToString("hh:mm:ss ")+msg.ClientName + ":" + msg.Content + "\n");
        }
    }
}
