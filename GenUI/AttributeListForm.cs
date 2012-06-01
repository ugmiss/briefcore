using System;
using Business;
using DevExpress.XtraEditors.Controls;

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
    }
}