using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using TelerikUsing;

namespace FastText
{
    public partial class MemForm : Telerik.WinControls.UI.RadForm
    {
        /// <summary>
        /// 当前对象。
        /// </summary>
        public Mem Current;
        /// <summary>
        /// 逻辑。
        /// </summary>
        MemLogic logic = new MemLogic();
        /// <summary>
        /// 逻辑。
        /// </summary>
        AssortInfoLogic astLogic = new AssortInfoLogic();
        /// <summary>
        /// 构造。
        /// </summary>
        public MemForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造。
        /// </summary>
        /// <param name="mem"></param>
        public MemForm(Mem mem)
        {
            InitializeComponent();

            Current = logic.GetById(mem.Id);
        }
        /// <summary>
        /// 内容改变。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Current != null)
                Current.Context = this.richTextBox1.Rtf;
        }
        /// <summary>
        /// 标题改变。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void radTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Current != null)
                Current.Title = this.radTextBox1.Text;
        }
        /// <summary>
        /// 保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Current != null)
            {
                if (logic.GetById(Current.Id) != null)
                {
                    Current.Title = this.radTextBox1.Text;
                    Current.Context = this.richTextBox1.Rtf;
                    Current.Assort = radDropDownList1.Text;
                    Current.IsCommon = cbkCommon.Checked;
                   
                    logic.Update(Current);
                    logic.Seg(Current, this.richTextBox1.Text);

                    MsgBox.Show("保存完毕");

                }
                else
                {
                    logic.Seg(Current, this.richTextBox1.Text);
                    logic.Add(Current);
                    MsgBox.Show("保存完毕");
                }
            }
            else
            {
                Current = new Mem();
                Current.Id = Guid.NewGuid().ToString();
                Current.Title = this.radTextBox1.Text;
                Current.Context = this.richTextBox1.Rtf;
                Current.Assort = radDropDownList1.Text;
                Current.IsCommon = cbkCommon.Checked;
                if (astLogic.GetByText(radDropDownList1.Text) == null)
                {
                    AssortInfo info = new AssortInfo();
                    info.AssortId = Guid.NewGuid().ToString();
                    info.AssortText = radDropDownList1.Text;
                    astLogic.Add(info);
                }
                Current.LastAccessDate = DateTime.Now;
                MsgBox.Show("保存完毕");
                logic.Add(Current);
            }
            this.Close();
        }
        /// <summary>
        /// 删除。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (Current != null)
            {
                if (logic.GetById(Current.Id) != null)
                {
                    logic.Del(Current);
                    MsgBox.Show("删除完毕");
                }
            }
            this.Close();
        }
        /// <summary>
        /// 复制内容。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButton1_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Rtf != null)
            {
                Clipboard.Clear();
                Clipboard.SetData(DataFormats.StringFormat, this.richTextBox1.Text);
                this.Close();
            }
        }
        /// <summary>
        /// 加载。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemForm_Load(object sender, EventArgs e)
        {
            this.radTextBox1.TextChanged += new EventHandler(radTextBox1_TextChanged);
            this.richTextBox1.TextChanged += new EventHandler(richTextBox1_TextChanged);
            radDropDownList1.DataSource = astLogic.Get();
            radDropDownList1.DisplayMember = "assorttext";
            if (Current != null)
            {
                this.radTextBox1.Text = Current.Title;
                this.richTextBox1.Rtf = Current.Context;
                this.radDropDownList1.Text = Current.Assort;
                cbkCommon.Checked = Current.IsCommon;

            }
            richTextBox1.Select();
        }
    }
}
