using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Utility;
using System.Threading;
using Algorithms;

namespace Snake
{

    public partial class MainForm : Form
    {
        public const int rowCount = 64;
        public const int colCount = 64;
        public Direction CurrentDirection { get; set; }

        public List<PointM> CurrentPlan { get; set; }
        public Queue<PointM> Snake = new Queue<PointM>();
        public PointM Bean { get; set; }
        int[][] Matrix = new int[rowCount][];
        bool IsGameOver = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < rowCount; i++)
            {
                Matrix[i] = Enumerable.Repeat(0, rowCount).ToArray();
            }
            Init();
            do
            {
                Loop();
            } while (!IsGameOver);
        }

        public void Loop()
        {
            if (CurrentPlan == null)
                CurrentPlan = GetNewPlan();
            Thread.Sleep(500);
            PointM head = Snake.ToArray()[Snake.Count - 1];
            Direction d = CurrentDirection;
            int x = head.X + ((d == Direction.Right) ? 1 : 0) + ((d == Direction.Left) ? -1 : 0);
            int y = head.Y + ((d == Direction.Down) ? 1 : 0) + ((d == Direction.Up) ? -1 : 0);
            if (Bean.X == x && Bean.Y == y)
            {
                Snake.Enqueue(new PointM() { X = Bean.X, Y = Bean.Y });
                Bean = RandomBean();
            }
            else if (x >= rowCount || x < 0 || y < 0 || y >= colCount || Matrix[x][y] == 1)
            {
                GameOver();
            }
            else
            {
                Snake.Enqueue(new PointM() { X = x, Y = y });
                Snake.Dequeue();
            }
            CurrentDirection = GetNextDirection();
            Refresh();
        }

        void Refresh()
        {
            for (int i = 0; i < rowCount; i++)
            {
                Matrix[i] = Enumerable.Repeat(0, rowCount).ToArray();
            }
            Snake.ForEach(p =>
            {
                Matrix[p.X][p.Y] = 1;
            });
            Matrix[Bean.X][Bean.Y] = 1;

            pnlCanvas.Refresh();
        }

        void GameOver()
        {
            IsGameOver = true;
            MessageBox.Show("");
        }
        Direction GetNextDirection()
        {
            PointM head = Snake.ToArray()[Snake.Count - 1];
            PointM next = CurrentPlan[0];
            CurrentPlan.Remove(next);
            if ((next.X - head.X) == 1)
                return Direction.Right;
            if ((next.X - head.X) == -1)
                return Direction.Left;
            if ((next.Y - head.Y) == 1)
                return Direction.Down;
            if ((next.Y - head.Y) == -1)
                return Direction.Up;
            return CurrentDirection;
        }
        bool TestPointM(PointM p)
        {
            int x = p.X;
            int y = p.Y;
            PointM head = Snake.ToArray()[Snake.Count - 1];
            PointM tag = Bean;
            if (p.X == head.X && p.Y == head.Y)
                return true;
            if (p.X == tag.X && p.Y == tag.Y)
                return true;
            if (x >= rowCount || x < 0 || y < 0 || y >= colCount || Matrix[x][y] == 1)
            {
                return false;
            }
            return true;
        }

        public List<PointM> GetNewPlan()
        {
            List<PointM> Plan = new List<PointM>();
            PointM head = Snake.ToArray()[Snake.Count - 1];
            PointM tag = Bean;
            List<Vertex> vlist = new List<Vertex>();
            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < colCount; k++)
                {
                    if (Matrix[i][k] == 1)
                    {
                        if ((i == head.X && k == head.Y) || (i == tag.X && k == tag.Y))
                        {

                        }
                        else
                        {
                            continue;
                        }
                    }
                    List<Edge> list = new List<Edge>();
                    PointM M = new PointM() { X = i, Y = k };
                    Vertex v = new Vertex() { ID = M.ID, Tag = M };

                    PointM R = new PointM(i + 1, k);
                    PointM L = new PointM(i - 1, k);
                    PointM U = new PointM(i, k - 1);
                    PointM D = new PointM(i, k + 1);

                    if (TestPointM(R))
                        list.Add(new Edge() { FromID = M.ID, ToID = R.ID, Weight = 1.0 });
                    if (TestPointM(L))
                        list.Add(new Edge() { FromID = M.ID, ToID = L.ID, Weight = 1.0 });
                    if (TestPointM(U))
                        list.Add(new Edge() { FromID = M.ID, ToID = U.ID, Weight = 1.0 });
                    if (TestPointM(D))
                        list.Add(new Edge() { FromID = M.ID, ToID = D.ID, Weight = 1.0 });
                    v.EdgeList = list;
                    vlist.Add(v);
                }
            }

            //非负权有向图的最短路径Dijkstra算法。
            Route p = Dijkstra.GetShortestRoute(vlist, head.ID, tag.ID);
            foreach (string id in p.IDList)
            {
                string[] xy = id.Replace(")", "").Replace("(", "").Split(',');
                PointM temp = new PointM(xy[0].ParseTo<int>(), xy[1].ParseTo<int>());
                Plan.Add(temp);
            }
            return Plan;
        }

        public void Init()
        {
            Snake.Enqueue(new PointM() { X = 0, Y = 0 });
            Snake.Enqueue(new PointM() { X = 1, Y = 0 });
            Snake.Enqueue(new PointM() { X = 2, Y = 0 });
            Snake.ForEach(p =>
            {
                Matrix[p.X][p.Y] = 1;
            });
            Bean = RandomBean();
            Matrix[Bean.X][Bean.Y] = 1;
            CurrentDirection = Direction.Right;
        }
        public PointM RandomBean()
        {
            int row = RandomFactory.Next(rowCount);
            int col = RandomFactory.Next(colCount);
            if (Matrix[row][col] == 0)
            {
                return new PointM() { X = row, Y = col };
            }
            else
            {
                return RandomBean();
            }
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.LightGray, 0, 0, pnlCanvas.Width, pnlCanvas.Height);
            //g.Clear(Color.White);
            int len = pnlCanvas.Width / rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < colCount; k++)
                {
                    if (Matrix[i][k] == 1)
                    {
                        g.FillRectangle(Brushes.White, i * len, k * len, len, len);
                    }
                }
            }
        }
    }
    public enum Direction
    {
        Up, Right, Down, Left
    }
    public class PointM
    {
        public PointM() { }
        public PointM(int x, int y)
        {
            X = x;
            Y = y;
        }
        public string ID
        {
            get
            {
                return "(" + X + "," + Y + ")";
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
