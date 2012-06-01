using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Business;

namespace GenUI
{
    public partial class ModelListForm : XtraForm
    {
        public ModelListForm()
        {
            InitializeComponent();
        }
        string path = "conn.setting".AppPath();

        private void btnSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new SettingForm().ShowDialog();
        }

        void BindData()
        {
            string aa = null;
            gridControl1.DataSource = Environment.Logic.GetAll<Sys_Model>(p => p.TableName != "" && aa != p.TableTitle);
            gridView1.Columns["Id"].Visible = false;
            gridView1.Columns["UIXml"].Visible = false;
            gridView1.Columns["TableName"].Caption = "表名称";
            gridView1.Columns["TableTitle"].Caption = "中文名称";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRow() != null)
                {
                    Sys_Model sys_Model = gridView1.GetFocusedRow() as Sys_Model;
                    new AttributeListForm(sys_Model).ShowDialog();
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Sys_ModelForm().ShowDialog();
            BindData();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRow() != null)
            {
                Sys_Model sys_Model = gridView1.GetFocusedRow() as Sys_Model;
                Environment.Logic.Delete(sys_Model);
                BindData();
            }
        }
    }
}
