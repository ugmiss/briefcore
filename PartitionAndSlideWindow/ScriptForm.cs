using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PartitionAndSlideWindow
{
    public partial class ScriptForm : Form
    {
        string scp = "";
        public ScriptForm(string scp)
        {
            this.scp = scp;
            InitializeComponent();
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = scp;
        }

    }
}
