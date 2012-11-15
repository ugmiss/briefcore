using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace SlideWindowDemo
{
    public partial class MainForm : Form
    {
        public const string master = @"server=.;uid=sa;pwd=123456;database=master";

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
                using (BussinessExecuter exec = new BussinessExecuter(master))
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
                    if (!Directory.Exists(setting.DBPath))
                        Directory.CreateDirectory(setting.DBPath);
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
                    using (BussinessExecuter dbexec = new BussinessExecuter("server=.;uid=sa;pwd=123456;database={0}".FormatWith(setting.DBName)))
                    {
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

                        AppendDescriptionText("开始创建分区架构");
                        sql = SqlTexts.CreatePartitionScheme(setting);
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区架构完成");

                        AppendDescriptionText("开始创建分区表");
                        sql = SqlTexts.CreatePartitionTable(setting, "Orders");
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区表完成");

                        AppendDescriptionText("开始创建分区备份表");
                        sql = SqlTexts.CreatePartitionTable(setting, "OrdersBak");
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区备份表完成");


                        AppendDescriptionText("开始创建分区索引");
                        sql = SqlTexts.CreatePartitionTableIndex(setting, "Orders");
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区索引完成");

                        AppendDescriptionText("开始创建分区备份索引");
                        sql = SqlTexts.CreatePartitionTableIndex(setting, "OrdersBak");
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区备份索引完成");
                    }
                }
                btnInit.Enabled = true;
            });
        }
        public void AppendCodeText(string text)
        {
            this.Append(txtLog, text + @"
go", Color.Blue, Color.White);
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

        bool Adding = false;

        private void btnData_Click(object sender, EventArgs e)
        {
            if (Adding)
            {
                btnData.Text = "模拟数据";
                Adding = false;
                return;
            }

            if (!Adding)
            {
                btnData.Text = "正在模拟..";
                Adding = true;
            }
            ThreadPool.QueueUserWorkItem(o =>
            {
                BussinessExecuter be = new BussinessExecuter("server=.;uid=sa;pwd=123456;database=DataCenter");
                be.DeleteAll<Orders>();
                while (Adding)
                {
                    Random r = new Random(Guid.NewGuid().GetHashCode());
                    //int day = r.Next(30) + 1;
                    //DateTime d = new DateTime(2012, 11, day);
                    Thread.Sleep(6);
                    Orders or = new Orders()
                    {
                        OrderDate = DateTime.Now,
                        ProductCount = r.Next(1000),
                        ProductName = new Utility.Hanzi.CnNameFactory().GetBoyName(),
                        ProductPrice = r.NextDouble() + r.Next(500)
                    };
                    be.Add<Orders>(or);
                }
                btnData.Text = "模拟数据";
            });
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
    [Description("IsTable")]
    public class Orders
    {
        [Description("DataField,IsPrimaryKey,IsIdentity")]
        public int OrderID { set; get; }
        [Description("DataField")]
        public DateTime OrderDate { set; get; }
        [Description("DataField")]
        public string ProductName { set; get; }
        [Description("DataField")]
        public int ProductCount { set; get; }
        [Description("DataField")]
        public double ProductPrice { set; get; }
    }
}
