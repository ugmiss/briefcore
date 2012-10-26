using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProjectBase.Tools.Wiki;

namespace GoogleCodeWikiEditor
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new WikiConverter().ConvertToHtml(memoEdit1.Text);
            new PreviewForm(result).ShowDialog();
        }
    }
}