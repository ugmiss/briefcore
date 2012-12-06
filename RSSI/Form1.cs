using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RSSI
{
    public partial class Form1 : Form
    {
        public event EventHandler FormClick;
        public Form1()
        {
            InitializeComponent();
        }
        private void btnCalc_Click(object sender, EventArgs e)
        {

            double x1 = Convert.ToDouble(tx1.Text);
            double x2 = Convert.ToDouble(tx2.Text);
            double x3 = Convert.ToDouble(tx3.Text);
            double x4 = Convert.ToDouble(tx4.Text);

            double y1 = Convert.ToDouble(ty1.Text);
            double y2 = Convert.ToDouble(ty2.Text);
            double y3 = Convert.ToDouble(ty3.Text);
            double y4 = Convert.ToDouble(ty4.Text);

            double d1 = Convert.ToDouble(td1.Text);
            double d2 = Convert.ToDouble(td2.Text);
            double d3 = Convert.ToDouble(td3.Text);
            double d4 = Convert.ToDouble(td4.Text);

            double xA = Calctor.GetY(y1, y2, y3, x1, x2, x3, d1, d2, d3);
            double yA = Calctor.GetY(x1, x2, x3, y1, y2, y3, d1, d2, d3);



            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            double x1 = Convert.ToDouble(tx1.Text);
            double x2 = Convert.ToDouble(tx2.Text);
            double x3 = Convert.ToDouble(tx3.Text);
            double x4 = Convert.ToDouble(tx4.Text);

            double y1 = Convert.ToDouble(ty1.Text);
            double y2 = Convert.ToDouble(ty2.Text);
            double y3 = Convert.ToDouble(ty3.Text);
            double y4 = Convert.ToDouble(ty4.Text);

            double d1 = Convert.ToDouble(td1.Text);
            double d2 = Convert.ToDouble(td2.Text);
            double d3 = Convert.ToDouble(td3.Text);
            double d4 = Convert.ToDouble(td4.Text);

            double xA = Calctor.GetY(y1, y2, y3, x1, x2, x3, d1, d2, d3);
            double yA = Calctor.GetY(x1, x2, x3, y1, y2, y3, d1, d2, d3);

            double xB = Calctor.GetY(y4, y2, y3, x4, x2, x3, d4, d2, d3);
            double yB = Calctor.GetY(x4, x2, x3, y4, y2, y3, d4, d2, d3);

            double xC = Calctor.GetY(y1, y2, y4, x1, x2, x4, d1, d2, d4);
            double yC = Calctor.GetY(x1, x2, x4, y1, y2, y4, d1, d2, d4);

            double xD = Calctor.GetY(y1, y4, y3, x1, x4, x3, d1, d4, d3);
            double yD = Calctor.GetY(x1, x4, x3, y1, y4, y3, d1, d4, d3);


            Graphics g = e.Graphics;
            Pen p1 = new Pen(Color.Blue);
            Pen p2 = new Pen(Color.Green);
            Pen p3 = new Pen(Color.Red);
            Pen p4 = new Pen(Color.Black);

            Pen pl = new Pen(Color.Blue);
            float r = 10;
            g.FillEllipse(Brushes.Blue, (float)x1 - r, (float)y1 - r, 2 * r, 2 * r);
            g.FillEllipse(Brushes.Green, (float)x2 - r, (float)y2 - r, 2 * r, 2 * r);
            g.FillEllipse(Brushes.Red, (float)x3 - r, (float)y3 - r, 2 * r, 2 * r);

            g.DrawEllipse(p1, (float)(x1 - d1), (float)(y1 - d1), (float)d1 * 2, (float)d1 * 2);
            g.DrawEllipse(p2, (float)(x2 - d2), (float)(y2 - d2), (float)d2 * 2, (float)d2 * 2);
            g.DrawEllipse(p3, (float)(x3 - d3), (float)(y3 - d3), (float)d3 * 2, (float)d3 * 2);
          // g.DrawEllipse(p4, (float)(x4 - d4), (float)(y4 - d4), (float)d4 * 2, (float)d4 * 2);



            g.DrawLine(p1, (float)x1, (float)y1, (float)xA, (float)yA);
            g.DrawLine(p2, (float)x2, (float)y2, (float)xA, (float)yA);
            g.DrawLine(p3, (float)x3, (float)y3, (float)xA, (float)yA);
            if (false)
            {
                g.FillEllipse(Brushes.Black, (float)x4 - r, (float)y4 - r, 2 * r, 2 * r);

                g.DrawLine(p3, (float)x3, (float)y3, (float)xB, (float)yB);
                g.DrawLine(p2, (float)x2, (float)y2, (float)xB, (float)yB);
                g.DrawLine(p4, (float)x4, (float)y4, (float)xB, (float)yB);


                g.DrawLine(p1, (float)x1, (float)y1, (float)xC, (float)yC);
                g.DrawLine(p2, (float)x2, (float)y2, (float)xC, (float)yC);
                g.DrawLine(p4, (float)x4, (float)y4, (float)xC, (float)yC);


                g.DrawLine(p1, (float)x1, (float)y1, (float)xD, (float)yD);
                g.DrawLine(p3, (float)x3, (float)y3, (float)xD, (float)yD);
                g.DrawLine(p4, (float)x4, (float)y4, (float)xD, (float)yD);
            }
            lbl1.Text = "(x - x1) * (x - x1) + (y - y1) * (y - y1)=" + ((xA - x1) * (xA - x1) + (yA - y1) * (yA - y1)).ToString();
            lbl1res.Text = "d1*d1=" + (d1 * d1).ToString();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            return;
            ThreadPool.QueueUserWorkItem(o =>
            {
                double x1 = Convert.ToDouble(tx1.Text);
                double x2 = Convert.ToDouble(tx2.Text);
                double x3 = Convert.ToDouble(tx3.Text);
                double x4 = Convert.ToDouble(tx4.Text);

                double y1 = Convert.ToDouble(ty1.Text);
                double y2 = Convert.ToDouble(ty2.Text);
                double y3 = Convert.ToDouble(ty3.Text);
                double y4 = Convert.ToDouble(ty4.Text);

                double d1 = Convert.ToDouble(td1.Text);
                double d2 = Convert.ToDouble(td2.Text);
                double d3 = Convert.ToDouble(td3.Text);
                double d4 = Convert.ToDouble(td4.Text);
                Random r = new Random(Guid.NewGuid().GetHashCode());
                //td1.Text = Math.Sqrt((double)((e.X - x1) * (e.X - x1) + (e.Y - y1) * (e.Y - y1)) + 10000).ToString();
                //td2.Text = Math.Sqrt((double)((e.X - x2) * (e.X - x2) + (e.Y - y2) * (e.Y - y2)) + 10000).ToString();
                //td3.Text = Math.Sqrt((double)((e.X - x3) * (e.X - x3) + (e.Y - y3) * (e.Y - y3)) + 10000).ToString();
                //td4.Text = Math.Sqrt((double)((e.X - x4) * (e.X - x4) + (e.Y - y4) * (e.Y - y4)) + 10000).ToString();

                td1.Text = Math.Sqrt((double)((e.X - x1) * (e.X - x1) + (e.Y - y1) * (e.Y - y1)) + r.NextDouble() * 50000 - 30000).ToString();
                td2.Text = Math.Sqrt((double)((e.X - x2) * (e.X - x2) + (e.Y - y2) * (e.Y - y2)) + r.NextDouble() * 50000 - 30000).ToString();
                td3.Text = Math.Sqrt((double)((e.X - x3) * (e.X - x3) + (e.Y - y3) * (e.Y - y3)) + r.NextDouble() * 50000 - 30000).ToString();
                td4.Text = Math.Sqrt((double)((e.X - x4) * (e.X - x4) + (e.Y - y4) * (e.Y - y4)) + r.NextDouble() * 50000 - 30000).ToString();
               // Thread.Sleep(500);
                panel1.Refresh();
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
