using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Snake
{
    public partial class MainForm : Form
    {
        public int rowCount = 10;
        public int colCount = 10;
        public Direction CurrentDirection { get; set; }
        public Queue<Point> Snake = new Queue<Point>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int[][] Matrix = new int[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                Matrix[i] = Enumerable.Repeat(0, 10).ToArray();
            }
        }
        public void Init()
        {

        }
        public void RandomBean()
        {
        }
    }
    public enum Direction
    {
        Up, Right, Down, Left
    }
}
