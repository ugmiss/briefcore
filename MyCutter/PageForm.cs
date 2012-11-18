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
    public partial class PageForm : RadForm
    {
        public Page Page { get; set; }
        public Book Book { get; set; }
        public int index { get; set; }

        public PageForm(Book Book, int index)
        {
            this.Book = Book;
            this.index = index;
            InitializeComponent();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            Page = new Page();
            Page.Name = txtName.Text;
            Page.Index = txtIndex.Text.ParseTo<int>();
            Page.BackgroundImage = txtBackground.Text;
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
            string res = Common.SelectFile("pic");
            string path = AppDomain.CurrentDomain.BaseDirectory + Book.BookName + "\\resources\\";
            txtBackground.Text = Common.AddResouce(Book.BookName, res, path + Common.GetShortFileName(res));
        }

        private void PageForm_Load(object sender, EventArgs e)
        {
            txtIndex.Text = index.ToString();
        }
    }
}
