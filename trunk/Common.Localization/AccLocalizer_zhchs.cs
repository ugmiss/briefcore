using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class AccLocalizer_zhchs : DevExpress.Accessibility.EditResAccLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.Accessibility.AccStringId id)
		{
			switch (id)
			{
				case DevExpress.Accessibility.AccStringId.ActionPress:
					return "压力";
				case DevExpress.Accessibility.AccStringId.NameScroll:
					return "滚动条";
				case DevExpress.Accessibility.AccStringId.NameScrollIndicator:
					return "位置";
				case DevExpress.Accessibility.AccStringId.NameScrollLineUp:
					return "上一行";
				case DevExpress.Accessibility.AccStringId.NameScrollLineDown:
					return "向下一行";
				case DevExpress.Accessibility.AccStringId.NameScrollColumnLeft:
					return "向左一列";
				case DevExpress.Accessibility.AccStringId.NameScrollColumnRight:
					return "向右一列";
				case DevExpress.Accessibility.AccStringId.NameScrollAreaUp:
					return "上一页";
				case DevExpress.Accessibility.AccStringId.NameScrollAreaDown:
					return "下一页";
				case DevExpress.Accessibility.AccStringId.NameScrollAreaLeft:
					return "向左一页";
				case DevExpress.Accessibility.AccStringId.NameScrollAreaRight:
					return "向右一页";
				case DevExpress.Accessibility.AccStringId.DescScrollLineUp:
					return "竖直位置上移一行";
				case DevExpress.Accessibility.AccStringId.DescScrollLineDown:
					return "竖直位置下移一行";
				case DevExpress.Accessibility.AccStringId.DescScrollAreaUp:
					return "竖直位置上移两行";
				case DevExpress.Accessibility.AccStringId.DescScrollAreaDown:
					return "竖直位置下移两行";
				case DevExpress.Accessibility.AccStringId.DescScrollVertIndicator:
					return "显示当前竖直位置，可以通过拖拽直接改变位置。";
				case DevExpress.Accessibility.AccStringId.DescScrollColumnLeft:
					return "水平位置左移一列";
				case DevExpress.Accessibility.AccStringId.DescScrollColumnRight:
					return "水平位置右移一列";
				case DevExpress.Accessibility.AccStringId.DescScrollAreaLeft:
					return "水平位置左移两列";
				case DevExpress.Accessibility.AccStringId.DescScrollAreaRight:
					return "水平位置右移两列";
				case DevExpress.Accessibility.AccStringId.DescScrollHorzIndicator:
					return "显示当前水平位置，可以通过拖拽直接改变位置。";
				case DevExpress.Accessibility.AccStringId.ButtonPush:
					return "压力";
				case DevExpress.Accessibility.AccStringId.ButtonOpen:
					return "打开";
				case DevExpress.Accessibility.AccStringId.ButtonClose:
					return "关闭";
				case DevExpress.Accessibility.AccStringId.MouseDoubleClick:
					return "双击";
				case DevExpress.Accessibility.AccStringId.OpenKeyboardShortcut:
					return "Alt+Down";
				case DevExpress.Accessibility.AccStringId.CheckEditCheck:
					return "检查";
				case DevExpress.Accessibility.AccStringId.CheckEditUncheck:
					return "未检查";
				case DevExpress.Accessibility.AccStringId.TabSwitch:
					return "切换";
				case DevExpress.Accessibility.AccStringId.SpinBox:
					return "旋转";
				case DevExpress.Accessibility.AccStringId.SpinUpButton:
					return "向上";
				case DevExpress.Accessibility.AccStringId.SpinDownButton:
					return "向下";
				case DevExpress.Accessibility.AccStringId.SpinLeftButton:
					return "左";
				case DevExpress.Accessibility.AccStringId.SpinRightButton:
					return "右";
				case DevExpress.Accessibility.AccStringId.GridNewItemRow:
					return "新项目行";
				case DevExpress.Accessibility.AccStringId.GridFilterRow:
					return "过滤行";
				case DevExpress.Accessibility.AccStringId.GridHeaderPanel:
					return "表头面板";
				case DevExpress.Accessibility.AccStringId.GridDataPanel:
					return "数据面板";
				case DevExpress.Accessibility.AccStringId.GridCell:
					return "单元格";
				case DevExpress.Accessibility.AccStringId.GridRow:
					return "行 {0}";
				case DevExpress.Accessibility.AccStringId.GridRowExpand:
					return "扩展";
				case DevExpress.Accessibility.AccStringId.GridRowCollapse:
					return "折叠";
				case DevExpress.Accessibility.AccStringId.GridCardExpand:
					return "扩展";
				case DevExpress.Accessibility.AccStringId.GridCardCollapse:
					return "折叠";
				case DevExpress.Accessibility.AccStringId.GridRowActivate:
					return "激活";
				case DevExpress.Accessibility.AccStringId.GridCellEdit:
					return "编辑";
				case DevExpress.Accessibility.AccStringId.GridCellFocus:
					return "焦点";
				case DevExpress.Accessibility.AccStringId.GridDataRowExpand:
					return "扩展细节";
				case DevExpress.Accessibility.AccStringId.GridDataRowCollapse:
					return "折叠细节";
				case DevExpress.Accessibility.AccStringId.GridColumnSortAscending:
					return "升序排列";
				case DevExpress.Accessibility.AccStringId.GridColumnSortDescending:
					return "降序排列";
				case DevExpress.Accessibility.AccStringId.GridColumnSortNone:
					return "删除排序";
				case DevExpress.Accessibility.AccStringId.BarLinkCaption:
					return "项目";
				case DevExpress.Accessibility.AccStringId.BarLinkClick:
					return "压力";
				case DevExpress.Accessibility.AccStringId.BarLinkMenuOpen:
					return "打开";
				case DevExpress.Accessibility.AccStringId.BarLinkMenuClose:
					return "关闭";
				case DevExpress.Accessibility.AccStringId.BarLinkStatic:
					return "静态";
				case DevExpress.Accessibility.AccStringId.BarLinkEdit:
					return "编辑";
				case DevExpress.Accessibility.AccStringId.BarDockControlTop:
					return "靠顶部";
				case DevExpress.Accessibility.AccStringId.BarDockControlLeft:
					return "靠左侧";
				case DevExpress.Accessibility.AccStringId.BarDockControlBottom:
					return "靠底部";
				case DevExpress.Accessibility.AccStringId.BarDockControlRight:
					return "靠右侧";
				case DevExpress.Accessibility.AccStringId.NavBarGroupExpand:
					return "扩展";
				case DevExpress.Accessibility.AccStringId.NavBarGroupCollapse:
					return "折叠";
				case DevExpress.Accessibility.AccStringId.NavBarItemClick:
					return "压力";
				case DevExpress.Accessibility.AccStringId.NavBarScrollUp:
					return "向上滚动";
				case DevExpress.Accessibility.AccStringId.NavBarScrollDown:
					return "向下滚动";
				case DevExpress.Accessibility.AccStringId.TreeListNodeExpand:
					return "展开";
				case DevExpress.Accessibility.AccStringId.TreeListNodeCollapse:
					return "折叠";
				case DevExpress.Accessibility.AccStringId.TreeListNode:
					return "节点";
				case DevExpress.Accessibility.AccStringId.TreeListNodeCell:
					return "单元格";
				case DevExpress.Accessibility.AccStringId.TreeListColumnSortAscending:
					return "升序排列";
				case DevExpress.Accessibility.AccStringId.TreeListColumnSortDescending:
					return "降序排列";
				case DevExpress.Accessibility.AccStringId.TreeListColumnSortNone:
					return "删除排序";
				case DevExpress.Accessibility.AccStringId.TreeListHeaderPanel:
					return "标题面板";
				case DevExpress.Accessibility.AccStringId.TreeListDataPanel:
					return "数据面板";
				case DevExpress.Accessibility.AccStringId.TreeListCellEdit:
					return "编辑";
				case DevExpress.Accessibility.AccStringId.TreelistRowActivate:
					return "活动的";
				case DevExpress.Accessibility.AccStringId.ScrollableControlDescription:
					return "滚动";
				case DevExpress.Accessibility.AccStringId.ScrollableControlDefaultAction:
					return "默认动作";
			}
			return base.GetLocalizedString(id);
		}
	}
}
