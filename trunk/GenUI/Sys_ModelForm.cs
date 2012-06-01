using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Business;

namespace GenUI
{
    public partial class Sys_ModelForm : DevExpress.XtraEditors.XtraForm
    {
        public Sys_ModelForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Sys_Model Sys_Model = new Sys_Model();
            Sys_Model.Id = Comb.NewGuid();
            Sys_Model.TableName = txtName.Text;
            Sys_Model.TableTitle = txtTitle.Text;
            Environment.Logic.Add<Sys_Model>(Sys_Model);
            Close();
        }
    }
}