using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (GISDataEntities GISDataEntities = new GISDataEntities())
            {
                GIS_Mine mine = new GIS_Mine() { Name = "aaa", IsValid = true };
                GISDataEntities.GIS_Mine.AddObject(mine);
                GISDataEntities.SaveChanges();
            }
        }
    }
}



