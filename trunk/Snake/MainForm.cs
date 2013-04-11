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

namespace Snake
{

    public partial class MainForm : Form
    {
        public const int rowCount = 10;
        public const int colCount = 10;
        public Direction CurrentDirection { get; set; }
        public Queue<PointM> Snake = new Queue<PointM>();
        public PointM Bean { get; set; }
        int[][] Matrix = new int[rowCount][];
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < rowCount; i++)
            {
                Matrix[i] = Enumerable.Repeat(0, 10).ToArray();
            }
        }

        public void Loop()
        {

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
