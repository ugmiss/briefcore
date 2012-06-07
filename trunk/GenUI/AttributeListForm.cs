using System;
using Business;
using DevExpress.XtraEditors.Controls;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace GenUI
{
    public partial class AttributeListForm : DevExpress.XtraEditors.XtraForm
    {
        Sys_Model CurrentSys_Model;
        Sys_ModelAttribute CurrentSys_ModelAttribute;
        public AttributeListForm()
        {
            InitializeComponent();
        }

        public AttributeListForm(Sys_Model sys_Model)
        {
            CurrentSys_Model = sys_Model;
            InitializeComponent();
        }

        private void AttributeListForm_Load(object sender, EventArgs e)
        {
            BindData();
            cboType.Init();
            cboControl.Init();
            cboControl.Properties.Items.AddRange(typeof(EnumControl).EnumList().ToArray());
            cboControl.SelectedIndex = 0;
            cboType.Properties.Items.AddRange(typeof(EnumFieldType).EnumList().ToArray());
            cboType.SelectedIndex = 0;
            gridView1_FocusedRowChanged(null, null);
        }
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentSys_ModelAttribute == null)
            {
                Sys_ModelAttribute attr = new Sys_ModelAttribute();
                attr.Id = Comb.NewGuid();
                attr.ModelId = CurrentSys_Model.Id;
                attr.AllowNull = checkEdit1.Checked;
                attr.AttrControl = ((cboControl.SelectedItem as EnumObject).Value).ToString();
                attr.AttrIndex = txtIndex.Text.ParseTo<int>();
                attr.AttrLenth = txtLength.Text;
                attr.AttrName = txtName.Text;
                attr.AttrTitle = txtTitle.Text;
                attr.AttrType = ((cboType.SelectedItem as EnumObject).Value).ToString();
                attr.IsPk = checkEdit2.Checked;
                Environment.Logic.Add<Sys_ModelAttribute>(attr);
            }
            else
            {
                Sys_ModelAttribute attr = new Sys_ModelAttribute();
                attr.Id = CurrentSys_ModelAttribute.Id;
                attr.ModelId = CurrentSys_Model.Id;
                attr.AllowNull = checkEdit1.Checked;
                attr.AttrControl = ((cboControl.SelectedItem as EnumObject).Value).ToString();
                attr.AttrIndex = txtIndex.Text.ParseTo<int>();
                attr.AttrLenth = txtLength.Text;
                attr.AttrName = txtName.Text;
                attr.AttrTitle = txtTitle.Text;
                attr.AttrType = ((cboType.SelectedItem as EnumObject).Value).ToString();
                attr.IsPk = checkEdit2.Checked;

                Environment.Logic.Modify(attr);
            }
            BindData();
        }

        public void BindData()
        {
            try
            {
                gridControl1.Init();
                gridControl1.DataSource = Environment.Logic.GetAll<Sys_ModelAttribute>().FindAll(p => p.ModelId == CurrentSys_Model.Id);
            }
            catch (Exception ex)
            {
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            Sys_ModelAttribute curSys_ModelAttribute = gridView1.GetFocusedRow() as Sys_ModelAttribute;
            if (null == curSys_ModelAttribute)
            {
                checkEdit1.Checked = default(bool);
                checkEdit2.Checked = default(bool);
                cboControl.SelectedIndex = 0;
                txtIndex.Text = "";
                txtLength.Text = "";
                txtName.Text = "";
                txtTitle.Text = "";
                cboType.SelectedIndex = 0;
            }
            else
            {
                checkEdit2.Checked = curSys_ModelAttribute.IsPk;
                checkEdit1.Checked = curSys_ModelAttribute.AllowNull;

                // cboControl.SelectedItem = cboControl.Properties.Items.First(p => p.Value.ToString() == typeof(EnumControl).GetEnumObject(curSys_ModelAttribute.AttrControl.ParseTo<int>()).Value);//初始化。
                foreach (var cbi in cboControl.Properties.Items)
                {
                    if (((EnumObject)cbi).Value.ToString() == curSys_ModelAttribute.AttrControl)
                    {
                        cboControl.SelectedItem = cbi;
                    }
                }
                // cboControl.SelectedItem = typeof(EnumControl).GetEnumObject(curSys_ModelAttribute.AttrControl.ParseTo<int>());
                txtIndex.Text = curSys_ModelAttribute.AttrIndex.ToString();
                txtLength.Text = curSys_ModelAttribute.AttrLenth;
                txtName.Text = curSys_ModelAttribute.AttrName;
                txtTitle.Text = curSys_ModelAttribute.AttrTitle;
                foreach (var cbi in cboType.Properties.Items)
                {
                    if (((EnumObject)cbi).Value.ToString() == curSys_ModelAttribute.AttrType)
                    {
                        cboType.SelectedItem = cbi;
                    }
                }
                CurrentSys_ModelAttribute = curSys_ModelAttribute;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CurrentSys_ModelAttribute = null;
            checkEdit1.Checked = default(bool);
            checkEdit2.Checked = default(bool);
            cboControl.SelectedIndex = 0;
            txtIndex.Text = "";
            txtLength.Text = "";
            txtName.Text = "";
            txtTitle.Text = "";
            cboType.SelectedIndex = 0;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AttributeUIForm form = new AttributeUIForm(Environment.Logic.GetAll<Sys_ModelAttribute>().FindAll(p => p.ModelId == CurrentSys_Model.Id).ToArray(), CurrentSys_Model);
            form.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentSys_ModelAttribute != null)
            {
                Environment.Logic.Delete(CurrentSys_ModelAttribute);
                BindData();
            }
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentSys_ModelAttribute != null)
            {
                Environment.Logic.ExecuteNonQuery(CreateTable());

                if (Directory.Exists(namespacestr.AppPath()))
                    Directory.Delete(namespacestr.AppPath(), true);
                Directory.CreateDirectory(namespacestr.AppPath());
                string sqltable = "SELECT TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE Table_name<>'sysdiagrams' and (Table_Type='BASE TABLE' or  Table_Type='View') and ISNULL(OBJECTPROPERTY(OBJECT_ID(TABLE_NAME), 'IsMSShipped'), 0) = 0 ORDER BY TABLE_NAME where TABLE_NAME='" + CurrentSys_Model.TableName + "'";
                foreach (DataRow dr in Environment.Logic.QueryDataTable(sqltable).Rows)
                {
                    bool isview = !(dr["TABLE_TYPE"].ToString() == "BASE TABLE");
                    string tablename = dr["TABLE_NAME"].ToString();
                    Gen(tablename.FirstUpper(), isview);
                }
                MessageBox.Show("生成完毕。");
                Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        public string CreateTable()
        {
            List<string> pks = new List<string>();
            List<string> attrlist = new List<string>();
            var attrs = Environment.Logic.GetAll<Sys_ModelAttribute>().FindAll(p => p.ModelId == CurrentSys_Model.Id);
            foreach (Sys_ModelAttribute attr in attrs)
            {
                if (attr.IsPk)
                    pks.Add(attr.AttrName + " asc");
                string ty = ((EnumFieldType)(attr.AttrType.ParseTo<int>())).ToString();
                string len = "";
                if (!attr.AttrLenth.IsEmpty())
                    len = "(" + attr.AttrLenth + ")";
                string temp = attr.AttrName + " " + ty + " " + len + " " + (attr.AllowNull ? "null" : "not null");
                attrlist.Add(temp);
            }

            string attrstring = string.Join(",", attrlist.ToArray());
            string pkstring = string.Join(",", pks.ToArray());
            string sql = @"
            if exists (select * from sys.sysobjects where name='{0}')
            drop table {0}
            CREATE TABLE [dbo].[{0}](
	        {1}
            CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
            (
	            {2}
            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            ) ON [PRIMARY]";
            return sql.FormatWith(CurrentSys_Model.TableName, attrstring, pkstring);
        }
        string namespacestr = "Business";

        void Gen(string tablename, bool isview)
        {
            List<string> fields = new List<string>();
            string sql =
                 @"select 
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
               where Type.Name <> 'sysname' and (Tab.type = 'U' or Tab.type='V')  and Tab.Name<>'sysdiagrams' and Tab.Name='" + tablename + "'";
            string code =
@"using System;
using System.ComponentModel;

namespace {0}
{{
    {3}
    public class {1}
    {{
{2}
    }}
}}";
            string fieldstr = "";
            foreach (DataRow dr in Environment.Logic.QueryDataTable(sql).Rows)
            {
                string publicName = dr["propname"].ToString();
                bool pk = Convert.ToBoolean(dr["pk"]);
                bool is_nullable = Convert.ToBoolean(dr["is_nullable"]);
                string typestr = dr["type"].ToString();
                string nullable = is_nullable && typestr != "object" && typestr != "string" && typestr != "byte[]" ? "?" : "";
                if (pk)
                    fieldstr += "        [Description(\"IsPrimaryKey\")]\r\n";
                fieldstr += "        public " + typestr + nullable + " " + publicName + " { set; get; }\r\n";
            }
            fieldstr = fieldstr.TrimEnd();
            string istborview = !isview ? "        [Description(\"IsTable\")]\r\n" : "        [Description(\"IsView\")]\r\n";
            code.FormatWith(namespacestr, tablename, fieldstr, istborview).WriteToFile((namespacestr + "\\" + tablename + ".cs").AppPath());
        }
    }
}