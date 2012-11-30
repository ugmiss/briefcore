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
            label1.Text = new Utility.Hanzi.CnNameFactory().GetBoyName();
            EndpointAddress endpointAddress = new EndpointAddress("net.tcp://localhost:1999/MessageService");
            InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
            MessageClient client = new MessageClient(instanceContext);
            client.Endpoint.Address = endpointAddress;
            client.SendMessage(gid, "Login");
        }
        Guid gid = Guid.NewGuid();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                EndpointAddress endpointAddress = new EndpointAddress("net.tcp://localhost:1999/MessageService");
                InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
                MessageClient client = new MessageClient(instanceContext);
                client.Endpoint.Address = endpointAddress;
                client.SendMessage(gid, label1.Text + ":" + textBox1.Text);
            }
        }

    }
}
