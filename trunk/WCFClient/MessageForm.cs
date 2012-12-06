using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using WCFContract;
using WCFClient.WCFService;
using Utility;

namespace WCFClient
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            MessageServiceClient client;
            try
            {
                label1.Text = CnNameFactory.GetBoyName();
                InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
                client = new MessageServiceClient(instanceContext);
                WcfMsg msg = new WcfMsg() { ID = Guid.NewGuid(), MsgType = MsgType.Login };
                client.SendClientMessage(clientid, msg);
            }
            catch
            {
                client.Abort();
            }
        }
        Guid clientid = Guid.NewGuid();
        private void button1_Click(object sender, EventArgs e)
        {
            Send();
        }

        void Send()
        {
            if (textBox1.Text != "")
            {
                InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
                MessageServiceClient client = new MessageServiceClient(instanceContext);
                WcfMsg msg = new WcfMsg() { ID = Guid.NewGuid(), MsgType = MsgType.AllChat, ClientName = label1.Text, Content = textBox1.Text };
                client.SendClientMessage(clientid, msg);
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send();
            }
        }

    }
}
