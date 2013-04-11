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

namespace Snake
{

    public partial class MainForm : Form
    {
        public const int rowCount = 64;
        public const int colCount = 64;
        public Direction CurrentDirection { get; set; }
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
            return Direction.Right;
        }


        public List<PointM> GetNewPlan()
        {
            return null;
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
    public struct PointM
    {
        public int X;
        public int Y;
    }
}
