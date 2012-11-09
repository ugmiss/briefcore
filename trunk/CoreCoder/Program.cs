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

            Utility.Hanzi.CnNameFactory fac = new Utility.Hanzi.CnNameFactory();
            List<string> li = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                string name = fac.GetNewName();
                if (li.Contains(name))
                {
                    i--;
                    continue;
                }
                li.Add(name);
            }
            foreach (var c in li.OrderBy(p => p))
            {
                Console.Write(c + "\t");
            }
            Console.ReadKey();
            //Application.Run(new CodeForm());
        }
        class HT
        {
            public string ID { get; set; }
            public int Name { get; set; }
            public bool IsValid { get; set; }
        }
    }
}
