using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Text.RegularExpressions;

namespace TelerikUsing
{
    /// <summary>
    /// 异常窗体。
    /// </summary>
    public partial class ExBoxForm : Telerik.WinControls.UI.RadForm
    {
        /// <summary>
        /// 当前异常对象。
        /// </summary>
        Exception CurEx = null;
        /// <summary>
        /// 带参构造。
        /// </summary>
        /// <param name="ex">异常。</param>
        public ExBoxForm(Exception ex)
        {
            InitializeComponent();
            CurEx = ex;
        }
        /// <summary>
        /// 窗体加载。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExBox_Load(object sender, EventArgs e)
        {
            // 异常信息太长，换行显示。
            string msg = CurEx.Message;
            int length = GetLength(msg);
            char[] chars = msg.ToCharArray();
            msg="";
            int sp=84;
            for (int i = 0; i < chars.Length; i++)
            {
                msg += chars[i];
                if (GetLength(msg) % sp == 0 | GetLength(msg) % sp == 1 && GetLength(msg)>80)
                {
                    msg += "\n\r";
                }
            }
            this.radLabel1.Text = msg;
            this.radTextBox1.Text = CurEx.Message + "\r\n" + CurEx.Source + "\r\n" + CurEx.StackTrace;
        }
        /// <summary>
        /// 获取串占的字符数。
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static int GetLength(string strSource)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            int nLength = strSource.Length;

            for (int i = 0; i < strSource.Length; i++)
            {
                if (regex.IsMatch(strSource.Substring(i, 1)))
                {
                    nLength++;
                }
            }

            return nLength;
        }
        /// <summary>
        /// 展开显示详细信息。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButton2_Click(object sender, EventArgs e)
        {
            if (this.Height == 113)
            {
                this.Height = 349;
            }
            else
            {
                this.Height = 113;
            }
        }
        /// <summary>
        /// 关闭。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
