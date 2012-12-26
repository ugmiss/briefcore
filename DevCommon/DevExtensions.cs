using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Data;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraGrid.Columns;

namespace System
{
    /// <summary>
    /// 菜单条扩展。
    /// </summary>
    public static class BarExtensions
    {
        /// <summary>
        /// 菜单条初始化。
        /// </summary>
        /// <param name="bars"></param>
        public static void BarInit(params Bar[] bars)
        {
            foreach (var bar in bars)
            {
                bar.OptionsBar.AllowQuickCustomization = false;// 禁用自定义下拉
                bar.OptionsBar.DrawDragBorder = false;// 禁用拖拽
            }
        }
        /// <summary>
        /// 菜单条初始。
        /// </summary>
        /// <param name="bar"></param>
        public static void BarInit(this Bar bar)
        {
            bar.OptionsBar.AllowQuickCustomization = false;// 禁用自定义下拉
            bar.OptionsBar.DrawDragBorder = false;// 禁用拖拽
        }
        /// <summary>
        /// 取窗体的BarManager。
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static BarManager GetBarManager(this XtraForm form)
        {
            BarManager myManager = null;
            foreach (var childControl in form.Controls)
            {
                if (childControl is BarDockControl)
                {
                    myManager = (childControl as BarDockControl).Manager;
                    if (myManager != null)
                        break;
                }
            }
            return myManager;
        }
    }
    /// <summary>
    /// 列表扩展。
    /// </summary>
    public static class GridControlExtensions
    {
        /// <summary>
        /// 列表控件初始化。
        /// </summary>
        /// <param name="gridControl"></param>
        public static void GridControlInit(this GridControl gridControl)
        {
            var gridView1 = gridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            //gridView1.OptionsBehavior.Editable = true;
            //gridView1.OptionsBehavior.ReadOnly = true;
        }
        /// <summary>
        /// 列表视图初始化。
        /// </summary>
        /// <param name="gridView1"></param>
        public static void GridViewInit(this DevExpress.XtraGrid.Views.Grid.GridView gridView1)
        {
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            gridView1.OptionsView.ShowGroupPanel = false;
        }
        /// <summary>
        /// 初始化列。
        /// </summary>
        /// <param name="gridView1"></param>
        /// <param name="Map"></param>
        public static void GridViewInitCols(this DevExpress.XtraGrid.Views.Grid.GridView gridView1, Dictionary<string, string> Map)
        {
            gridView1.Columns.Clear();
            gridView1.BeginInit();
            foreach (var key in Map.Keys)
            {
                GridColumn col = new GridColumn();
                col.FieldName = key;
                col.Caption = Map[key];
                col.Visible = true;
                gridView1.Columns.Add(col);
            }
            gridView1.EndInit();
        }
    }
    /// <summary>
    /// 弹出式列表扩展。
    /// </summary>
    public static class GridLookUpEditExtensions
    {
        /// <summary>
        /// 弹出式列表下拉控件初始化。
        /// </summary>
        /// <param name="gridLookUpEdit"></param>
        /// <param name="dataSource"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        public static void GridLookUpEditInit(this GridLookUpEdit gridLookUpEdit, object dataSource, string valueMember, string displayMember)
        {
            gridLookUpEdit.Properties.View.OptionsBehavior.AutoPopulateColumns = false;
            gridLookUpEdit.Properties.DataSource = dataSource;
            gridLookUpEdit.Properties.ValueMember = valueMember;
            gridLookUpEdit.Properties.DisplayMember = displayMember;
            gridLookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            gridLookUpEdit.Properties.View.BestFitColumns();
            gridLookUpEdit.Properties.ShowFooter = false;
            //gridLookUpEdit.Properties.AutoComplete = false;
            gridLookUpEdit.Properties.ImmediatePopup = true;
            gridLookUpEdit.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            gridLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }
    }
    /// <summary>
    /// 下拉控件扩展。
    /// </summary>
    public static class ComboBoxEditExtensions
    {
        /// <summary>
        /// 下拉控件初始化。
        /// </summary>
        /// <param name="cbo"></param>
        public static void ComboEditInit(this ComboBoxEdit cbo)
        {
            cbo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }
    }
    /// <summary>
    /// 树控件扩展。
    /// </summary>
    public static class TreeListExtensions
    {
        /// <summary>
        /// 树控件初始化。
        /// </summary>
        /// <param name="treeList"></param>
        public static void TreeListInit(this TreeList treeList)
        {
            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.ShowHorzLines = false;
            treeList.OptionsView.ShowIndicator = false;
            treeList.OptionsView.ShowVertLines = false;
        }
        /// <summary>
        /// 取树节点的集合。
        /// </summary>
        /// <param name="treelist"></param>
        /// <returns></returns>
        public static List<TreeListNode> GetAllNodes(this TreeList treelist)
        {
            AllNodeOperation allNodeOperation = new AllNodeOperation();
            treelist.NodesIterator.DoOperation(allNodeOperation);
            return allNodeOperation.ResultNodeList;
        }
        /// <summary>
        /// 非绑定方式加载树。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeList1"></param>
        /// <param name="list"></param>
        /// <param name="root"></param>
        public static void LoadTree<T>(this TreeList treeList1, List<T> list, T root) where T : ITreeObject
        {
            treeList1.BeginUnboundLoad();
            treeList1.ClearNodes();

            if (root != null)
            {
                //root对象需要重写头ToString()
                TreeListNode node = treeList1.AppendNode(new object[] { root }, null);
                node.Tag = root;
                LoadTreeNode(treeList1, node, list);
            }
            treeList1.EndUnboundLoad();
            treeList1.ExpandAll();
        }
        /// <summary>
        /// 非绑定方式加载树节点。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeList1"></param>
        /// <param name="node"></param>
        /// <param name="list"></param>
        public static void LoadTreeNode<T>(TreeList treeList1, TreeListNode node, List<T> list) where T : ITreeObject
        {
            T parent = (T)node.Tag;
            foreach (var s in list.ToArray())
            {
                if (s.ParentID == parent.ID)
                {
                    TreeListNode cnode = treeList1.AppendNode(new object[] { s
                    }, node);
                    cnode.Tag = s;
                    LoadTreeNode(treeList1, node, list);
                }
            }
        }
    }
    public interface ITreeObject
    {
        string ID { get; set; }
        string ParentID { get; set; }
    }
    public class AllNodeOperation : TreeListOperation
    {
        List<TreeListNode> resultNodeList = new List<TreeListNode>();
        public override void Execute(TreeListNode node)
        {
            resultNodeList.Add(node);
        }
        public List<TreeListNode> ResultNodeList { get { return resultNodeList; } }
    }
    // 过滤策略
    public class FilterNodeOperation : TreeListOperation
    {
        string fieldname;
        string condition;
        public FilterNodeOperation(string fieldname, string condition)
        {
            this.fieldname = fieldname;
            this.condition = condition;
        }
        List<TreeListNode> resultNodeList = new List<TreeListNode>();
        public override void Execute(TreeListNode node)
        {
            if (node[fieldname].ToString().Contains(condition))
                resultNodeList.Add(node);
        }
        public List<TreeListNode> ResultNodeList { get { return resultNodeList; } }
    }
}
