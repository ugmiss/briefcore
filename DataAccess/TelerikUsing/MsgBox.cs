using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls;
using System.IO;

namespace TelerikUsing
{
    /// <summary>
    /// 消息箱。
    /// </summary>
    public class MsgBox : RadMessageBox
    {
        /// <summary>
        /// 普通信息。
        /// </summary>
        /// <param name="text"></param>
        public static new void Show(string text)
        {
            RadMessageBox.Show(text, "信息");
        }
        /// <summary>
        /// 异常信息。
        /// </summary>
        /// <param name="ex"></param>
        public static void Show(Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "error.log",true,Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.Source==null?"":ex.Source.ToString());
                sw.WriteLine(ex.StackTrace==null?"":ex.StackTrace.ToString());
                sw.WriteLine(ex.InnerException==null?"":ex.InnerException.Message);
            }
            new ExBoxForm(ex).ShowDialog();
        }
    }
}
