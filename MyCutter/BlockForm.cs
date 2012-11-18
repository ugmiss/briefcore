using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.IO;

namespace MyCutter
{
    /// <summary>
    /// 块配置窗体。
    /// </summary>
    public partial class BlockForm : RadForm
    {
        public string BookName { get; set; }
        public Block Block { get; set; }
        public List<ActionDetail> ActionList = new List<ActionDetail>();
        public BlockForm(string BookName)
        {
            this.BookName = BookName;
            InitializeComponent();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            Block = new Block();
            Block.Height = txtHeight.Text.ParseTo<int>();
            Block.Width = txtWidth.Text.ParseTo<int>();
            Block.X = txtX.Text.ParseTo<int>();
            Block.Y = txtY.Text.ParseTo<int>();
            Block.BlockResource = txtBackground.Text;
            Block.BlockType = GetBlockType(ddlBlockType.SelectedItem.Text);
            Block.Actions = ActionList;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        string GetBlockType(string text)
        {
            switch (text)
            {
                case "图片": return "Pic";
                case "文字": return "Text";
                case "视频": return "Video";

            }
            return "";
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 添加动作。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButton6_Click(object sender, EventArgs e)
        {
            DetailForm df = new DetailForm(BookName);
            df.ShowDialog();
            if (df.CurActionDetail != null)
            {
                ActionList.Add(df.CurActionDetail);
                ReLoad();
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            string src = Common.SelectFile("all");
            string path = AppDomain.CurrentDomain.BaseDirectory + BookName + "\\resources\\";
            txtBackground.Text = Common.AddResouce(BookName, src, path + Common.GetShortFileName(src));
        }

        private void radButton5_Click(object sender, EventArgs e)
        {

        }

        private void BlockForm_Load(object sender, EventArgs e)
        {
            this.radGridView1.Init();

            txtX.Text = Block.X.ToString();
            txtY.Text = Block.Y.ToString();
            txtHeight.Text = Block.Height.ToString();
            txtWidth.Text = Block.Width.ToString();
        }

        void ReLoad()
        {
            this.radGridView1.DataSource = ActionList.ToArray();
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("ActionType", "类型");
            map.Add("TagX", "x");
            map.Add("TagY", "y");
            map.Add("Speed", "速度");
            map.Add("Angle", "角度");
            map.Add("Replay", "是否循环");
            map.Add("ActionResoure", "文件");
            this.radGridView1.InitGridColumns(map);
            this.radGridView1.Refresh();
        }

        private void radButton8_Click(object sender, EventArgs e)
        {

        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows != null)
            {
                ActionDetail ad = radGridView1.SelectedRows[0].DataBoundItem as ActionDetail;
                ActionList.Remove(ActionList.Find(p => p.ID == ad.ID));
                ReLoad();
            }
        }


    }
}
