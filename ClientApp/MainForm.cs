using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entity;
using IService;

namespace ClientApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public MainForm()
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            // TODO: Add any constructor code after InitializeComponent call
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            IRemoteLogic remoteLogic = Activator.GetObject(typeof(IRemoteLogic), @"http://localhost:7012/serverlogic.rem") as IRemoteLogic;
            UserInfo u=remoteLogic.getUserInfo();
            this.label1.Text = u.Name;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            IRemoteLogic remoteLogic = Activator.GetObject(typeof(IRemoteLogic), "tcp://localhost:12345/serverlogic.rem") as IRemoteLogic;
            UserInfo u = remoteLogic.getUserInfo();
            this.label1.Text = u.Password;

        }
    }
}