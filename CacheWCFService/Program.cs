using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CacheBusiness;

namespace CacheWCFService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            BusinessManager.StartUp(new StaticBusiness());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerForm());
        }
    }
}
