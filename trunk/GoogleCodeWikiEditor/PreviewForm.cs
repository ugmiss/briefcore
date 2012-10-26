using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GoogleCodeWikiEditor
{
    public partial class PreviewForm : DevExpress.XtraEditors.XtraForm
    {
        string file = "temp.html";
        public PreviewForm(string html)
        {
            html.WriteToFile(file);
            InitializeComponent();
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri(file.AppPath());
        }
    }
}