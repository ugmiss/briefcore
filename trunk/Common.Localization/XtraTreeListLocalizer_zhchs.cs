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
                return "��������";
            }
        }
        public override string GetLocalizedString(DevExpress.XtraTreeList.Localization.TreeListStringId id)
        {
            switch (id)
            {
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterSum:
                    return "��";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMin:
                    return "��Сֵ";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMax:
                    return "���ֵ";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterCount:
                    return "����";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterAverage:
                    return "ƽ��ֵ";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterNone:
                    return "��";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterAllNodes:
                    return "���нڵ�";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterSumFormat:
                    return "��={0:#.##}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMinFormat:
                    return "��Сֵ={0}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterMaxFormat:
                    return "���ֵ={0}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterCountFormat:
                    return "{0}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuFooterAverageFormat:
                    return "ƽ��ֵ={0:#.##}";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnSortAscending:
                    return "��������";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnSortDescending:
                    return "��������";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnColumnCustomization:
                    return "��ѡ��";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnBestFit:
                    return "���ƥ��";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnBestFitAllColumns:
                    return "���ƥ�� (������)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.ColumnCustomizationText:
                    return "�Զ���";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.ColumnNamePrefix:
                    return "��";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PrintDesignerHeader:
                    return "��ӡ����";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PrintDesignerDescription:
                    return "Ϊ��ǰ����״�б����ò�ͬ�Ĵ�ӡѡ��";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.InvalidNodeExceptionText:
                    return " Ҫ������ǰֵ��?";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MultiSelectMethodNotSupported:
                    return "OptionsBehavior.MultiSelectδ����ʱ��ָ���������ܹ���.";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.CustomizationFormColumnHint:
                    return "�Ϸ��е����Զ�����";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterPanelCustomizeButton:
                    return "�༭������";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.WindowErrorCaption:
                    return "����";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorOkButton:
                    return "ȷ��(&)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorCancelButton:
                    return "ȡ��(&)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorApplyButton:
                    return "Ӧ��(&)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.FilterEditorCaption:
                    return "�������༭��";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnAutoFilterRowHide:
                    return "�����Զ�������";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnAutoFilterRowShow:
                    return "��ʾ�Զ�������";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnFilterEditor:
                    return "�������༭��...";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.MenuColumnClearFilter:
                    return "��չ�����";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PopupFilterAll:
                    return "(����)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PopupFilterBlanks:
                    return "(�հ�)";
                case DevExpress.XtraTreeList.Localization.TreeListStringId.PopupFilterNonBlanks:
                    return "(�ǿհ�)";
            }
            return base.GetLocalizedString(id);
        }
    }
}
