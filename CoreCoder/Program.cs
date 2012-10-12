using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CoreCoder
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string x = BussinessExecuter.GetMyWhere<HT>(p => true == p.IsValid && p.IsValid && p.IsValid == false);

            Application.Run(new CodeForm());
        }
        class HT
        {
            public string ID { get; set; }
            public int Name { get; set; }
            public bool IsValid { get; set; }
        }
    }
}
