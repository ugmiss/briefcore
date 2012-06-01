using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Utility.Windows
{
    public class Screen
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,
            uint msg,
            uint wParam,
            int lParam);

        private const uint WM_SYSCOMMAND = 0x0112;
        private const uint SC_MONITORPOWER = 0xF170;
        public static void Close(IntPtr Handle)
        {
            SendMessage(Handle, WM_SYSCOMMAND, SC_MONITORPOWER, 2);  //关闭显示器;
        }
        public static void Open(IntPtr Handle)
        {
            SendMessage(Handle, WM_SYSCOMMAND, SC_MONITORPOWER, -1);  //关闭显示器;
        }
    }
}
