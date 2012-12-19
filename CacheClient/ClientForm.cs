using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace CacheClient
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientCore.Instance.AddUser(new CacheBusiness.Model.UserInfo() { ID = Guid.NewGuid().ToString(), Name = CnNameGenerator.GetBoyName(), Age = RandomFactory.Next(20) + 20 });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientCore.Instance.AddUser(new CacheBusiness.Model.UserInfo() { ID = Guid.NewGuid().ToString(), Name = CnNameGenerator.GetBoyName(), Age = RandomFactory.Next(20) + 20 });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientCore.Instance.GetUserData();
        }
    }
}
