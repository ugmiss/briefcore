using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameCenter
{
    public partial class CenterForm : Form
    {
        public CenterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateRoomForm form = new CreateRoomForm();
            form.ShowDialog();
        }
    }
}
