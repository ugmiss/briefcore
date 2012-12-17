using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;
using Algorithms.Genetic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace AlgorithmDemo
{
    public partial class TSPForm : Form
    {
        public TSPForm()
        {
            InitializeComponent();
        }
        List<City> cityList = new List<City>();

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int citycount = txtCityNum.Text.ParseTo<int>();
            cityList.Clear();
            for (int i = 0; i < citycount; i++)
            {
                City c = new City();
                c.X = RandomFactory.Next(panel1.Width);
                c.Y = RandomFactory.Next(panel1.Height);
                cityList.Add(c);
            }
            result = null;
            panel1.Refresh();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (City c in cityList)
            {
                g.FillEllipse(Brushes.Red, c.X, c.Y, 3, 3);
            }
            if (result != null)
            {
                Pen p = new Pen(Color.Blue);
                for (int i = 0; i < result.Length; i++)
                {
                    var ca = cityList[result[i]];
                    var cb = i + 1 == result.Length ? cityList[result[0]] : cityList[result[i + 1]];
                    g.DrawLine(p, ca.X, ca.Y, cb.X, cb.Y);
                }
            }
        }
        int[] result = null;
        private void btnCalc_Click(object sender, EventArgs e)
        {
            TSPGeneticHelper helper = new TSPGeneticHelper(cityList);
           
                Revolution revolution = new Revolution();
                revolution.OnBestChange += new BestChange(revolution_OnBestChange);
                revolution.InitData(500, cityList.Count, 5000, 500);
                revolution.InitFunc(TSPGeneticHelper.FitnessFunc, TSPGeneticHelper.ChromosomeFunc, TSPGeneticHelper.ChooseFunc, TSPGeneticHelper.CrossFunc, TSPGeneticHelper.MutationAction);
                revolution.Begin();
           
        }

        void revolution_OnBestChange(Revolution sender)
        {
            result = sender.BestIndividual;



            lblLength.Text = "长度：" + TSPGeneticHelper.GetBestLenth(result);
            lbltimes.Text = sender.CurrentGenarationIndex.ToString();

            panel1.Refresh();

        }


        private void TSPForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            btnRandom_Click(null, null);
            panel1.Refresh();
        }
    }
}
