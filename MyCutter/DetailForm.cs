using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace MyCutter
{
    public partial class DetailForm : RadForm
    {
        public ActionDetail CurActionDetail { get; set; }
        string BookName;
        public DetailForm(string BookName)
        {
            this.BookName = BookName;
            InitializeComponent();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            CurActionDetail = new ActionDetail();
            CurActionDetail.ID = Comb.NewStringGuid();
            CurActionDetail.TagX = txtX.Text.ParseTo<int>();
            CurActionDetail.ActionType = GetActionType(ddlBlockType.SelectedItem.Text);
            CurActionDetail.TagY = txtY.Text.ParseTo<int>();
            CurActionDetail.Replay = chkRePlay.Checked;
            CurActionDetail.Speed = txtSpeed.Text.ParseTo<int>();
            CurActionDetail.Angle = txtAngle.Text.ParseTo<int>();
            CurActionDetail.ActionResoure = txtResource.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        string GetActionType(string text)
        {
            switch (text)
            {
                case "移动": return "move";
                case "播放视频": return "playvideo";
                case "播放音频": return "playaudio";
            }
            return "";
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            string res = Common.SelectFile("all");
            string path = AppDomain.CurrentDomain.BaseDirectory + BookName + "\\resources\\";
            txtResource.Text = Common.AddResouce(BookName, res, path + Common.GetShortFileName(res));
        }

        private void PageForm_Load(object sender, EventArgs e)
        {
        }
    }
    public enum ActionType
    {
        Move,
        //播放视频
        PlayVideo,
        //播放声音
        PlayAudio
    }
}
