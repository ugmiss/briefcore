using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace GenUI
{
    public static class DevExtensions
    {
        public static void Init(this GridView gridView1)
        {
            gridView1.OptionsView.ShowGroupPanel = false;
        }

        public static void Init(this GridControl gridControl1)
        {
            (gridControl1.MainView as GridView).OptionsView.ShowGroupPanel = false;
        }
        public static void Init(this ComboBoxEdit comboBoxEdit1)
        {
            comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }
    }
}

