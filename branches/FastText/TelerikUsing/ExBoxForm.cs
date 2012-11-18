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
    /// �쳣���塣
    /// </summary>
    public partial class ExBoxForm : Telerik.WinControls.UI.RadForm
    {
        /// <summary>
        /// ��ǰ�쳣����
        /// </summary>
        Exception CurEx = null;
        /// <summary>
        /// ���ι��졣
        /// </summary>
        /// <param name="ex">�쳣��</param>
        public ExBoxForm(Exception ex)
        {
            InitializeComponent();
            CurEx = ex;
        }
        /// <summary>
        /// ������ء�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExBox_Load(object sender, EventArgs e)
        {
            // �쳣��Ϣ̫����������ʾ��
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
        /// ��ȡ��ռ���ַ�����
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
        /// չ����ʾ��ϸ��Ϣ��
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
        /// �رա�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
