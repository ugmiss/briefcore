using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Business;

namespace GenUI
{
    public partial class AttributeUIForm : DevExpress.XtraEditors.XtraForm
    {
        List<Prop> PropList = new List<Prop>();
        Sys_ModelAttribute[] attrs;
        Sys_Model curSys_Model;
        EnumOperation enumOperation;
        DiyForm curDiyForm;
        public AttributeUIForm(Sys_ModelAttribute[] attrs, Sys_Model model)
        {
            curSys_Model = model;
            this.attrs = attrs;
            InitializeComponent();
        }

        void LoadListCtrl()
        {
            PropList.Clear();
            foreach (Sys_ModelAttribute m in attrs)
            {
                if (m.AttrControl.ParseTo<int>() != (int)EnumControl.NONE)
                    PropList.Add(new Prop(m.Id, m.AttrTitle, m.AttrName, m.AttrType, m.AttrControl.ParseTo<int>(), 0));
            }
            this.layoutControlDragDrop1.listView1.Clear();
            PropListCache.Proplist = new List<Prop>(PropList.ToArray());
            PropListCache.AllProplist = new List<Prop>(PropList.ToArray());
            foreach (Prop p in PropList)
            {
                ListViewItem item = new ListViewItem(p.PropName);
                item.Tag = p;
                item.ImageIndex = GetImageIndex(((EnumControl)p.PropCtrl).ToString());
                layoutControlDragDrop1.listView1.Items.Add(item);
            }
            layoutControlDragDrop1.listView1.Refresh();
        }
        int GetImageIndex(string ctrlname)
        {
            switch (ctrlname)
            {
                case "TextBox":
                    return layoutControlDragDrop1.ListCtrlImages.Images.IndexOfKey("单行文本控件.gif");
                case "DropDownList":
                    return layoutControlDragDrop1.ListCtrlImages.Images.IndexOfKey("枚举下拉.gif");
                case "DateEdit":
                    return layoutControlDragDrop1.ListCtrlImages.Images.IndexOfKey("日期控件.gif");
                case "CheckBox":
                    return layoutControlDragDrop1.ListCtrlImages.Images.IndexOfKey("枚举单选.gif");
                case "TextArea":
                    return layoutControlDragDrop1.ListCtrlImages.Images.IndexOfKey("多行文本控件.gif");
                case "UploadEdit":
                    return layoutControlDragDrop1.ListCtrlImages.Images.IndexOfKey("单文件上传.gif");
            }
            return 0;
        }

        private void AttributeUIForm_Load(object sender, EventArgs e)
        {
            LoadListCtrl();
        }

        public void Save()
        {
            List<Ctrl> list = new List<Ctrl>();
            foreach (Control c in layoutControlDragDrop1.dragDropLayoutControl1.layoutControl2.Controls)
            {

                Ctrl cc = new Ctrl();
                cc.p = c.Location;
                cc.s = c.Size;
                cc.tp = c.GetType().ToString();
                cc.txt = c.Text;
                if (c.Tag != null)
                    cc.FieldName = c.Tag.ToString();
                if (cc.tp.StartsWith("DevExpress.XtraLayout")) continue;
                if (cc.txt == "" && cc.FieldName == null) continue;
                list.Add(cc);
            }
            foreach (LayoutControlItem item in layoutControlDragDrop1.dragDropLayoutControl1.layoutControlGroup2.Items)
            {
                if (item.Tag == null && item.Text == "") continue;
                Ctrl cc = new Ctrl();
                cc.p = new Point(item.Location.X, item.Control.Location.Y);
                cc.s = new Size(100, 27);
                cc.txt = item.Text.ToString();
                cc.tp = "Label";
                if (item.Tag != null)
                    cc.FieldName = item.Tag.ToString();
                if (cc.tp.StartsWith("DevExpress.XtraLayout")) continue;
                list.Add(cc);
            }
            curDiyForm = new DiyForm();
            curDiyForm.Height = layoutControlDragDrop1.dragDropLayoutControl1.Height;
            curDiyForm.Width = layoutControlDragDrop1.dragDropLayoutControl1.Width;
            curDiyForm.CtrlList = list;
            curSys_Model.UIXml = curDiyForm.XmlSerialize<DiyForm>();
            Environment.Logic.ExecuteNonQuery(CreateTable());
            Environment.Logic.Modify<Sys_Model>(curSys_Model);
        }

        public string CreateTable()
        {
            List<string> pks = new List<string>();
            List<string> attrlist = new List<string>();
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
            return sql.FormatWith(curSys_Model.TableName, attrstring, pkstring);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonForm form = new CommonForm(EnumOperation.View, curSys_Model, Comb.NewGuid(), new Dictionary<string, object>(), "");
            form.ShowDialog();
            //Logic.GetAll<Sys_Model>(p=>p.Id==
        }
    }
}