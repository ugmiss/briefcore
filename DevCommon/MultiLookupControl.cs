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
namespace DevCommon
{
    /// <summary>
    /// 多列模糊绑定弹出列表控件。
    /// </summary>
    public partial class MultiLookupControl : XtraUserControl
    {
        /* 控件使用代码样例
        * List<person> li = new List<person>();
        * Parallel.For(1, 10, p =>
        * {
        *     li.Add(new person() { Id = Guid.NewGuid().ToString(), Name = NameFactory.GetBoyName(), PersonNumber = 1001 + li.Count, Sex = RandomFactory.Next(2) == 1 ? "男" : "女" });
        * });
        * Dictionary<string, string> dic = new Dictionary<string, string>();
        * dic.Add("Id", "ID");
        * dic.Add("PersonNumber", "编号");
        * dic.Add("Name", "姓名");
        * dic.Add("Sex", "性别");
        * multiLookupControl1.Init(li, "Id", "Name", dic);
        */
        /// <summary>
        /// 数据源。
        /// </summary>
        object dataSource = null;
        /// <summary>
        /// 绑定列及列名。
        /// </summary>
        Dictionary<string, string> bindCols = null;
        /// <summary>
        /// 值成员。
        /// </summary>
        string valueMember = null;
        /// <summary>
        /// 显示成员。
        /// </summary>
        string displayMember = null;
        /// <summary>
        /// 构造方法。
        /// </summary>
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
        /// <summary>
        /// 编辑值变化时。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridLookup_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(delegate { FilterLookup(sender); }));
        }
        /// <summary>
        /// 弹出窗口时。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridLookup_Popup(object sender, EventArgs e)
        {
            FilterLookup(sender);
        }
        /// <summary>
        /// 初始化控件数据源及设置。
        /// </summary>
        /// <param name="gridLookUpEdit"></param>
        /// <param name="dataSource"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <param name="bindCols"></param>
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
        /// <summary>
        /// 过滤。
        /// </summary>
        /// <param name="sender"></param>
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