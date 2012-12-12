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
        List<Route> routeList = new List<Route>();

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
            for (int i = 0; i < citycount; i++)
            {
                for (int k = 0; k < citycount; k++)
                {
                    if (i == k)
                        continue;
                    Route r = new Route();
                    r.CityA = cityList[i];
                    r.CityB = cityList[k];
                    r.Distance = System.Math.Sqrt((r.CityA.X - r.CityB.X) * (r.CityA.X - r.CityB.X) + (r.CityA.X - r.CityB.X) * (r.CityA.X - r.CityB.X));
                }
            }


        }
    }

    public class Route
    {
        public City CityA { get; set; }
        public City CityB { get; set; }
        public double Distance { get; set; }
    }


    public class City
    {
        public string CityName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
