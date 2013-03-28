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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoForm());
        }
    }
}
