using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Algorithms.Dijkstra;
using Geometria;

namespace AlgorithmDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Test.Try();
            var x = LineToLine.GetX(0, 0, 1, 3, 3, 4, 4, 3);
            var y = LineToLine.GetY(0, 0, 1, 3, 3, 4, 4, 3);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoForm());
        }
    }
}
