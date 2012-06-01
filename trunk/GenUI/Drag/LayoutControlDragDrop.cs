using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraLayout.Dragging;
using DevExpress.Utils.Controls;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System.Collections.Generic;
using Business;

namespace GenUI
{
    /// <summary>
    /// Summary description for Employees.
    /// </summary>
    public partial class LayoutControlDragDrop : UserControl, IDragManager
    {

        private Label lblRubbish;
        private ImageList imageList3;
        private IContainer components;
        private ImageList imageList1;
        private ImageList imageList2;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        public DragDropLayoutControl dragDropLayoutControl1;
        private LayoutControlItem layoutControlItem3;
        public ImageList ListCtrlImages;
        public ListView listView1;
        public LayoutControlDragDrop()
        {
            InitializeComponent();
            dragDropLayoutControl1.DragDrop += new DragEventHandler(dragDropLayoutControl1_DragDrop);
        }

        void dragDropLayoutControl1_DragDrop(object sender, DragEventArgs e)
        {
            ReLoadListView();
        }
        //common
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutControlDragDrop));
            this.lblRubbish = new System.Windows.Forms.Label();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.ListCtrlImages = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dragDropLayoutControl1 = new DragDropLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRubbish
            // 
            this.lblRubbish.AllowDrop = true;
            this.lblRubbish.ImageIndex = 0;
            this.lblRubbish.ImageList = this.imageList3;
            this.lblRubbish.Location = new System.Drawing.Point(483, 6);
            this.lblRubbish.Name = "lblRubbish";
            this.lblRubbish.Size = new System.Drawing.Size(50, 98);
            this.lblRubbish.TabIndex = 3;
            this.lblRubbish.DragDrop += new System.Windows.Forms.DragEventHandler(this.label1_DragDrop);
            this.lblRubbish.DragLeave += new System.EventHandler(this.label1_DragLeave);
            this.lblRubbish.DragEnter += new System.Windows.Forms.DragEventHandler(this.label1_DragEnter);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList3.Images.SetKeyName(0, "");
            this.imageList3.Images.SetKeyName(1, "");
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Location = new System.Drawing.Point(6, 6);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.Size = new System.Drawing.Size(467, 98);
            this.listView1.SmallImageList = this.ListCtrlImages;
            this.listView1.StateImageList = this.imageList1;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            this.listView1.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.listView1_GiveFeedback);
            // 
            // ListCtrlImages
            // 
            this.ListCtrlImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListCtrlImages.ImageStream")));
            this.ListCtrlImages.TransparentColor = System.Drawing.Color.Magenta;
            this.ListCtrlImages.Images.SetKeyName(0, "default.gif");
            this.ListCtrlImages.Images.SetKeyName(1, "IFrame控件.gif");
            this.ListCtrlImages.Images.SetKeyName(2, "标签控件.gif");
            this.ListCtrlImages.Images.SetKeyName(3, "表格控件.gif");
            this.ListCtrlImages.Images.SetKeyName(4, "表头控件.gif");
            this.ListCtrlImages.Images.SetKeyName(5, "层控件.gif");
            this.ListCtrlImages.Images.SetKeyName(6, "单文件上传.gif");
            this.ListCtrlImages.Images.SetKeyName(7, "单行文本控件.gif");
            this.ListCtrlImages.Images.SetKeyName(8, "单选.gif");
            this.ListCtrlImages.Images.SetKeyName(9, "单选人控件.gif");
            this.ListCtrlImages.Images.SetKeyName(10, "单选组织控件.gif");
            this.ListCtrlImages.Images.SetKeyName(11, "弹出控件.gif");
            this.ListCtrlImages.Images.SetKeyName(12, "多文件上传.gif");
            this.ListCtrlImages.Images.SetKeyName(13, "多行文本控件.gif");
            this.ListCtrlImages.Images.SetKeyName(14, "多选人控件.gif");
            this.ListCtrlImages.Images.SetKeyName(15, "多选组织控件.gif");
            this.ListCtrlImages.Images.SetKeyName(16, "枚举单选.gif");
            this.ListCtrlImages.Images.SetKeyName(17, "枚举复选.gif");
            this.ListCtrlImages.Images.SetKeyName(18, "枚举下拉.gif");
            this.ListCtrlImages.Images.SetKeyName(19, "签名控件.gif");
            this.ListCtrlImages.Images.SetKeyName(20, "日期控件.gif");
            this.ListCtrlImages.Images.SetKeyName(21, "隐藏控件.gif");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dragDropLayoutControl1);
            this.layoutControl1.Controls.Add(this.listView1);
            this.layoutControl1.Controls.Add(this.lblRubbish);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(539, 500);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dragDropLayoutControl1
            // 
            this.dragDropLayoutControl1.AllowDrop = true;
            this.dragDropLayoutControl1.BackColor = System.Drawing.Color.Cyan;
            this.dragDropLayoutControl1.Location = new System.Drawing.Point(6, 114);
            this.dragDropLayoutControl1.Name = "dragDropLayoutControl1";
            this.dragDropLayoutControl1.Size = new System.Drawing.Size(527, 363);
            this.dragDropLayoutControl1.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(539, 500);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHotTrack = false;
            this.layoutControlItem1.Control = this.listView1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(477, 108);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AllowHotTrack = false;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem3.Control = this.dragDropLayoutControl1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(537, 390);
            this.layoutControlItem3.Text = " ";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(4, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AllowHotTrack = false;
            this.layoutControlItem2.Control = this.lblRubbish;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(477, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(60, 108);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.TrimClientAreaToControl = false;
            // 
            // LayoutControlDragDrop
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "LayoutControlDragDrop";
            this.Size = new System.Drawing.Size(539, 500);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }
        LayoutControlItem dragItem = null;
        LayoutControlItem IDragManager.DragItem
        {
            get { return dragItem; }
            set { dragItem = value; }
        }
        void IDragManager.SetDragCursor(DragDropEffects e) { SetDragCursor(e); }
        private void SetDefaultCursor()
        {
            Cursor = Cursors.Default;
        }
        private void SetDragCursor(DragDropEffects e)
        {
            if (e == DragDropEffects.Move)
                Cursor = new Cursor("images/move.ico");
            if (e == DragDropEffects.Copy)
                Cursor = new Cursor("images/copy.ico");
            if (e == DragDropEffects.None)
                Cursor = Cursors.No;
        }
        private LayoutControlItem GetDragNode(IDataObject data)
        {
            return data.GetData(typeof(LayoutControlItem)) as LayoutControlItem;
        }
        //ListView1
        private ListViewItem newItem = null;
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            newItem = listView1.GetItemAt(e.X, e.Y);
        }
        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (newItem == null || e.Button != MouseButtons.Left) return;
            dragItem = new LayoutControlItem();
            Prop prop = (newItem.Tag as Prop);
            dragItem.Name = prop.ID.ToString();

            switch ((EnumControl)prop.PropCtrl)
            {
                case EnumControl.TextBox:
                    dragItem.Control = new TextEdit();
                    break;
                case EnumControl.DateEdit:
                    dragItem.Control = new DateEdit();
                    break;
                case EnumControl.DropDownList:
                    ComboBoxEdit edit = new ComboBoxEdit();
                    edit.Properties.Items.AddRange(new object[] { "aa", "ss", "dd" });
                    dragItem.Control = edit;
                    break;
                //case EnumControl.CheckBox:
                //    CheckEdit chkedit = new CheckEdit();
                //    chkedit.Text = "";
                //    dragItem.Control = chkedit;
                //    break;
                case EnumControl.TextArea:
                    MemoEdit txt = new MemoEdit();
                    txt.Height = 50;
                    dragItem.Control = txt;
                    break;
                case EnumControl.UploadEdit:
                    ButtonEdit upl = new ButtonEdit();
                    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                    dragItem.Control = upl;
                    break;
                //case EnumControl.OrgSelectMulti:
                //    ButtonEdit upl2 = new ButtonEdit();
                //    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                //    dragItem.Control = upl2;
                //    break;
                //case EnumControl.OrgSelectSingle:
                //    ButtonEdit upl3 = new ButtonEdit();
                //    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                //    dragItem.Control = upl3;
                //    break;
                //case EnumControl.UserSelectMulti:
                //    ButtonEdit upl4 = new ButtonEdit();
                //    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                //    dragItem.Control = upl4;
                //    break;
                //case EnumControl.UserSelectSingle:
                //    ButtonEdit upl5 = new ButtonEdit();
                //    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                //    dragItem.Control = upl5;
                //    break;

                //case EnumControl.ProjectSelectSingle:
                //    ButtonEdit upl6 = new ButtonEdit();
                //    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                //    dragItem.Control = upl6;
                //    break;
                case EnumControl.NumBox:
                    SpinEdit sp = new SpinEdit();
                    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                    dragItem.Control = sp;
                    break;
                case EnumControl.NumBoxSmall:
                    SpinEdit sp2 = new SpinEdit();
                    //upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                    dragItem.Control = sp2;
                    break;
                //case EnumControl.LabelEdit:
                //    LabelControl lab = new LabelControl();
                //    lab.Size = new Size(500, 2);
                //    //lab.Enabled = false;
                //    lab.ForeColor = Color.Gray;
                //    //lab.BackColor = Color.DimGray;
                //    lab.Text = "------------------------------------------------------------------------------------------";
                //    // upl.ButtonClick += delegate { new UploadForm().ShowDialog(); };
                //    dragItem.Control = lab;
                //    break;
                case EnumControl.NONE:
                    return;
            }
            dragItem.Control.Tag = prop.PropField;
            dragItem.Control.Name = dragItem.Name;
            if (dragItem.Control is CheckEdit)
            {
                dragItem.Control.Text = newItem.Text;
                dragItem.Text = " ";
                dragItem.Name = " ";
            }
            else
            {
                dragItem.Text = newItem.Text;
            }
            listView1.DoDragDrop(dragItem, DragDropEffects.Copy);

            PropListCache.Proplist.Remove(PropListCache.Proplist.Find(p => p.PropName == newItem.Text));
            ReLoadListView();
        }
        private void listView1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }
        //Label1
        private void label1_DragDrop(object sender, DragEventArgs e)
        {
            if (CanRecycleDragItem())
            {
                Control control = dragItem.Control;
                dragItem.Parent.Remove(dragItem);
                Prop prop = PropListCache.AllProplist.Find(p => p.ID.ToString() == control.Name);
                PropListCache.Proplist.Add(prop);
                if (control != null)
                {
                    control.Parent = null;
                    control.Dispose();
                }
                ReLoadListView();
                dragItem = null;
            }
            SetDefaultLabel();
        }
        void ReLoadListView()
        {



            listView1.Items.Clear();
            foreach (Prop p in PropListCache.Proplist)
            {
                ListViewItem item = new ListViewItem(p.PropName);
                item.Tag = p;
                item.ImageIndex = GetImageIndex(((EnumControl)p.PropCtrl).ToString());
                listView1.Items.Add(item);
            }
            listView1.Refresh();
        }
        int GetImageIndex(string ctrlname)
        {
            switch (ctrlname)
            {
                case "TextBox":
                    return ListCtrlImages.Images.IndexOfKey("单行文本控件.gif");
                case "DropDownList":
                    return ListCtrlImages.Images.IndexOfKey("枚举下拉.gif");
                case "DateEdit":
                    return ListCtrlImages.Images.IndexOfKey("日期控件.gif");
                case "CheckBox":
                    return ListCtrlImages.Images.IndexOfKey("枚举单选.gif");
                case "TextArea":
                    return ListCtrlImages.Images.IndexOfKey("多行文本控件.gif");
                case "UploadEdit":
                    return ListCtrlImages.Images.IndexOfKey("单文件上传.gif");
            }
            return 0;
        }
        private void label1_DragEnter(object sender, DragEventArgs e)
        {
            if (CanRecycleDragItem())
            {
                lblRubbish.ImageIndex = 1;
                e.Effect = DragDropEffects.Copy;
                Cursor = new Cursor("images/delete.ico");
            }
        }
        private void label1_DragLeave(object sender, EventArgs e)
        {
            SetDefaultLabel();
        }
        private void SetDefaultLabel()
        {
            lblRubbish.ImageIndex = 0;
            SetDefaultCursor();
        }
        protected bool CanRecycleDragItem()
        {
            if (dragItem == null) return false;
            if (dragItem.Owner == null) return false;
            return true;
        }
    }
}
