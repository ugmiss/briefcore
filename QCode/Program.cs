using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QCode
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string ss = @"抚琴闲雨五湖静,搬子弈星九霄空,掬水留香千云醉,拈花一笑万山倾";
            string sss = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ss));//
            string s = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(sss));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
