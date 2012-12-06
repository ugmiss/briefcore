using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Data.Linq.Mapping;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using System.Data.Linq;
using System.Xml.Linq;
using System.Threading;
using Telerik.WinControls.UI;
using DataAccess;
using TelerikUsing;
using Utility;


namespace DataTransfer
{
    public partial class TransferMainForm : Telerik.WinControls.UI.RadForm
    {
        public string TempDir = AppDomain.CurrentDomain.BaseDirectory + "Temp\\";
        public TransferMainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 确定。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButton4_Click(object sender, EventArgs e)
        {
            radWaitingBar1.Visible = true;
            this.radWaitingBar1.StartWaiting();
            this.radGroupBox1.Enabled = false;
            this.radButton1.Enabled = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    Trans();
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex);
                }
                this.radWaitingBar1.EndWaiting();
                radWaitingBar1.Visible = false;
                this.radGroupBox1.Enabled = true;
                this.radButton1.Enabled = true;
            });
        }
        /// <summary>
        /// 数据库备份还原。
        /// </summary>
        void Trans()
        {
            try
            {
                if (radRadioButton6.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    if (!string.IsNullOrEmpty(this.radDropDownList1.Text))
                    {
                        Logic.BackUp(Logic.GetConnStr(txtServer.Text, txtUid.Text, txtPwd.Text, txtDataBase.Text), TempDir);//备份
                        Utility.SharpZip.CreateZipFile(TempDir, this.txtDir.Text +"\\"+ this.radDropDownList1.Text);
                        foreach (string f in Directory.GetFiles(TempDir))
                            File.Delete(f);
                    }
                    return;
                }

                string filepath = GetFilePath();
                if (filepath == null) return;
                if (radRadioButton3.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    Logic.CreateDB(Logic.GetConnStr(txtServer.Text, txtUid.Text, txtPwd.Text, txtDataBase.Text), TempDir);//建库表
                    Logic.TransData(Logic.GetConnStr(txtServer.Text, txtUid.Text, txtPwd.Text, txtDataBase.Text), TempDir);//导数据
                }
                else if (radRadioButton5.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    Logic.CreateDB(Logic.GetConnStr(txtServer.Text, txtUid.Text, txtPwd.Text, txtDataBase.Text), TempDir);//建库表
                }
                else if (radRadioButton4.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    //string[] diff = Logic.VaryDataBase(txtServer.Text, txtUid.Text, txtPwd.Text, txtDataBase.Text);
                    //if (diff != null && diff.Length > 0)
                    //{
                    //    MsgBox.Show(string.Concat(diff));
                    //    return;
                    //}
                    Logic.TransData(Logic.GetConnStr(txtServer.Text, txtUid.Text, txtPwd.Text, txtDataBase.Text), TempDir);//导数据
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex);
            }
        }

        string GetFilePath()
        {
            string filepath = null;
            if (!string.IsNullOrEmpty(this.radDropDownList1.Text))
                filepath = this.radDropDownList1.Text;
            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    filepath = dialog.FileName;
                }
            }
            Directory.Delete(TempDir, true);

            if (!Directory.Exists(TempDir))
                Directory.CreateDirectory(TempDir);
            if (filepath == null) return null;
            Utility.SharpZip.UnZipFile(filepath, AppDomain.CurrentDomain.BaseDirectory + "Temp\\");
            return filepath;
        }

        private void TransferMainForm_Load(object sender, EventArgs e)
        {
            this.Icon = AppResource._24;
            radWaitingBar1.Visible = false;
            txtDir.Text = new Enviroment().GetTempDir();
            new Enviroment().SaveTempDir(txtDir.Text);
            this.radDropDownList1.DataSource = Directory.GetFiles(txtDir.Text, "*.zip");
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            if (DialogResult.OK == dia.ShowDialog())
            {
                this.txtDir.Text = dia.SelectedPath;
                new Enviroment().SaveTempDir(txtDir.Text);
                radRadioButton6_ToggleStateChanged(null, null);
            }
        }

        private void radRadioButton6_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (radRadioButton6.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                this.radDropDownList1.DataSource = null;
                this.radDropDownList1.Text = DateTime.Now.ToString("yyyy_MM_dd") + ".zip";
                radDropDownList1.CaseSensitive = false;
                radDropDownList1.DropDownStyle = RadDropDownStyle.DropDown;
            }
            else
            {
                radDropDownList1.Text = "";
                this.radDropDownList1.DataSource = Directory.GetFiles(this.txtDir.Text, "*.zip");
                radDropDownList1.CaseSensitive = true;
                radDropDownList1.DropDownStyle = RadDropDownStyle.DropDownList;
            }

        }
    }
}
