using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Reflection;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;
namespace Test
{
    public partial class MultiLookupControl : XtraUserControl
    {
        
        object dataSource = null;
        Dictionary<string, string> bindCols = null;
        string valueMember = null;
        string displayMember = null;
        public MultiLookupControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化方法。
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="valueMember">值</param>
        /// <param name="displayMember">显示</param>
        /// <param name="bindCols">绑定列和列名</param>
        public void Init(object dataSource, string valueMember, string displayMember, Dictionary<string, string> bindCols)
        {
            this.dataSource = dataSource;
            this.bindCols = bindCols;
            this.valueMember = valueMember;
            this.displayMember = displayMember;
            GridLookUpEditInit(gridLookup, this.dataSource, this.valueMember, this.displayMember, bindCols);
            gridLookup.Popup += new EventHandler(gridLookup_Popup);
            gridLookup.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(gridLookup_EditValueChanging);
        }
        void gridLookup_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(delegate { FilterLookup(sender); }));
        }
        void gridLookup_Popup(object sender, EventArgs e)
        {
            FilterLookup(sender);
        }
        void GridLookUpEditInit(GridLookUpEdit gridLookUpEdit, object dataSource, string valueMember, string displayMember, Dictionary<string, string> bindCols)
        {
            gridLookUpEdit.Properties.View.OptionsBehavior.AutoPopulateColumns = false;
            gridLookUpEdit.Properties.DataSource = dataSource;
            gridLookUpEdit.Properties.ValueMember = valueMember;
            gridLookUpEdit.Properties.DisplayMember = displayMember;
            gridLookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            gridLookUpEdit.Properties.View.BestFitColumns();
            gridLookUpEdit.Properties.ShowFooter = false;
            gridLookUpEdit.Properties.ImmediatePopup = true;
            gridLookUpEdit.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            gridLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

            GridView gridView = gridLookUpEdit.Properties.View as GridView;
            foreach (string s in this.bindCols.Keys)
            {
                GridColumn col = new GridColumn();
                col.FieldName = s;
                col.Caption = bindCols[s];
                col.Visible = true;
                gridView.Columns.Add(col);
            }
        }
        void FilterLookup(object sender)
        {
            try
            {
                GridLookUpEdit edit = sender as GridLookUpEdit;
                GridView gridView = edit.Properties.View as GridView;
                FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);
                List<CriteriaOperator> li = new List<CriteriaOperator>();
                foreach (string s in this.bindCols.Keys)
                {
                    CriteriaOperator op = new BinaryOperator(s, "%" + edit.AutoSearchText + "%", BinaryOperatorType.Like);
                    li.Add(op);
                }
                string filterCondition = new GroupOperator(GroupOperatorType.Or, li.ToArray()).ToString();
                fi.SetValue(gridView, filterCondition);
                MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
                mi.Invoke(gridView, null);
            }
            catch { }
        }
    }
}