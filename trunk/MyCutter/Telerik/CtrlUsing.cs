using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls;

namespace System
{
    public static class CtrlUsing
    {
        public static void Init(this RadGridView radGridView)
        {
            radGridView.ShowGroupPanel = false;
            radGridView.AllowEditRow = false;
            radGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            radGridView.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            radGridView.MasterGridViewTemplate.AddNewRowPosition = SystemRowPosition.Bottom;
            radGridView.AllowColumnReorder = false;
            radGridView.AllowRowResize = false;
            radGridView.ShowRowHeaderColumn = true;
            radGridView.CaseSensitive = false;
        }

        public static void Init(params object[] controls)
        {
            foreach (object control in controls)
            {
                if (control is Telerik.WinControls.UI.RadGridView)
                {
                    RadGridView radGridView = control as RadGridView;
                    radGridView.ShowGroupPanel = false;
                    radGridView.AllowEditRow = false;
                    radGridView.MasterGridViewTemplate.AllowAddNewRow = false;
                    radGridView.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                    radGridView.MasterGridViewTemplate.AddNewRowPosition = SystemRowPosition.Bottom;
                    radGridView.AllowColumnReorder = false;
                    radGridView.AllowRowResize = false;
                    radGridView.ShowRowHeaderColumn = true;
                    radGridView.CaseSensitive = false;
                }
            }
        }
        public static void InitGridColumns(this RadGridView radGridView1, Dictionary<string, string> colmap)
        {
            radGridView1.Columns.ToList().ForEach(p =>
            {
                if (colmap.Keys.Contains(p.Name))
                    p.HeaderText = colmap[p.Name];
                else
                    p.IsVisible = false;
            });
            int index = 0;
            colmap.Keys.ToList().ForEach(p =>
            {
                radGridView1.Columns.Move(radGridView1.Columns[p].Index, index++);
            });
        }

        [Obsolete("Using InitGridColumns Instead.")]
        public static void InitGridCol(Dictionary<string, string> colmap, RadGridView radGridView1)
        {
            for (int i = 0; i < radGridView1.Columns.Count; i++)
            {
                string colName = radGridView1.Columns[i].Name;
                if (!colmap.Keys.Contains(radGridView1.Columns[i].Name))
                    radGridView1.Columns[i].IsVisible = false;// 隐藏不显示的列。
                else
                {
                    radGridView1.Columns[i].HeaderText = colmap[radGridView1.Columns[i].Name];//设置列头。
                }
            }
            int index = 0;
            foreach (string colname in colmap.Keys)
            {
                radGridView1.Columns.Move(radGridView1.Columns[colname].Index, index++);//排序。
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
}
