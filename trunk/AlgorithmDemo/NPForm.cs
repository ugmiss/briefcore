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
    public partial class NPForm : Form
    {
        public NPForm()
        {
            InitializeComponent();
        }

        private void NPForm_Load(object sender, EventArgs e)
        {
            li.Add(new Deal() { Weight = 3, Value = 8 });
            li.Add(new Deal() { Weight = 5, Value = 14 });
            li.Add(new Deal() { Weight = 4, Value = 11 });
            li.Add(new Deal() { Weight = 6, Value = 18 });
            li.Add(new Deal() { Weight = 8, Value = 19 });
            li.Add(new Deal() { Weight = 9, Value = 23 });
            li.Add(new Deal() { Weight = 11, Value = 25 });
            dataGridView1.DataSource = li.ToArray();
        }
        List<Deal> li = new List<Deal>();
     
        private void btnCalc_Click(object sender, EventArgs e)
        {
            var max= txtMax.Text.ParseTo<double>();
            NPGeneticHelper helper = new NPGeneticHelper(max, li);


            Revolution revolution = new Revolution();
            revolution.InitData(100, li.Count, 200,100);
            revolution.InitFunc(NPGeneticHelper.FitnessFunc, NPGeneticHelper.ChromosomeFunc, NPGeneticHelper.ChooseFunc, NPGeneticHelper.CrossFunc, NPGeneticHelper.MutationAction);
            revolution.Begin();
            lblResult.Text = string.Join("", revolution.BestIndividual); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Deal d = new Deal();
            d.Weight = Convert.ToDouble(txtWeight.Text);
            d.Value = Convert.ToDouble(txtValue.Text);
            li.Add(d);
            dataGridView1.DataSource = li.ToArray();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            li.Clear();
            dataGridView1.DataSource = li.ToArray();
        }
    }
}
