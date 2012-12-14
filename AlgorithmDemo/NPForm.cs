using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        double MaxWeight = 0;
        private void btnCalc_Click(object sender, EventArgs e)
        {
            MaxWeight = Convert.ToDouble(txtMax.Text);
            Deal best = li.OrderByDescending(p => p.Value / p.Weight).ToArray()[0];


            double MaxGuess = MaxWeight / best.Weight * best.Value;

            Func<int[], double> func = p =>
            {
                double SumWeight = 0;
                double SumValue = 0;
                for (int x = 0; x < p.Length; x++)
                {
                    if (p[x] == 1)
                    {
                        Deal d = li[x];
                        SumWeight += d.Weight;
                        SumValue += d.Value;
                    }
                }
                if (SumWeight > MaxWeight)
                    return 0;
                else
                    return (double)1 / (MaxGuess - SumValue);
            };

            //Revolution.ChooseStrategy = new RouletteChooseStrategy();
            //Revolution.Begin(func, 100, li.Count, 2000);
            double Value = 0;
            double Weight = 0;
            //Individual individual = Revolution.GetBestIndividual();
            //double dd = individual.Fitness;
            //for (int x = 0; x < individual.Chromosome.GeneArray.Length; x++)
            //{
            //    if (individual.Chromosome.GeneArray[x] == 1)
            //    {
            //        Deal d = li[x];
            //        Weight += d.Weight;
            //        Value += d.Value;
            //    }
            //}
            lblResult.Text = "C：" + MaxGuess + " 重量：" + Weight + " 价值：" + Value;
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
    /// <summary>
    /// 货物
    /// </summary>
    class Deal
    {
        public double Weight { get; set; }
        public double Value { get; set; }
    }
}
