using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
    public class XtraTreeListLocalizer_zhchs : DevExpress.XtraTreeList.Localization.TreeListResLocalizer
    {
        public override string Language
        {
            get
            {
                return "简体中文";
            }
        }
        public override string GetLocalizedString(DevExpress.XtraTreeList.Localization.TreeListStringId id)
        {
            switch (id)
            {
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterSum:
                    return "和";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMin:
                    return "最小值";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMax:
                    return "最大值";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterCount:
                    return "计数";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterAverage:
                    return "平均值";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterNone:
                    return "无";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterAllNodes:
                    return "所有节点";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterSumFormat:
                    return "和={0:#.##}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMinFormat:
                    return "最小值={0}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMaxFormat:
                    return "最大值={0}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterCountFormat:
                    return "{0}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterAverageFormat:
                    return "平均值={0:#.##}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnSortAscending:
                    return "升序排列";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnSortDescending:
                    return "降序排列";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnColumnCustomization:
                    return "列选择";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnBestFit:
                    return "最佳匹配";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnBestFitAllColumns:
                    return "最佳匹配 (所有列)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.ColumnCustomizationText:
                    return "自定义";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.ColumnNamePrefix:
                    return "列";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PrintDesignerHeader:
                    return "打印设置";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PrintDesignerDescription:
                    return "为当前的树状列表设置不同的打印选项";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.InvalidNodeExceptionText:
                    return " 要修正当前值吗?";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MultiSelectMethodNotSupported:
                    return "OptionsBehavior.MultiSelect未激活时，指定方法不能工作.";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.CustomizationFormColumnHint:
                    return "拖放列到这自定布局";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterPanelCustomizeButton:
                    return "编辑过滤器";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.WindowErrorCaption:
                    return "错误";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorOkButton:
                    return "确定(&)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorCancelButton:
                    return "取消(&)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorApplyButton:
                    return "应用(&)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorCaption:
                    return "过滤器编辑器";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnAutoFilterRowHide:
                    return "隐藏自动过滤行";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnAutoFilterRowShow:
                    return "显示自动过滤行";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnFilterEditor:
                    return "过滤器编辑器...";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnClearFilter:
                    return "清空过滤器";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PopupFilterAll:
                    return "(所有)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PopupFilterBlanks:
                    return "(空白)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PopupFilterNonBlanks:
                    return "(非空白)";
            }
            return base.GetLocalizedString(id);
        }
    }
}
