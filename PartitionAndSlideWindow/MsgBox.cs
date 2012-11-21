using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PartitionAndSlideWindow
{
    public class MsgBox
    {
        public static DialogResult Show(string text)
        {
            return XtraMessageBox.Show(text);
        }

        public static DialogResult Show(Exception ex)
        {
            return XtraMessageBox.Show(ex.Message);
        }
    }
}
