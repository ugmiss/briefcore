using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace System
{
    public class FormUsing
    {
        public static void ThemeInit(string theme)
        {

        }

        public static void FormInit(Form form)
        {
            form.ShowIcon = false;
            form.StartPosition = FormStartPosition.CenterParent;
        }
        public static void FormInit(Form form, InitType initType)
        {
            if (initType == InitType.FixDialog)
            {
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
            }
            form.ShowIcon = false;
            form.StartPosition = FormStartPosition.CenterParent;
        }

        public static void FormInit(Form form, InitType initType,int width)
        {
            FormInit(form, initType);
            form.Width = width;
        }
    }
    public enum InitType
    {
        /// <summary>
        /// 固定大小窗体。
        /// </summary>
        [Description("固定窗口")]
        FixDialog,
        /// <summary>
        /// 可编辑大小窗体。
        /// </summary>
        [Description("可编辑")]
        Sizeable
    }
}
