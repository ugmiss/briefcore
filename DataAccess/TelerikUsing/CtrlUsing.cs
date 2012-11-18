using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls;

namespace TelerikUsing
{
    public class CtrlUsing
    {
        public static void Init(params object[] controls)
        {
            foreach (object control in controls)
            {
                if (control is Telerik.WinControls.UI.RadGridView)
                {
                    RadGridView radGridView = control as RadGridView;
                    radGridView.ShowGroupPanel = false;
                    radGridView.MasterGridViewTemplate.AllowAddNewRow = false;
                    radGridView.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                    radGridView.MasterGridViewTemplate.AddNewRowPosition = SystemRowPosition.Bottom;
                    radGridView.AllowColumnReorder = false;
                    radGridView.AllowRowResize = false;
                    radGridView.ShowRowHeaderColumn = false;
                    radGridView.CaseSensitive = false;
                }
            }
        }

        public static void ValidEmpty(Control c, string name)
        {
            if (c is TextBox)
            {
                if (string.IsNullOrEmpty(((TextBox)c).Text))
                {
                    throw new BusinessException(name + "不能为空");
                }
            }
            if (c is RadTextBox)
            {
                if (string.IsNullOrEmpty(((RadTextBox)c).Text))
                {
                    throw new BusinessException(name + "不能为空");
                }
            }
            if (c is RadLabel)
            {
                if (string.IsNullOrEmpty(((RadLabel)c).Text))
                {
                    throw new BusinessException(name + "不能为空");
                }
            }
        }

        public static void Disable(params Control[] cs)
        {
            foreach (Control c in cs)
            {
                c.Enabled = false;
            }
        }

        public static void Enable(params Control[] cs)
        {
            foreach (Control c in cs)
            {
                c.Enabled = true;
            }
        }

    }
    public class BusinessException : Exception
    {
        public BusinessException(string Msg)
            : base(Msg)
        {
        }
    }

}
