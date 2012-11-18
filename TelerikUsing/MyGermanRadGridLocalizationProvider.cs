using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls;

namespace TelerikUsing
{

    //Program中加入以下代码
    // 本地化。
    //      RadGridLocalizationProvider.CurrentProvider = new MyChineseRadGridLocalizationProvider();
    //      RadMessageLocalizationProvider.CurrentProvider = new MyBGRadMessageLocalizationProvider();
    /// <summary>
    /// 本地化类。
    /// </summary>
    public class MyChineseRadGridLocalizationProvider : RadGridLocalizationProvider
    {
        /// <summary>
        /// 重写的本地化取值方法。
        /// </summary>
        /// <param name="id">功能键值。</param>
        /// <returns>本地化串。</returns>
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadGridStringId.AddNewRowString: return "点击添加新行";
                case RadGridStringId.NoDataText: return "没有数据";
                case RadGridStringId.GroupingPanelDefaultMessage: return "拖一列到面板进行分组";
                case RadGridStringId.GroupingPanelHeader: return "分组：";
                case RadGridStringId.GroupByThisColumnMenuItem: return "分组：";
                case RadGridStringId.CustomFilterDialogCheckBoxNot: return "是否";
                case RadGridStringId.CustomFilterDialogLabel: return "显示的列";
                case RadGridStringId.FilterFunctionContains: return "包含";
                case RadGridStringId.FilterFunctionCustom: return "自定义";
                case RadGridStringId.FilterFunctionDoesNotContain: return "不包含";
                case RadGridStringId.FilterFunctionEndsWith: return "以结尾";
                case RadGridStringId.FilterFunctionEqualTo: return "等于";
                case RadGridStringId.FilterFunctionGreaterThan: return "大于";
                case RadGridStringId.FilterFunctionGreaterThanOrEqualTo: return "大于等于";
                case RadGridStringId.FilterFunctionLessThan: return "小于";
                case RadGridStringId.FilterFunctionLessThanOrEqualTo: return "小于等于";
                case RadGridStringId.FilterFunctionNoFilter: return "不过滤";
                case RadGridStringId.FilterFunctionStartsWith: return "以开头";
                case RadGridStringId.FilterFunctionIsNull: return "为空";
                case RadGridStringId.FilterFunctionIsEmpty: return "空串";
                case RadGridStringId.FilterFunctionNotIsNull: return "不为空";
                case RadGridStringId.FilterFunctionNotIsEmpty: return "不为空串";
                case RadGridStringId.FilterFunctionNotEqualTo: return "不等于";
                case RadGridStringId.CustomFilterDialogCaption: return "自定义条件";
                case RadGridStringId.FilterFunctionNotBetween: return "不在之间";
                case RadGridStringId.FilterFunctionBetween: return "在之间";

                case RadGridStringId.FilterOperatorBetween: return "在之间";
                case RadGridStringId.FilterOperatorContains: return "包含";
                case RadGridStringId.FilterOperatorDoesNotContain: return "不包含";
                case RadGridStringId.FilterOperatorEndsWith: return "以结尾";
                case RadGridStringId.FilterOperatorEqualTo: return "等于";
                case RadGridStringId.FilterOperatorGreaterThan: return "大于";
                case RadGridStringId.FilterOperatorGreaterThanOrEqualTo: return "大于等于";
                case RadGridStringId.FilterOperatorIsEmpty: return "空串";
                case RadGridStringId.FilterOperatorIsNull: return "为空";
                case RadGridStringId.FilterOperatorLessThan: return "小于";
                case RadGridStringId.FilterOperatorLessThanOrEqualTo: return "小于等于";
                case RadGridStringId.FilterOperatorNoFilter: return "不过滤";
                case RadGridStringId.FilterOperatorNotBetween: return "不在之间";
                case RadGridStringId.FilterOperatorNotEqualTo: return "不等于";
                case RadGridStringId.FilterOperatorNotIsEmpty: return "不是空串";
                case RadGridStringId.FilterOperatorNotIsNull: return "不为空";
                case RadGridStringId.FilterOperatorStartsWith: return "以开头";
                case RadGridStringId.FilterOperatorIsLike: return "包含";
                case RadGridStringId.FilterOperatorNotIsLike: return "不包含";
                case RadGridStringId.FilterOperatorIsContainedIn: return "包含在内";
                case RadGridStringId.FilterOperatorNotIsContainedIn: return "不包含在内";
                case RadGridStringId.FilterOperatorCustom: return "自定义";

                case RadGridStringId.DeleteRowMenuItem: return "删除行";
                case RadGridStringId.SortAscendingMenuItem: return "升序";
                case RadGridStringId.SortDescendingMenuItem: return "降序";
                case RadGridStringId.ClearSortingMenuItem: return "清排序";
                case RadGridStringId.ConditionalFormattingMenuItem: return "条件格式";
                case RadGridStringId.UngroupThisColumn: return "取消分组";
                case RadGridStringId.ColumnChooserMenuItem: return "列选择";
                case RadGridStringId.HideMenuItem: return "隐藏";
                case RadGridStringId.UnpinMenuItem: return "解锁";
                case RadGridStringId.PinMenuItem: return "锁定状态";
                case RadGridStringId.PinAtLeftMenuItem: return "左锁定";
                case RadGridStringId.PinAtRightMenuItem: return "右锁定";
                case RadGridStringId.BestFitMenuItem: return "自适应";
                case RadGridStringId.PasteMenuItem: return "粘贴";
                case RadGridStringId.EditMenuItem: return "编辑";
                case RadGridStringId.ClearValueMenuItem: return "清空";
                case RadGridStringId.CopyMenuItem: return "复制";

                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
    /// <summary>
    /// 消息本地化。
    /// </summary>
    public class MyBGRadMessageLocalizationProvider : RadMessageLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadMessageStringID.AbortButton: return "终止";
                case RadMessageStringID.CancelButton: return "取消";
                case RadMessageStringID.IgnoreButton: return "忽略";
                case RadMessageStringID.NoButton: return "否";
                case RadMessageStringID.OKButton: return "确定";
                case RadMessageStringID.RetryButton: return "重试";
                case RadMessageStringID.YesButton: return "是";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }

}