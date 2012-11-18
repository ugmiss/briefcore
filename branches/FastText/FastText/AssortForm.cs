using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using TelerikUsing;

namespace FastText
{
    public partial class AssortForm : Telerik.WinControls.UI.RadForm
    {
        public AssortForm()
        {
            InitializeComponent();
        }
        AssortInfoLogic logic = new AssortInfoLogic();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (radTextBox1.Text != "")
            {
                AssortInfo ast = new AssortInfo();
                ast.AssortId = Guid.NewGuid().ToString();
                ast.AssortText = radTextBox1.Text;
                logic.Add(ast);
                ReLoad();
            }
        }

        private void AssortForm_Load(object sender, EventArgs e)
        {
            ReLoad();
        }

        void ReLoad()
        {
            listControl1.DataSource = logic.Get();
            listControl1.DisplayMember = "AssortText";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (listControl1.SelectedItem != null)
            {
                AssortInfo ast = listControl1.SelectedItem.DataBoundItem as AssortInfo;
                logic.Del(ast);
                MsgBox.Show("É¾³ýÍê³É¡£");
                ReLoad();
            }
        }
    }
}
