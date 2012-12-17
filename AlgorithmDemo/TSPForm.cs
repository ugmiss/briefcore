using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;

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
            double[][] DistanceMap = new double[citycount][];
            for (int i = 0; i < citycount; i++)
            {
                DistanceMap[i] = new double[citycount];
            }
            for (int i = 0; i < citycount; i++)
            {
                for (int k = 0; k < citycount; k++)
                {
                    if (i == k)
                        continue;
                    var CityA = cityList[i];
                    var CityB = cityList[k];
                    DistanceMap[i][k] = System.Math.Sqrt((CityA.X - CityB.X) * (CityA.X - CityB.X) + (CityA.Y - CityB.Y) * (CityA.Y - CityB.Y));
                }
            }
            panel1.Refresh();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (City c in cityList)
            {
                g.FillEllipse(Brushes.Red, c.X, c.Y, 3, 3);
            }
        }
    }

    public class City
    {
        public string CityName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
