using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PartitionAndSlideWindow
{
    public partial class MainForm : XtraForm
    {
        #region const string
        const string page0Text = @"方案概要：
    采用滑动窗口机制，把分区表的分区依次移入到另一个分区表。
分区初始工作：
    创建新库或已有库追加分区文件组（File Group），
    创建分区函数（Partition Function），
    创建分区方案（Partition Scheme），
    创建或修改分区表（Partition Table）；
滑动归档工作：
    分区表分区切换（Partition Swtich）,
    临时表归档并清理（Achieve & Clean Up）,
    分区函数合并（Partition Merge）,
    拆分（Partition Split）";
        #endregion
        InitSetting setting = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            welcomeWizardPage1.IntroductionText = page0Text;
        }

        private void wizardPage1_PageCommit(object sender, EventArgs e)
        {

        }

        private void wizardPage2_PageCommit(object sender, EventArgs e)
        {
            setting.DBPath = txtDBPath.Text;
            setting.PartitionCount = txtPartitionCount.Text.ParseTo<int>();
            setting.IntervalType = (EnumIntervalType)comboBox1.SelectedIndex;
            setting.StartTime = txtStartTime.Text.ParseTo<DateTime>();
            setting.Interval = txtInterval.Text.ParseTo<int>();
            List<string> tablelist = new List<string>();
            string sqltable = "SELECT TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE Table_name<>'sysdiagrams' and (Table_Type='BASE TABLE' or  Table_Type='View') and ISNULL(OBJECTPROPERTY(OBJECT_ID(TABLE_NAME), 'IsMSShipped'), 0) = 0 ORDER BY TABLE_NAME";
            foreach (DataRow dr in exec.QueryDataTable(sqltable).Rows)
            {
                string tablename = dr["TABLE_NAME"].ToString();
                tablelist.Add(tablename);
            }
            cboTable.Items.Clear();
            cboTable.Items.AddRange(tablelist.ToArray());
            cboTable.SelectedIndexChanged += new EventHandler(cboTable_SelectedIndexChanged);
            if (tablelist.Count > 0)
            {
                cboTable.SelectedIndex = 0;
            }
        }

        void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTable.SelectedItem.ToString() != "")
            {
                string tablename = cboTable.SelectedItem.ToString();
                string sql = SqlTexts.GetTableInfo(tablename);
                List<string> fields = new List<string>();
                foreach (DataRow dr in exec.QueryDataTable(sql).Rows)
                {
                    string propname = dr["propname"].ToString();
                    bool pk = Convert.ToBoolean(dr["pk"]);
                    bool is_identity = Convert.ToBoolean(dr["is_identity"]);
                    bool is_nullable = Convert.ToBoolean(dr["is_nullable"]);
                    string typestr = dr["type"].ToString();
                    string nullable = is_nullable && typestr != "object" && typestr != "string" && typestr != "byte[]" ? "?" : "";
                    if (typestr == "DateTime")
                    {
                        fields.Add(propname);
                    }
                }
                cboCol.Items.Clear();
                cboCol.Items.AddRange(fields.ToArray());
                if (fields.Count > 0)
                {
                    cboCol.SelectedIndex = 0;
                }
                lisIndex.Items.Clear();
            }
        }



        private void wizardPage3_PageCommit(object sender, EventArgs e)
        {
            setting.TName = cboTable.Text;
            setting.ColName = cboCol.Text;
            // 追加分区
            try
            {
                exec.ExecuteNonQuery(SqlTexts.AppendGroup(setting));
            }
            catch
            {

            }
            // 添加分区函数
            exec.ExecuteNonQuery(SqlTexts.RemovePartitionFunc(setting));
            try
            {
                exec.ExecuteNonQuery(SqlTexts.CreatePartitionFunc(setting));
                // 添加分区架构
                exec.ExecuteNonQuery(SqlTexts.CreatePartitionScheme(setting));
            }
            catch
            {
            }

            // 修改表 移除主键
            string tablename = cboTable.SelectedItem.ToString();
            string col = cboCol.SelectedItem.ToString();
            DataTable dtindex = exec.QueryDataTable(SqlTexts.GetTableIndexInfo(tablename));
            bool flag = false;
            foreach (DataRow dr in dtindex.Rows)
            {
                string cluster = dr.Field<string>("cluster");
                string cols = dr.Field<string>("Co_Names");
                if (cluster.ToLower() == "clustered")
                {
                    string ixName = dr.Field<string>("Index_Name");
                    bool ispk = dr.Field<bool>("is_primary_key");
                    if (ispk)
                    {
                        exec.ExecuteNonQuery(SqlTexts.UpdatePK(tablename, ixName, cols));
                    }
                    else
                    {
                        exec.ExecuteNonQuery(SqlTexts.UpdateIX(tablename, ixName, cols, setting.DBName, col));
                        flag = true;
                    }
                }
            }
            // 修改表 为分区表
            if (!flag)
            {
                exec.ExecuteNonQuery(SqlTexts.CreateIX(tablename, "IX_" + tablename + col, col, setting.DBName, col));
            }
            setting.TScript = SqlTexts.GetScript(setting);
            new ScriptForm(setting.TScript).ShowDialog();
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        BussinessExecuter exec = null;
        // 跳转前
        private void wizardPage1_PageValidating(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
        {
            setting = new InitSetting()
            {
                Server = txtServer.Text,
                UID = txtUid.Text,
                Pwd = txtPwd.Text,
                DBName = txtDBName.Text
            };

            exec = new BussinessExecuter(setting.ConnectionString);
            if (!exec.TestConnection(1))
            {
                MsgBox.Show("数据库连接失败。");
                e.Valid = false;
            }
        }
        // wizardPage3 点上一步
        private void wizardPage3_PageRollback(object sender, EventArgs e)
        {

        }
    }
}
