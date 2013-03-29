using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Algorithms.Genetic;
using System.Threading;

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
            pblist.Clear();
            pwlist.Clear();
            panel1.Refresh();
            double[] rights;
            int[] blacks = Enumerable.Range(0, 25).ToArray();

            int sum = FiveTigerGeneticHelper.GetBlackPoint(blacks, out rights);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Brown);
            Pen pb = new Pen(Color.Black);
            g.DrawLine(p, 48, 48, 48, 252);
            g.DrawLine(p, 48, 48, 252, 48);
            g.DrawLine(p, 252, 48, 252, 252);
            g.DrawLine(p, 48, 252, 252, 252);
            for (int i = 0; i < 5; i++)
            {
                g.DrawLine(p, 50 + i * 50, 50, 50 + i * 50, 250);
                g.DrawLine(p, 50, 50 + i * 50, 250, 50 + i * 50);
            }
            foreach (Point point in pblist)
            {
                g.FillEllipse(Brushes.Black, point.X - 15, point.Y - 15, 30, 30);
            }
            foreach (Point point in pwlist)
            {
                g.FillEllipse(Brushes.White, point.X - 15, point.Y - 15, 30, 30);
            }
        }
        List<Point> pblist = new List<Point>();
        List<Point> pwlist = new List<Point>();

        public int[] Blacks()
        {
            List<int> li = new List<int>();
            foreach (var p in pblist)
            {
                int x = p.X / 50 - 1;
                int y = p.Y / 50 - 1;
                li.Add(x + 5 * y);
            }
            return li.OrderBy(x => x).ToArray();
        }

        public int[] Whites()
        {
            List<int> li = new List<int>();
            foreach (var p in pwlist)
            {
                int x = p.X / 50 - 1;
                int y = p.Y / 50 - 1;
                li.Add(x + 5 * y);
            }
            return li.OrderBy(x => x).ToArray();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

            int x = (e.X + 15) / 50 * 50;
            int y = (e.Y + 15) / 50 * 50;
            if (x < 50 || x > 250 || y < 50 || y > 250)
                return;
            Point pp = pblist.Find(p => p.X == x && p.Y == y);
            if (pp.X == 0 && pp.Y == 0)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    pblist.Add(new Point(x, y));
                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        int inx = Calc();
                        pwlist.Add(new Point(inx % 5 * 50 + 50, inx / 5 * 50 + 50));
                        panel1.Refresh();
                    });
                }
                //else
                //{
                //   
                //}
                panel1.Refresh();
            }
        }

        int Calc()
        {
            int[] blacks = Blacks();
            int[] whites = Whites();

            FiveTigerGeneticHelper helper = new FiveTigerGeneticHelper(blacks, whites);
            Revolution revolution = new Revolution();
            revolution.InitData(50, 25 - blacks.Length - whites.Length, 200, 50);
            revolution.InitFunc(FiveTigerGeneticHelper.FitnessFunc, FiveTigerGeneticHelper.ChromosomeFunc,
                FiveTigerGeneticHelper.ChooseFunc, FiveTigerGeneticHelper.CrossFunc, FiveTigerGeneticHelper.MutationAction);
            revolution.Begin();
            int[] best = revolution.BestIndividual;

            int[] b = new int[25];
            for (int i = 0; i < blacks.Length; i++)
            {
                b[blacks[i]] = 1;
            }
            for (int i = 0; i < whites.Length; i++)
            {
                b[whites[i]] = -1;
            }
            int index = 0;
            int pindex = 0;
            while (true)
            {
                if (index == 24)
                    break;
                if (b[index] == 0)
                {
                    b[index] = best[pindex] == 1 ? 1 : -1;
                    pindex++;
                }
                else
                {
                    index++;
                }
            }
            double[] brights;
            for (int i = 0; i < 25; i++)
            {
                b[i] = b[i] == 1 ? 1 : 0;
            }
            int bpoint = FiveTigerGeneticHelper.GetBlackPoint(b, out brights);
            for (int i = 0; i < 25; i++)
            {
                b[i] = b[i] == 1 ? 0 : 1;
            }
            //int wpoint = FiveTigerGeneticHelper.GetBlackPoint(b, out brights);

            for (int i = 0; i < blacks.Length; i++)
            {
                brights[blacks[i]] = 0;
            }
            for (int i = 0; i < whites.Length; i++)
            {
                brights[whites[i]] = 0;
            }
            int bestindex = 0;
            double bestrights = 0;
            for (int n = 0; n < brights.Length; n++)
            {
                if (brights[n] >= bestrights)
                {
                    bestrights = brights[n];
                    bestindex = n;
                }
            }
            return bestindex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Blacks();
        }

        private void FiveTigerForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
