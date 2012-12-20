using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;
using CacheBusiness;

namespace CacheWCFService
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                
                using (ServiceHost host = new ServiceHost(typeof(CacheWCFService)))
                {
                    host.Open();
                    while (true)
                    {

                    }
                }
            });
        }
    }
}
