using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WCFClient;
using WCFClient.WCFService;
using GameModel;

namespace GameCenter
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public Player player { get; set; }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            player = new Player();
            player.UserName = txtUserName.Text;
            player.Password = txtPass.Text;
            player.ClientID = ClientManager.ClientID;
            ClientManager.Instance.SendClientMessage(MsgType.Login, null, player.XmlSerialize(), player.GetType());
        }

        void Instance_OnReceiveFromServer(Guid clientid, WcfMsg msg)
        {
            if (msg.MsgType == MsgType.LoginOK)
            {
                this.Close();
                ClientCache.CurrentPlayer = player;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ClientManager.Instance.OnReceiveFromServer += new OnReceiveFromServer(Instance_OnReceiveFromServer);
        }
    }
}
