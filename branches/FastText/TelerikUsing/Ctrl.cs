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
    public class Ctrl
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
        public static void Init(Form form)
        {

        }

        public static void LocalizationInit()
        {
            RadGridLocalizationProvider.CurrentProvider = new MyChineseRadGridLocalizationProvider();
            RadMessageLocalizationProvider.CurrentProvider = new MyBGRadMessageLocalizationProvider();
        }
    }
}
