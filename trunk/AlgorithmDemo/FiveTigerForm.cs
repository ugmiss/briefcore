using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Algorithms.Genetic;

namespace AlgorithmDemo
{
    public partial class FiveTigerForm : Form
    {
        public FiveTigerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] rights;
            int[] blacks = Enumerable.Range(0, 25).ToArray();

            int sum= FiveTigerGeneticHelper.GetBlackPoint(blacks, out rights);
                
        }
    }
}
