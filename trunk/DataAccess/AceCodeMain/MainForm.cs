using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccess;
using AceCodeMain.Object;

namespace AceCodeMain
{
    /// <summary>
    /// 主窗体。
    /// </summary>
    public partial class MainForm : Form
    {
        List<Pair> tablePairlist;
        SqlExecuter Executer;
        string conn = "";
        string ut;
        string up;
        string ud;
        public MainForm(string sql)
        {
            InitializeComponent();
            conn = sql;
        }
        /// <summary>
        /// 窗体加载。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Executer = new SqlExecuter();
            Executer.Connection = new System.Data.SqlClient.SqlConnection(conn);
            Executer.OpenConnection();
            DataTable dataTable = Executer.QueryTable("select name from sysobjects where xtype ='u'");
            tablePairlist = new List<Pair>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Pair pair = new Pair();
                pair.Select = true;
                pair.Name = dr[0].ToString();
                tablePairlist.Add(pair);
            }
            var q = from c in tablePairlist orderby c.Name select c;

            this.dataGridView1.DataSource = q.ToList();
            dataGridView1.Columns[1].Width = 200;
        }
        /// <summary>
        /// 生成。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGen_Click(object sender, EventArgs e)
        {
            //获取要生成的表名
            List<string> Tables = new List<string>();
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                Pair p = row.DataBoundItem as Pair;
                if (p.Select) Tables.Add(FirstBig(p.Name));
            }
            List<TableClass> TableClasslist = new List<TableClass>();
            foreach (string tableName in Tables)
            {
                TableClass tableClass = new TableClass();
                DataTable table = Executer.QueryTable("SELECT sysobjects.Name as tb_name, syscolumns.Name as col_name, SysTypes.Name as col_type, syscolumns.Length as col_len, isnull(sys.extended_properties.Value,syscolumns.Name) as col_memo," +
                                     "case when syscolumns.name in (select primarykey=a.name FROM syscolumns a inner join sysobjects b on a.id=b.id  and b.xtype='U' and b.name<>'dtproperties'" +
                                     " where  exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (" +
                                     " SELECT name FROM sysindexes WHERE indid in(  SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid ))) and b.name=sysobjects.Name " +
                                     ") then 1 else 0 end as is_key FROM sysobjects,SysTypes,syscolumns " +
                                     "LEFT JOIN sys.extended_properties ON (Syscolumns.Id = sys.extended_properties.major_id AND" +
                                     " Syscolumns.Colid = sys.extended_properties.minor_id) WHERE (sysobjects.Xtype ='u' OR Sysobjects.Xtype ='v') " +
                                     "AND Sysobjects.Id = Syscolumns.Id AND SysTypes.XType = Syscolumns.XType " +
                                     "AND SysTypes.Name <> 'sysname' AND Sysobjects.Name Like '%' and SysObjects.Name={0} ORDER By SysObjects.Name, SysColumns.colid ",
                                     tableName
                                     );
                tableClass.ClassnameBig = FirstBig(tableName);
                tableClass.ClassnameSmall = FirstSmall(tableName);
                tableClass.Classtext = GetClassDisplay(tableName, Executer);
                if (tableClass.Classtext == null) tableClass.Classtext = tableClass.ClassnameBig;
                foreach (DataRow row in table.Rows)
                {
                    string colname = row["col_name"].ToString();
                    string coltxt = GetPropertyDisplay(tableName, colname, Executer);
                    if (coltxt == null) coltxt = FirstBig(colname);
                    tableClass.Proplisttext.Add(coltxt);
                    tableClass.Proplist.Add(colname);
                    tableClass.ProplistBig.Add(FirstBig(colname));
                    tableClass.ProplistSmall.Add(FirstSmall(colname));
                    tableClass.ProplistType.Add(AttributeMapping.Mapping[row["col_type"].ToString()]);
                    tableClass.ProplistConvert.Add(AttributeMapping.ConvertMapping[AttributeMapping.Mapping[row["col_type"].ToString()]]);

                    if (Convert.ToBoolean(row["is_key"]))
                    {
                        tableClass.PkeySmall = FirstSmall(colname);
                    }
                }
                TableClasslist.Add(tableClass);
            }
            try
            {
                System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Project/Adapter", true);
            }
            catch { }
            try
            {
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Project/BusinessObject", true);
                }
            catch { }
            try
            {
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Project/BusinessLogic", true);
                }
            catch { }
            try
            {
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Project/DataAccess", true);
                }
            catch { }
            try
            {
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Project/IBusinessLogic", true);
                }
            catch { }
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Project");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Project/Adapter");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Project/BusinessObject");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Project/BusinessLogic");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Project/DataAccess");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Project/IBusinessLogic");
            foreach (TableClass tab in TableClasslist)
            {
                if (ckObj.Checked)
                    Gen.GenObject(tab);
                if (ckLogic.Checked)
                {
                    if (tab.ClassnameBig==FirstBig(ut))
                        Gen.GenUserLogic(tab,ud,up);
                    else
                        Gen.GenLogic(tab);
                    
                }
                if (ckInf.Checked)
                {
                    if (tab.ClassnameBig == FirstBig(ut))
                        Gen.GenUserInterface(tab);
                    else
                        Gen.GenInterface(tab);
                }
            }
            TableClass proxytable = new TableClass();
            proxytable.ProplistBig.AddRange(Tables);
            Gen.GenFactory(proxytable);

            if (checkBox2.Checked)
            {
                if (TableClasslist.Count == 0)
                    return;
                TableClass tab = TableClasslist[0];
                this.textBox1.Text=Gen.GenConst(tab);
            }

            if (ckAdp.Checked)
                Gen.GenAdapter(proxytable);
            if (ckFix.Checked)
                Gen.GenFix(conn);
            MessageBox.Show("生成完毕~");
            //this.Close();
        }
        /// <summary>
        /// 取属性（列）的描述
        /// </summary>
        /// <param name="tableName">表名。</param>
        /// <param name="colName">列名。</param>
        /// <returns>描述。</returns>
        public static string GetPropertyDisplay(string tableName, string colName, SqlExecuter Executer)
        {
            DataRow row = Executer.QueryRow("select * from ::fn_listextendedproperty (NULL, 'user', 'dbo','table', {0}, 'column', default) where objname = {1}", tableName, colName);
            if (row == null)
                return null;
            else
                return row["value"].ToString().Trim();
        }
        /// <summary>
        /// 取类（表）的描述。
        /// </summary>
        /// <param name="tableName">表名。</param>
        /// <returns>描述。</returns>
        public static string GetClassDisplay(string tableName, SqlExecuter Executer)
        {
            DataRow row = Executer.QueryRow("select value from ::fn_listextendedproperty('MS_Description','user','dbo','table',{0},null,null)", tableName);
            if (row == null)
                return null;
            else
                return row["value"].ToString().Trim();
        }
        /// <summary>
        /// 首字母大写。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string FirstBig(string str)
        {
            if (str == null) return null;
            if (str.Length == 1) return str.ToUpper();
            return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
        }
        /// <summary>
        /// 首字母小写。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string FirstSmall(string str)
        {
            if (str.Length == 1) return str.ToLower();
            return str.Substring(0, 1).ToLower() + str.Substring(1, str.Length - 1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBox1.Enabled = checkBox1.Checked;
            this.comboBox2.Enabled = checkBox1.Checked;
            this.comboBox3.Enabled = checkBox1.Checked;
            if (this.comboBox1.Enabled)
            {
                this.comboBox1.DataSource = tablePairlist;
                this.comboBox1.DisplayMember = "Name";
            }
            else
            {
                comboBox1.DataSource = null;
                comboBox2.DataSource = null;
                comboBox3.DataSource = null;
                ut = ud = up = null;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                ut = (comboBox1.SelectedItem as Pair).Name;
                //获取要生成的表名
                List<string> Tables = new List<string>();
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    Pair p = row.DataBoundItem as Pair;
                    Tables.Add(FirstBig(p.Name));
                }
                string usertable = (comboBox1.SelectedItem as Pair).Name;
                List<string> proplist = new List<string>();

                TableClass tableClass = new TableClass();
                DataTable table = Executer.QueryTable("SELECT sysobjects.Name as tb_name, syscolumns.Name as col_name, SysTypes.Name as col_type, syscolumns.Length as col_len, isnull(sys.extended_properties.Value,syscolumns.Name) as col_memo," +
                                     "case when syscolumns.name in (select primarykey=a.name FROM syscolumns a inner join sysobjects b on a.id=b.id  and b.xtype='U' and b.name<>'dtproperties'" +
                                     " where  exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (" +
                                     " SELECT name FROM sysindexes WHERE indid in(  SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid ))) and b.name=sysobjects.Name " +
                                     ") then 1 else 0 end as is_key FROM sysobjects,SysTypes,syscolumns " +
                                     "LEFT JOIN sys.extended_properties ON (Syscolumns.Id = sys.extended_properties.major_id AND" +
                                     " Syscolumns.Colid = sys.extended_properties.minor_id) WHERE (sysobjects.Xtype ='u' OR Sysobjects.Xtype ='v') " +
                                     "AND Sysobjects.Id = Syscolumns.Id AND SysTypes.XType = Syscolumns.XType " +
                                     "AND SysTypes.Name <> 'sysname' AND Sysobjects.Name Like '%' and SysObjects.Name={0} ORDER By SysObjects.Name, SysColumns.colid ",
                                     usertable
                                     );
                foreach (DataRow dr in table.Rows)
                {
                    proplist.Add(dr["col_name"].ToString());
                }
                comboBox2.DataSource = proplist.ToArray();
                comboBox3.DataSource = proplist.ToArray();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
             ud=comboBox2.SelectedItem as string;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            up = comboBox3.SelectedItem as string;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Pair pair in tablePairlist)
            {
                pair.Select = false;
            }
            this.dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text,this.textBox1.Text);
        }
    }
}
