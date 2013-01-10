using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FileShare
            //string filename = AppDomain.CurrentDomain.BaseDirectory + "log//" + cmblogfile.Text;
            //FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //rtblog.Text = sr.ReadToEnd();
            //sr.Close();
            //fs.Close();  
            Application.Run(new Form1());
        }
    }
}
