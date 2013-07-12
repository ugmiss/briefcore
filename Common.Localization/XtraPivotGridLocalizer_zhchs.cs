﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class XtraPivotGridLocalizer_zhchs : DevExpress.XtraPivotGrid.Localization.PivotGridResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraPivotGrid.Localization.PivotGridStringId id)
		{
			switch (id)
			{
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.RowHeadersCustomization:
					return "将行域放在这里";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.ColumnHeadersCustomization:
					return "将列域放在这里";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterHeadersCustomization:
					return "将数据筛选条件域放在这里";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.DataHeadersCustomization:
					return "将数据项目放在这里";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.RowArea:
					return "行区域";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.ColumnArea:
					return "列区域";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterArea:
					return "数据筛选区域";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.DataArea:
					return "数据区域";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterShowAll:
					return "(全部显示)";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterOk:
					return "确定";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterCancel:
					return "取消";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterBlank:
					return "(空白)";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterShowBlanks:
					return "显示空白";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterInvert:
					return "反转过滤";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormCaption:
					return "PivotGrid标题列表";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormText:
					return "将项目放到PivotGrid中";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormAddTo:
					return "增加到";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormHint:
					return "拖动字段到区域下";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormDeferLayoutUpdate:
					return "延迟布局更新";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormUpdate:
					return "更新";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormListBoxText:
					return "拖动字段至此自定义布局";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormHiddenFields:
					return "隐藏字段";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Total:
					return "合计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.GrandTotal:
					return "总计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormat:
					return "{0}合计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatCount:
					return "{0}数据数";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatSum:
					return "{0}小计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatMin:
					return "{0} 最小值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatMax:
					return "{0} 最大值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatAverage:
					return "{0}平均";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatStdDev:
					return "{0}标准差";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatStdDevp:
					return "{0}总体标准差";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatVar:
					return "{0}样本方差";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatVarp:
					return "{0}总体方差";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TotalFormatCustom:
					return "{0}统计值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesigner:
					return "打印设计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerPageOptions:
					return "选项";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerPageBehavior:
					return "执行";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerCategoryDefault:
					return "默认值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerCategoryLines:
					return "格线";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerCategoryHeaders:
					return "标题";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerCategoryFieldValues:
					return "字段值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerHorizontalLines:
					return "水平格线";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerVerticalLines:
					return "垂直格线";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerFilterHeaders:
					return "数据筛选条件标题";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerDataHeaders:
					return "数据标题";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerColumnHeaders:
					return "列标题";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerRowHeaders:
					return "行标题";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerHeadersOnEveryPage:
					return "标题在每一个页面";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerUnusedFilterFields:
					return "未使用过滤字段";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerMergeColumnFieldValues:
					return "合并列字段值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerMergeRowFieldValues:
					return "合并行字段值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrintDesignerUsePrintAppearance:
					return "使用打印外观";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuRefreshData:
					return "刷新数据";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuSortAscending:
					return "升序";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuSortDescending:
					return "降序";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuClearSorting:
					return "清除排序";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuShowExpression:
					return "表达式编辑器...";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuHideField:
					return "隐藏";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuShowFieldList:
					return "显示数据栏清单";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuHideFieldList:
					return "隐藏数据栏清单";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuFieldOrder:
					return "排序";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuMovetoBeginning:
					return "移到开头";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuMovetoLeft:
					return "移到左边";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuMovetoRight:
					return "移到右边";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuMovetoEnd:
					return "移到最后";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuCollapse:
					return "收合";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuExpand:
					return "展开";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuCollapseAll:
					return "全部收合";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuExpandAll:
					return "全部展开";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuShowPrefilter:
					return "显示过滤器";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuHidePrefilter:
					return "隐藏过滤器";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuSortFieldByColumn:
					return "按列\"{0}\"排序";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuSortFieldByRow:
					return "按行\"{0}\"排序";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PopupMenuRemoveAllSortByColumn:
					return "清除所有排序";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.DataFieldCaption:
					return "数据";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TopValueOthersRow:
					return "其他";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CellError:
					return "错误";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.ValueError:
					return "错误";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CannotCopyMultipleSelections:
					return "此命令不能多次选择";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrefilterInvalidProperty:
					return "(无效属性)";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrefilterInvalidCriteria:
					return "错误发生在滤器条件。请检测无效的属性标题和条件操作，并更正或删除。";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.PrefilterFormCaption:
					return "PivotGrid过滤器";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.EditPrefilter:
					return "编辑过虑器";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.OLAPMeasuresCaption:
					return "量度";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.OLAPDrillDownFilterException:
					return "当在一个报表的筛选字段中选择了多个项目时显示明细命令将无法执行。在执行之前请在报表筛选区域为每一个筛选选择一个单独项目。";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.OLAPNoOleDbProvidersMessage:
					return "为了使用的PivotGrid OLAP功能，你应该在系统上安装一个MS OLAP OLEDB提供程序。\r\n您可以在这里下载:";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TrendGoingUp:
					return "上升";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TrendGoingDown:
					return "下沉";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.TrendNoChange:
					return "不改变";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.StatusBad:
					return "坏";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.StatusNeutral:
					return "中立";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.StatusGood:
					return "好";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryCount:
					return "计数";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummarySum:
					return "总计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryMin:
					return "最小";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryMax:
					return "最大";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryAverage:
					return "平均";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryStdDev:
					return "标准差估计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryStdDevp:
					return "扩展标准差";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryVar:
					return "变异数估计";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryVarp:
					return "扩展变异数";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SummaryCustom:
					return "自定";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormStackedDefault:
					return "字段和区层叠";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormStackedSideBySide:
					return "字段和区并排";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormTopPanelOnly:
					return "只有字段";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormBottomPanelOnly2by2:
					return "只有2区中的2区";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormBottomPanelOnly1by4:
					return "只有4区中的1区";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.CustomizationFormLayoutButtonTooltip:
					return "自定布局";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterPopupToolbarShowOnlyAvailableItems:
					return "显示可用项";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterPopupToolbarShowNewValues:
                    return "显示新的字段值";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterPopupToolbarIncrementalSearch:
					return "增加搜索";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterPopupToolbarMultiSelection:
					return "多选";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterPopupToolbarRadioMode:
					return "单选";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.FilterPopupToolbarInvertFilter:
					return "反向过滤";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_Expand:
					return "[展开]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_Collapse:
					return "[折迭]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_SortedAscending:
					return "(升序)";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_SortedDescending:
					return "(降序)";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_FilterWindowSizeGrip:
					return "[调整大小]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_FilterButton:
					return "[过滤器]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_FilterButtonActive:
					return "[已过滤]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_DragHideField:
					return "隐藏";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_FilterAreaHeaders:
					return "[过滤区标题]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_ColumnAreaHeaders:
					return "[列区标题]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_RowAreaHeaders:
					return "[行区标题]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_DataAreaHeaders:
					return "[数据区标题]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_FieldListHeaders:
					return "[隐藏字段标题]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_LayoutButton:
					return "[布局按钮]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_StackedDefaultLayout:
					return "[堆叠默认布局]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_StackedSideBySideLayout:
					return "[堆叠并排布局]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_TopPanelOnlyLayout:
					return "[只上面板布局]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_BottomPanelOnly2by2Layout:
					return "[底部面板只2区中的2区布局]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.Alt_BottomPanelOnly1by4Layout:
					return "[底部面板只4区中的1区布局]";
				case DevExpress.XtraPivotGrid.Localization.PivotGridStringId.SearchBoxText:
					return "搜索";
			}
			return base.GetLocalizedString(id);
		}
	}
}
