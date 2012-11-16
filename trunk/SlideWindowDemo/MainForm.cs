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
        public const string master = @"server={0};uid={1};pwd={2};database=master";
        delegate void AppendMessageCallback(RichTextBox box, string text, Color forecolor, Color backcolor);
        bool Adding = false;
        string[] rot = "♫, ♬ ,♪ ,♩ ,♭ ,♪".Split(",".ToArray());
        int c = 0;
        string curr = "";
        System.Windows.Forms.Timer tadd = new System.Windows.Forms.Timer();
        bool moniting = false;
        object syncdt = new object();
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer tauto = new System.Windows.Forms.Timer();
        InitSetting setting = null;
        object syn = new object();

        public MainForm()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();
            tt.Interval = 1000;
            tt.Tick += delegate { this.labeltime.Text = DateTime.Now.ToString(SystemKeys.SqlDateTime); };
            tt.Start();

            this.txtScript.Text = @"if  exists (select name from sys.objects where name = N'{0}')
drop table {0}
create table [dbo].{0}(
OrderID int  not null  identity(1,1),
{2} datetime not null,
ProductName nvarchar(20) not null,
ProductPrice decimal(18,2) not null,
ProductCount int not null
)  on [{1}_Partition_Scheme] ({2});
--参数依次为：表名，库名，分区列名";
            Control.CheckForIllegalCrossThreadCalls = false;
            this.comboBox1.SelectedIndex = 4;
            this.txtStartTime.Text = DateTime.Now.ToString(SystemKeys.SqlDateTime);
            this.dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            setting = new InitSetting()
            {
                Server = txtServer.Text,
                UID = txtUid.Text,
                Pwd = txtPwd.Text,
                TName = txtTable.Text,
                ColName = txtCol.Text,
                TScript = txtScript.Text,

                DBName = txtDBName.Text,
                DBPath = txtDBPath.Text,
                PartitionCount = txtPartitionCount.Text.ParseTo<int>(),
                IntervalType = (EnumIntervalType)comboBox1.SelectedIndex,
                StartTime = txtStartTime.Text.ParseTo<DateTime>(),
                Interval = txtInterval.Text.ParseTo<int>()
            };
        }
        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            curr = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
        void btnInit_Click(object sender, EventArgs e)
        {
            btnInit.Enabled = false;
            ThreadPool.QueueUserWorkItem(o =>
            {
                using (BussinessExecuter exec = new BussinessExecuter(master.FormatWith(setting.Server, setting.UID, setting.Pwd)))
                {
                    setting = new InitSetting()
                    {
                        Server = txtServer.Text,
                        UID = txtUid.Text,
                        Pwd = txtPwd.Text,
                        TName = txtTable.Text,
                        ColName = txtCol.Text,
                        TScript = txtScript.Text,
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
                    using (BussinessExecuter dbexec = new BussinessExecuter("server={0};uid={1};pwd={2};database={3}".FormatWith(setting.Server, setting.UID, setting.Pwd, setting.DBName)))
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
                        sql = SqlTexts.CreatePartitionTable(setting, false);
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区表完成");

                        AppendDescriptionText("开始创建分区备份表");
                        sql = SqlTexts.CreatePartitionTable(setting, true);
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区备份表完成");


                        AppendDescriptionText("开始创建分区索引");
                        sql = SqlTexts.CreatePartitionTableIndex(setting, false);
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区索引完成");

                        AppendDescriptionText("开始创建分区备份索引");
                        sql = SqlTexts.CreatePartitionTableIndex(setting, true);
                        AppendCodeText(sql);
                        x = dbexec.ExecuteNonQuery(sql);
                        AppendDescriptionText("创建分区备份索引完成");


                    }
                }
                //btnInit.Enabled = true;
                //this.btnData_Click(null, null);
            });
        }
        void btnData_Click(object sender, EventArgs e)
        {
            if (Adding)
            {
                btnData.Text = "模拟数据";
                Adding = false;
                tadd.Stop();
                return;
            }

            if (!Adding)
            {
                tadd.Interval = 1000;
                tadd.Tick += delegate
                {
                    c++;
                    btnData.Text = "正在模拟" + rot[c % 6];
                };
                tadd.Start();
                Adding = true;
            }
            ThreadPool.QueueUserWorkItem(o =>
            {
                BussinessExecuter be = new BussinessExecuter("server={0};uid={1};pwd={2};database={3}".FormatWith(setting.Server, setting.UID, setting.Pwd, setting.DBName));
                //be.DeleteAll<Orders>();
                while (Adding)
                {
                    lock (syn)
                    {
                        Random r = new Random(Guid.NewGuid().GetHashCode());
                        Orders or = new Orders()
                        {
                            CreateTime = DateTime.Now,
                            ProductCount = r.Next(1000),
                            ProductName = new Utility.Hanzi.CnNameFactory().GetBoyName(),
                            ProductPrice = r.NextDouble() + r.Next(500)
                        };
                        be.Add<Orders>(or);
                        Append(txtLog, string.Format("{0} - {1} - {2} - {3}\n", or.CreateTime.ToString(SystemKeys.SqlDateTime)
                        , or.ProductName, or.ProductPrice, or.ProductCount), Color.Red, Color.White);
                    }
                }
            });
        }
        void btnMonitor_Click(object sender, EventArgs e)
        {
            if (moniting)
            {
                moniting = false;
                btnMonitor.Text = "监测表空间";
                t.Stop();
                return;
            }
            if (!moniting)
            {
                btnMonitor.Text = "监测中..";
                moniting = true;
            }
            if (moniting)
            {
                t.Interval = 1000;
                t.Tick += delegate
                {
                    lock (syncdt)
                    {
                        BussinessExecuter be = new BussinessExecuter("server={0};uid={1};pwd={2};database={3}".FormatWith(setting.Server, setting.UID, setting.Pwd, setting.DBName));
                        DataTable dt = be.QueryDataTable(SqlTexts.GetInfo(setting));
                        dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
                        this.dataGridView1.DataSource = dt;
                        this.dataGridView1.Refresh();
                        foreach (DataGridViewRow r in dataGridView1.Rows)
                        {
                            if (r.Cells[0].Value != null)
                                if (r.Cells[0].Value.ToString() == curr)
                                {
                                    r.Selected = true;
                                    break;
                                }
                        }
                        dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
                    }
                };
                t.Start();
                return;
            }
        }
        void btnAch_Click(object sender, EventArgs e)
        {
            using (BussinessExecuter be = new BussinessExecuter("server={0};uid={1};pwd={2};database={3}".FormatWith(setting.Server, setting.UID, setting.Pwd, setting.DBName)))
            {
                AppendDescriptionText("开始Switch 1");
                string sql = SqlTexts.Switch1(setting.TName, setting.TNameBak);
                be.ExecuteNonQuery(sql);
                AppendCodeText(sql);
                AppendDescriptionText("Switch 1 完成");

                AppendDescriptionText("开始Switch 2");
                sql = SqlTexts.Switch2(setting.TName, setting.TNameBak);
                be.ExecuteNonQuery(sql);
                AppendCodeText(sql);
                AppendDescriptionText("Switch 2 完成");

                DataTable dt = be.QueryDataTable(SqlTexts.GetInfo(setting));
                Dictionary<DateTime, string> datelist = new Dictionary<DateTime, string>();
                string NullGroup = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string s = dr.Field<string>("range_boundary");
                    if (s.NotNullOrEmpty())
                    {
                        datelist.Add(s.ParseTo<DateTime>(), dr.Field<string>("filegroup"));
                    }
                    else
                    {
                        NullGroup = dr.Field<string>("filegroup");
                    }
                }
                AppendDescriptionText("开始导出归档并清理临时库");
                sql = "TRUNCATE TABLE OrdersBak";
                be.ExecuteNonQuery(sql);
                AppendCodeText(sql);
                AppendDescriptionText("导出归档并清理临时库完成");

                DateTime dtstart = datelist.Keys.OrderBy(p => p).First();
                string group2 = datelist[dtstart];
                DateTime dtend = datelist.Keys.OrderByDescending(p => p).First();

                AppendDescriptionText("开始Merge");
                sql = SqlTexts.Merge(setting, dtstart);
                be.ExecuteNonQuery(sql);
                AppendCodeText(sql);
                AppendDescriptionText("Merge完成");

                AppendDescriptionText("开始设置NextUse");
                sql = SqlTexts.NextUsed(setting, group2);
                be.ExecuteNonQuery(sql);
                AppendCodeText(sql);
                AppendDescriptionText("设置NextUse完成");

                AppendDescriptionText("开始Split");
                sql = SqlTexts.Split(setting, dtend);
                be.ExecuteNonQuery(sql);
                AppendCodeText(sql);
                AppendDescriptionText("Split完成");
            }
        }

        void AppendCodeText(string text)
        {
            this.Append(txtLog, text + @"
go", Color.Blue, Color.White);
        }
        void AppendDescriptionText(string text)
        {
            this.Append(txtLog, "\n**********************" + text + "***********************\n", Color.Green, Color.White);
        }
        void Append(RichTextBox box, string text, Color forecolor, Color backcolor)
        {
            if (this.InvokeRequired)
            {
                AppendMessageCallback d = new AppendMessageCallback(Append);
                this.Invoke(d, new object[] { box, text, forecolor, backcolor });
            }
            else
            {
                int start = box.Text.Length;
                int len = text.Length;
                box.AppendText(text);
                box.Select(start, len);
                box.SelectionColor = forecolor;
                box.Select(start + len, 0);
                box.ScrollToCaret();

                if (box.Lines.Length > 1000)
                    box.Clear();
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            tauto.Interval = 10 * 1000;
            t.Tick += delegate
            {
                using (BussinessExecuter be = new BussinessExecuter("server={0};uid={1};pwd={2};database={3}".FormatWith(setting.Server, setting.UID, setting.Pwd, setting.DBName)))
                {
                    DataTable dt = be.QueryDataTable(SqlTexts.GetInfo(setting));
                    Dictionary<DateTime, string> datelist = new Dictionary<DateTime, string>();
                    string NullGroup = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        string s = dr.Field<string>("range_boundary");
                        if (s.NotNullOrEmpty())
                        {
                            datelist.Add(s.ParseTo<DateTime>(), dr.Field<string>("filegroup"));
                        }
                        else
                        {
                            NullGroup = dr.Field<string>("filegroup");
                        }
                    }
                    DateTime dtstart = datelist.Keys.OrderBy(p => p).First();
                    int secs = setting.Interval * setting.PartitionCount;
                    if (DateTime.Now.CompareTo(dtstart.AddSeconds(secs)) > 0)
                    {
                        btnAch_Click(null, null);
                    }
                }
            };
            t.Start();
        }



    }


    public class InitSetting
    {
        public string Server { get; set; }

        public string UID { get; set; }

        public string Pwd { get; set; }

        public string TName { get; set; }


        public string TNameBak
        {
            get
            {
                return TName + "Bak";
            }
        }

        public string ColName { get; set; }

        public string TScript { get; set; }

        public string DBName { get; set; }
        public string DBPath { get; set; }
        public int PartitionCount { get; set; }
        public DateTime StartTime { get; set; }
        public int Interval { get; set; }
        public EnumIntervalType IntervalType { get; set; }


    }
    public enum EnumIntervalType
    {
        Month, Day, Hour, Minute, Second
    }
    [Description("IsTable")]
    public class Orders
    {
        [Description("DataField,IsPrimaryKey,IsIdentity")]
        public int OrderID { set; get; }
        [Description("DataField")]
        public DateTime CreateTime { set; get; }
        [Description("DataField")]
        public string ProductName { set; get; }
        [Description("DataField")]
        public int ProductCount { set; get; }
        [Description("DataField")]
        public double ProductPrice { set; get; }
    }
}
