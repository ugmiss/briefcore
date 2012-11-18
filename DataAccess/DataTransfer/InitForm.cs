using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.IO;

namespace DataTransfer
{
    public partial class InitForm : Telerik.WinControls.UI.RadForm
    {
        public InitForm()
        {
            InitializeComponent();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            if (DialogResult.OK == dia.ShowDialog())
            {
                this.txtDir.Text = dia.SelectedPath;
                new Enviroment().SaveTempDir(txtDir.Text);
            }
        }
    }
}
