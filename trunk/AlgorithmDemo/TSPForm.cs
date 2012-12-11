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

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int citycount = txtCityNum.Text.ParseTo<int>();

            for (int i = 0; i < citycount; i++)
            {
                City c = new City();
                c.X = RandomFactory.Next(panel1.Width);
                c.Y = RandomFactory.Next(panel1.Height);
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
