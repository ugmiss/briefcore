using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AExplorer
{
    public partial class AEForm : Form
    {
        public AEForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.NotNullOrEmpty())
            {
                string html = Utility.HttpHelper.HttpGet(textBox1.Text,Encoding.UTF8);
                this.richTextBox1.Text = Utility.HttpHelper.NoHTML(html);
            }
        }
    }
}
