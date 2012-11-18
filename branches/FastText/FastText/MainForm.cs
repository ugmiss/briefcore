using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using TelerikUsing;
using System.Diagnostics;
using Telerik.WinControls.Data;

namespace FastText
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        /// <summary>
        /// 逻辑。
        /// </summary>
        AssortInfoLogic astLogic = new AssortInfoLogic();
        MemLogic logic = new MemLogic();
        MemForm currentForm = null;
        /// <summary>
        /// 构造。
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Ctrl.Init(radGridView1);
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (currentForm != null)
                currentForm.Close();
            new MemForm().ShowDialog();
            ReLoad();
        }

        void ReLoadAst()
        {
            radDropDownList1.DataSource = astLogic.Get();
            radDropDownList1.DisplayMember = "assorttext";
            radDropDownList1.Text = "";
            radDropDownList1.SelectedIndex = -1;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Down();
            ReLoad();
            ReLoadAst();
            this.radGridView1.Columns["id"].IsVisible = false;
            this.radGridView1.Columns["title"].HeaderText = "标题";
            this.radGridView1.Columns["title"].MinWidth = 130;
            this.radGridView1.Columns["assort"].HeaderText = "类别";
            this.radGridView1.Columns["assort"].IsVisible = false;
            this.radGridView1.Columns["assort"].MinWidth = 80;
            this.radGridView1.Columns["lastaccessdate"].HeaderText = "更新时间";
            this.radGridView1.Columns["lastaccessdate"].MinWidth = 110;
            this.radGridView1.Columns["type"].IsVisible = false;
            this.radGridView1.Columns["tag"].IsVisible = false;
            this.radGridView1.Columns["files"].IsVisible = false;
            this.radGridView1.Columns["iscommon"].HeaderText = "重要";
            this.radGridView1.Columns["iscommon"].MaxWidth = 48;
            this.radGridView1.Columns["iscommon"].MinWidth = 48;
            this.radGridView1.Columns["context"].IsVisible = false;
            this.radGridView1.Columns["context"].IsPinned = false;
            this.radGridView1.Columns.Move(7, 0);
            this.radGridView1.GroupDescriptors.Add("assort", ListSortDirection.Ascending);
            chkExtend.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            //for (int i = 0; i < this.radGridView1.Groups.Count; i++)
            //{
            //    this.radGridView1.Groups[i].Expand();
            //}
        }


        void ReLoad()
        {
            MemLogic logic = new MemLogic();
            Mem[] mems = logic.Get();
            this.radGridView1.DataSource = mems;
            for (int i = 0; i < this.radGridView1.Groups.Count; i++)
            {
                this.radGridView1.Groups[i].Expand();
            }
        }

        void ReLoad(string key)
        {
            MemLogic logic = new MemLogic();
            Mem[] mems = logic.GetByString(key);
            this.radGridView1.DataSource = mems;
            for (int i = 0; i < this.radGridView1.Groups.Count; i++)
            {
                this.radGridView1.Groups[i].Expand();
            }
        }
        void ReLoadFullText(string key)
        {
            MemLogic logic = new MemLogic();
            Mem[] mems = logic.GetByFullText(key);
            this.radGridView1.DataSource = mems;
            for (int i = 0; i < this.radGridView1.Groups.Count; i++)
            {
                this.radGridView1.Groups[i].Expand();
            }
        }


        private void radGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows != null && radGridView1.SelectedRows.Count > 0)
            {
                Mem mem = radGridView1.SelectedRows[0].DataBoundItem as Mem;
                if (currentForm != null)
                    currentForm.Close();
                currentForm = new MemForm(mem);
                currentForm.Show();
                currentForm.FormClosed += new FormClosedEventHandler(form_FormClosed);
            }
        }

        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (txtSearch.Text != "")
                return;
            ReLoad();
        }

        private void btnAst_Click(object sender, EventArgs e)
        {
            new AssortForm().ShowDialog();
            ReLoadAst();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReLoad();
        }
        /// <summary>
        /// 位置。
        /// </summary>
        public void Down()
        {
            int i_Height = 0;
            int i_Weight = 0;
            IntPtr hWnd = FindWindow("Shell_TrayWnd", 0);
            RECT rc = new RECT();
            try
            {
                GetWindowRect(hWnd, ref rc);
                i_Height = rc.Bottom - rc.Top;
                i_Weight = rc.Right - rc.Left;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            this.DesktopLocation = new Point(width - this.Width - 1, height - this.Height - i_Height);
        }
        /// <summary>
        /// Api结构。
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        /// <summary>
        /// 调用外部API。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string strClassName, int nptWindowName);

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoadFullText(txtSearch.Text);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ReLoadFullText(txtSearch.Text);
            }
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (radDropDownList1.Text != "")
                ReLoad(radDropDownList1.Text);

            else
                ReLoad();
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            Process.Start("DataTransfer.exe");
        }

        private void chkImp_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkImp.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                radGridView1.FilterDescriptors.Add("iscommon", FilterOperator.IsEqualTo, 1);
                radGridView1.Show();
                radGridView1.Refresh();
            }
            else
            {
                radGridView1.FilterDescriptors.Clear();
                radGridView1.Show();
                radGridView1.Refresh();
            }
        }

        private void chkExtend_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkExtend.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                for (int i = 0; i < this.radGridView1.Groups.Count; i++)
                {
                    this.radGridView1.Groups[i].Expand();
                }
            }
            else
            {
                for (int i = 0; i < this.radGridView1.Groups.Count; i++)
                {
                    this.radGridView1.Groups[i].Collapse();
                }
            }
        }
    }
}
