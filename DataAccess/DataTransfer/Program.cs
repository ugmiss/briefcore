using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TelerikUsing;
using System.IO;

namespace DataTransfer
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
                TelerikUsing.Environment.ApplicationInit();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Enviroment.xml"))
                {
                    new InitForm().ShowDialog();
                }
                Application.Run(new TransferMainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }
    }
}
