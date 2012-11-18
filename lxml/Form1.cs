using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace lxml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XNamespace v = "http://www.cnblogs.com/vwxyzh/";
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(v + "persons",
                    new XElement(v + "person",
                        new XElement(v + "firstName", "Zhenway"),
                        new XElement(v + "lastName", "Yan"),
                        new XElement(v + "address", "http://www.cnblogs.com/vwxyzh/")
                        )
                    )
                );
            doc.Declaration = new XDeclaration("1.0", "utf-8", "true");

            radTextBox1.Text = doc.ToString();


        }
    }
}
