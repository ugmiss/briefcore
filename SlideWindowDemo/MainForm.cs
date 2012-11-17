using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SlideWindowDemo
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
                string sql =
                     @"select isnull(identity_columns.is_identity ,0) is_identity,
                 isnull(pk.is_primary_key,0) pk,Col.is_nullable is_nullable,
                 (CASE
                 WHEN Type.name = 'uniqueidentifier' THEN 'string'
                 WHEN Type.name = 'char' THEN 'string'
                 WHEN Type.name = 'nchar' THEN 'string'
                 WHEN Type.name = 'varchar' THEN 'string'
                 WHEN Type.name = 'nvarchar' THEN 'string'
                 WHEN Type.name = 'text' THEN 'string'
                 WHEN Type.name = 'ntext' THEN 'string'
                 WHEN Type.name = 'xml' THEN 'string'
                 WHEN Type.name = 'image' THEN 'byte[]'
                 WHEN Type.name = 'timestamp' THEN 'byte[]'
                 WHEN Type.name = 'binary' THEN 'byte[]'
                 WHEN Type.name = 'varbinary' THEN 'byte[]'
                 WHEN Type.name = 'tinyint' THEN 'byte'
                 WHEN Type.name = 'int' THEN 'int'
                 WHEN Type.name = 'smallint' THEN 'short'
                 WHEN Type.name = 'bigint' THEN 'long'
                 WHEN Type.name = 'float' THEN 'double'
                 WHEN Type.name = 'real' THEN 'float'
                 WHEN Type.name = 'money' THEN 'decimal'
                 WHEN Type.name = 'smallmoney' THEN 'decimal'
                 WHEN Type.name = 'decimal' THEN 'decimal'
                 WHEN Type.name = 'numeric' THEN 'decimal'
                 WHEN Type.name = 'time' THEN 'DateTime'
                 WHEN Type.name = 'datetime' THEN 'DateTime'
                 WHEN Type.name = 'smalldatetime' THEN 'DateTime'
                 WHEN Type.name = 'bit' THEN 'bool'
                 WHEN Type.name = 'sql_variant' THEN 'object'
                 ELSE Type.name
                 END) [type], 
                 STUFF(Col.Name,1,1,UPPER(SUBSTRING(Col.Name,1,1))) [propname] 
                 from sys.objects Tab inner join sys.columns Col on Tab.object_id =Col.object_id
                 inner join sys.types Type on Col.system_type_id = Type.system_type_id
                 left join sys.identity_columns identity_columns on  Tab.object_id = identity_columns.object_id and Col.column_id = identity_columns.column_id
                 left join(
                 select index_columns.object_id,index_columns.column_id,indexes.is_primary_key 
                 from sys.indexes  indexes inner join sys.index_columns index_columns 
                 on indexes.object_id = index_columns.object_id and indexes.index_id = index_columns.index_id
                 where indexes.is_primary_key = 1
               ) PK on Tab.object_id = PK.object_id AND Col.column_id = PK.column_id
               where Type.Name <> 'sysname' and (Tab.type = 'U' or Tab.type='V')  and Tab.Name<>'sysdiagrams' and Tab.Name='" + tablename + "'  order by Col.object_id";
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
            }
        }

        private void wizardPage3_PageCommit(object sender, EventArgs e)
        {
            setting.TName = cboTable.Text;
            setting.ColName = cboCol.Text;
            exec.ExecuteNonQuery(SqlTexts.AppendGroup(setting));
            setting.TScript = "";
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
            try
            {
                exec = new BussinessExecuter(setting.ConnectionString);
                exec.QueryDataRow("select 1");
            }
            catch
            {
                e.ErrorText = "数据库连接失败。";
                e.Valid = false;
            }
        }
        // wizardPage3 点上一步
        private void wizardPage3_PageRollback(object sender, EventArgs e)
        {

        }
    }
}
