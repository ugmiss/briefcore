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
            List<Deal> li = new List<Deal>();
            li.Add(new Deal() { Weight = 7, Value = 12 });
            li.Add(new Deal() { Weight = 3, Value = 12 });
            li.Add(new Deal() { Weight = 4, Value = 40 });
            li.Add(new Deal() { Weight = 5, Value = 25 });
            Func<Individual, double> func = p =>
            {
                int SumWeight = 0;
                int SumValue = 0;
                for (int x = 0; x < p.Chromosome.GeneArray.Length; x++)
                {
                    if (p.Chromosome.GeneArray[x] == 1)
                    {
                        Deal d = li[x];
                        SumWeight += d.Weight;
                        SumValue += d.Value;
                    }
                }
                if (SumWeight > 10)
                    return 0;
                else
                    return (double)SumValue;
            };


            Revolution.Begin(func, 5, 4, 20000);

            Population pp = Revolution.CurrentPopulation;

            double dd = FitnessFunction.GetFitnessRate(pp.IndividualList[0], func);

            var lis = Revolution.GetList().OrderByDescending(p => FitnessFunction.GetFitnessRate(p, func)).ToList();
            var s = FitnessFunction.GetFitnessRate(lis[0], func);
        }
    }

    class Deal
    {
        public int Weight { get; set; }
        public int Value { get; set; }
    }
}
