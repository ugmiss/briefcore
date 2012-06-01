using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;

namespace Utility.Windows
{
    public class DeskTop
    {
        /// <summary>
        /// 设置墙纸。
        /// </summary>
        /// <param name="path"></param>
        public static void SetDeskTop(string path)
        {
            Bitmap bmp = (Bitmap)Image.FromFile(path);
            bmp.Save(AppDomain.CurrentDomain.BaseDirectory + "/Temp.png", ImageFormat.Bmp);
            string temppath = AppDomain.CurrentDomain.BaseDirectory + "/Temp.png";
            SystemParametersInfo(20, 1, temppath, 1);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    }
}
