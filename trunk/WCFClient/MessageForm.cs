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
            InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
            MessageServiceClient client = new MessageServiceClient(instanceContext);
            client.SendMessage(gid, "Login");
        }
        Guid gid = Guid.NewGuid();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                InstanceContext instanceContext = new InstanceContext(new MessageServiceCallback());
                MessageServiceClient client = new MessageServiceClient(instanceContext);
                client.SendMessage(gid, label1.Text + ":" + textBox1.Text);
            }
        }

    }
}
