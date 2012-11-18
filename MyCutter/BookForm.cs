using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace MyCutter
{
    public partial class BookForm : RadForm
    {
        public Book Book { get; set; }
        public BookForm()
        {
            InitializeComponent();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            Book = new Book();
            Book.BookName = txtName.Text;
            Book.Height = txtHeight.Text.ParseTo<int>();
            Book.Width = txtWidth.Text.ParseTo<int>();

            Book.Background = txtBackground.Text;
           
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            txtBackground.Text = Common.SelectFile("pic");
        }
    }
}
