using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TelerikUsing;

namespace FastText
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Ctrl.LocalizationInit();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex);
            }
        }
    }
}

