using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SlideWindowDemo
{
    public partial class Form1 : Form
    {
        BussinessExecuter exec = new BussinessExecuter("server=.;uid=sa;pwd=123456;database=master");
        BussinessExecuter dbexec = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 3;
        }

        private void btnInit_Click(object sender, EventArgs e)
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
            string sql = SqlTexts.RemoveDB(setting);
            txtLog.AppendText(sql);
            int x = exec.ExecuteNonQuery(sql);
            
            sql = SqlTexts.CreateDB(setting);
            txtLog.AppendText(sql);
            x = exec.ExecuteNonQuery(sql);

            dbexec = new BussinessExecuter("server=.;uid=sa;pwd=123456;database={0}".FormatWith(setting.DBName));

            sql = SqlTexts.CreatePartitionFunc(setting);
            txtLog.AppendText(sql);
            x = exec.ExecuteNonQuery(sql);



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
