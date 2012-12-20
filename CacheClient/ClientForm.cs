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
          //  dataGridView1.DataSource = ClientCore.Instance.GetUserData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            CacheBusiness.Model.UserInfo u = dataGridView1.SelectedRows[0].DataBoundItem as CacheBusiness.Model.UserInfo;
            ClientCore.Instance.DeleteUser(u);
           // dataGridView1.DataSource = ClientCore.Instance.GetUserData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ClientCore.Instance.GetUserData();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            CacheCallBack.OnOutTime += new OutTime(CacheCallBack_OnOutTime);
            dataGridView1.DataSource = ClientCore.Instance.GetUserData();
        }

        void CacheCallBack_OnOutTime(string typeName)
        {
            dataGridView1.DataSource = ClientCore.Instance.GetUserData();
        }
    }
}
