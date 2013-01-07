using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RecordTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool isRec = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRec)
            {
                mciSendString("close movie", "", 0, 0);
                mciSendString("open new type WAVEAudio alias movie", "", 0, 0);
                mciSendString("record movie", "", 0, 0);
            }
            else
            {
                mciSendString("stop movie", "", 0, 0);
                mciSendString("save movie D:\\1.wav", "", 0, 0);
                mciSendString("close movie", "", 0, 0);
            }
            isRec = !isRec;
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(
         string lpstrCommand,
         string lpstrReturnString,
         int uReturnLength,
         int hwndCallback
        );







    }
}
