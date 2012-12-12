using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.DataStructureAndAlgorithms.Genetic;

namespace AlgorithmDemo
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new NPForm().Show();
        }
    }

   
}
