using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SlideWindowDemo
{
    public partial class MainForm : Form
    {
        BussinessExecuter exec = new BussinessExecuter("server=.;uid=sa;pwd=123456;database=master");
        BussinessExecuter dbexec = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.comboBox1.SelectedIndex = 3;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            btnInit.Enabled = false;
            ThreadPool.QueueUserWorkItem(o =>
            {
                InitSetting setting = new InitSetting()
                {
                    DBName = txtDBName.Text,
                    DBPath = txtDBPath.Text,
                    PartitionCount = txtPartitionCount.Text.ParseTo<int>(),
                    IntervalType = (EnumIntervalType)comboBox1.SelectedIndex,
                    StartTime = txtStartTime.Text.ParseTo<DateTime>(),
                    Interval = txtInterval.Text.ParseTo<int>()
                };
                AppendDescriptionText("开始删除数据库");
                string sql = SqlTexts.RemoveDB(setting);
                AppendCodeText(sql);
                int x = exec.ExecuteNonQuery(sql);
                AppendDescriptionText("删除数据库完成");
                AppendDescriptionText("开始创建数据库");
                sql = SqlTexts.CreateDB(setting);
                AppendCodeText(sql);
                x = exec.ExecuteNonQuery(sql);
                AppendDescriptionText("创建数据库完成");
                dbexec = new BussinessExecuter("server=.;uid=sa;pwd=123456;database={0}".FormatWith(setting.DBName));
                AppendDescriptionText("开始删除分区函数");
                sql = (SqlTexts.RemovePartitionFunc(setting));
                AppendCodeText(sql);
                x = dbexec.ExecuteNonQuery(sql);
                AppendDescriptionText("删除分区函数完成");
                AppendDescriptionText("开始创建分区函数");
                sql = SqlTexts.CreatePartitionFunc(setting);
                AppendCodeText(sql);
                x = dbexec.ExecuteNonQuery(sql);
                AppendDescriptionText("创建分区函数完成");

                btnInit.Enabled = true;
            });
        }
        public void AppendCodeText(string text)
        {
            this.Append(txtLog, text, Color.Blue, Color.White);
        }
        public void AppendDescriptionText(string text)
        {
            this.Append(txtLog, "\n**********************" + text + "***********************\n", Color.Green, Color.White);
        }
        public void Append(RichTextBox box, string text, Color forecolor, Color backcolor)
        {
            int start = box.Text.Length;
            int len = text.Length;
            box.AppendText(text);
            box.Select(start, len);
            box.SelectionColor = forecolor;
            box.Select(start + len, 0);
            box.ScrollToCaret();
        }
    }


    public class InitSetting
    {
        public string DBName { get; set; }
        public string DBPath { get; set; }
        public int PartitionCount { get; set; }
        public DateTime StartTime { get; set; }
        public int Interval { get; set; }
        public EnumIntervalType IntervalType { get; set; }
    }
    public enum EnumIntervalType
    {
        Month, Day, Hour, Minute
    }
}
