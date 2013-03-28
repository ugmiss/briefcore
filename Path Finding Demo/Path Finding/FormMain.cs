// Upload to codeproject.com
// By Ibraheem AlKilanny
// d3_ib@hotmail.com - http://sites.google.com/site/ibraheemalkilany/
// all rights reserved 2011

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Path_Finding
{
    internal partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            
            // preventing IndexOutOfRangeException
            this.comboBox1.SelectedIndex = 0;

            // initializing
            this.graph = new Graph(this.pictureBoxDraw.Width, this.pictureBoxDraw.Height,
                new Size(this.pictureBoxDraw.Width / Vertex.Size, this.pictureBoxDraw.Height / Vertex.Size));

            this.graph.PathFound += new EventHandler(graph_PathFound);

            // causes to update the speed in graph object
            this.numericUpDownSpeed_ValueChanged(this, null);

            this.toolStripStatusLabelAbout_Click(this, null); // show about
        }

        private Graph graph;

        private Graph.Algorithms algorithm;

        private Stopwatch watch = new Stopwatch();

        private bool working
        {
            get { return !this.button1.Enabled; }
        }

        private bool canDrawPath;

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.canDrawPath = false;
            this.comboBox1.Enabled = false;
            this.numericUpDownCellSize.Enabled = false;
            this.numericUpDownSpeed.Enabled = false;
            this.toolStripStatusLabel1.Text = "Finding path...";

            // reset graph from last operation
            this.graph.Reset(false);
            this.algorithm = (Graph.Algorithms)this.comboBox1.SelectedIndex;

            // calculate the timer interval
            if (this.numericUpDownCellSize.Value < 10)
                this.timerDraw.Interval = (int)this.numericUpDownCellSize.Value;
            else
                this.timerDraw.Interval = 50;
            this.timerDraw.Enabled = true;
            this.backgroundWorker1.RunWorkerAsync(this.comboBox1.SelectedIndex);
        }

        private void pictureBoxDraw_Paint(object sender, PaintEventArgs e)
        {
            this.graph.Draw(e.Graphics, this.algorithm);
            if (this.canDrawPath)
            {
                this.graph.DrawPath(e.Graphics);
            }


            if (this.graph.PathLength > 0)
                this.labelLength.Text = String.Format("Path Length = {0:n0} units", this.graph.PathLength);
            else
                this.labelLength.Text = "Path not found";
        }

        private void graph_PathFound(object sender, EventArgs e)
        {
            this.timerDraw.Enabled = false;
            Application.DoEvents();
            this.canDrawPath = true;
            this.pictureBoxDraw.Invalidate();
        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {
            this.pictureBoxDraw.Invalidate();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.watch.Reset();
            this.watch.Start();
            this.graph.PathFinding((Graph.Algorithms)e.Argument);
            this.watch.Stop();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.MaximizeBox = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.button1.Enabled = true;
            this.comboBox1.Enabled = true;
            this.numericUpDownCellSize.Enabled = true;
            this.numericUpDownSpeed.Enabled = true;
            this.toolStripStatusLabel1.Text = "Operation Completed";
            long totalTime = this.watch.ElapsedMilliseconds;
            this.labelTime.Text = String.Format("Time elapsed = {0:n0} ms", this.watch.ElapsedMilliseconds);
            this.pictureBoxDraw.Select();
        }

        private void toolStripStatusLabelAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "This program demonstrates the path finding with the two popular algorithms:  Dijkstra and A*\n"
                + "For distance between two neighbour squares, we assume 10 units for vertical and horizontal neighbours, and 14 for diagonal neighbour.\n"
                + "The program provides 4 various algorithms to find path: Dijkstra, A*, Bi-Directional Dijkstra and Bi-Directional A*.\n\n\n"
                + " *** Instructions ***\n\n"
                + "  * Right\\left click the mouse to add\\remove walls.\n"
                + "  * The start point is indicated with green, and the end (target) point is indicated with red.\n"
                + "  * Press arrows, w,a,s and d keys to change positions of start and end points, respectively.\n"
                + "  * Press 'R' to reset map, and 'G' to generate random walls.\n\n"
            + "By Ibraheem AlKilanny - Nov 2011\nMore programs on\nhttp://sites.google.com/site/ibraheemalkilany/",
                "Path Finding Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.pictureBoxDraw.Select(); // so that user can move start\end points
        }

        private void numericUpDownSpeed_ValueChanged(object sender, EventArgs e)
        {
            this.graph.Sleep = (100 - (int)this.numericUpDownSpeed.Value) * 3;
            this.pictureBoxDraw.Select(); // so that user can move start\end points
        }

        private void pictureBoxDraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.working)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.graph.MakeWall(this.graph.GetVertex(e.Location));
                    this.pictureBoxDraw.Invalidate();
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.graph.RemoveWall(this.graph.GetVertex(e.Location));
                    this.pictureBoxDraw.Invalidate();
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!this.working && this.graph != null)
            {
                this.graph.Resize(new Size(this.pictureBoxDraw.Width / Vertex.Size, this.pictureBoxDraw.Height / Vertex.Size));

                this.canDrawPath = false;
                this.pictureBoxDraw.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.working)
                return;
            switch (e.KeyCode)
            {
                case Keys.R:
                    this.graph.Reset(true);
                    this.canDrawPath = false;
                    break;
                case Keys.G:
                    this.graph.MakeRandomWalls();
                    this.canDrawPath = false;
                    break;
                case Keys.Up:
                    if (this.graph.Start.Y - 1 >= 0)
                        this.graph.Start.Y--;
                    break;
                case Keys.Down:
                    if (this.graph.Start.Y + 1 < this.graph.Size.Height)
                        this.graph.Start.Y++;
                    break;
                case Keys.Right:
                    if (this.graph.Start.X + 1 < this.graph.Size.Width)
                        this.graph.Start.X++;
                    break;
                case Keys.Left:
                    if (this.graph.Start.X - 1 >= 0)
                        this.graph.Start.X--;
                    break;
                case Keys.W:
                    if (this.graph.End.Y - 1 >= 0)
                        this.graph.End.Y--;
                    break;
                case Keys.A:
                    if (this.graph.End.X - 1 >= 0)
                        this.graph.End.X--;
                    break;
                case Keys.S:
                    if (this.graph.End.Y + 1 < this.graph.Size.Height)
                        this.graph.End.Y++;
                    break;
                case Keys.D:
                    if (this.graph.End.X + 1 < this.graph.Size.Width)
                        this.graph.End.X++;
                    break;
            }
            this.pictureBoxDraw.Invalidate();
        }

        private void numericUpDownCellSize_ValueChanged(object sender, EventArgs e)
        {
            Vertex.Size = (int)this.numericUpDownCellSize.Value;
            this.Form1_Resize(sender, e);
            this.pictureBoxDraw.Select(); // so that user can move start\end points
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pictureBoxDraw.Select(); // so that user can move start\end points
        }
    }
}
