using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class LayoutLocalizer_zhchs : DevExpress.XtraLayout.Localization.LayoutResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraLayout.Localization.LayoutStringId id)
		{
			switch (id)
			{
				case DevExpress.XtraLayout.Localization.LayoutStringId.CustomizationParentName:
					return "定制";
				case DevExpress.XtraLayout.Localization.LayoutStringId.DefaultItemText:
					return "项目";
				case DevExpress.XtraLayout.Localization.LayoutStringId.DefaultActionText:
					return "默认动作";
				case DevExpress.XtraLayout.Localization.LayoutStringId.DefaultEmptyText:
					return "无";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LayoutItemDescription:
					return "版面设计控制器的项目元素";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LayoutGroupDescription:
					return "版面设计控制器的群组元素";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TabbedGroupDescription:
					return "版面控制器的群组标签页元素";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LayoutControlDescription:
					return "版面控制";
				case DevExpress.XtraLayout.Localization.LayoutStringId.CustomizationFormTitle:
					return "定制";
				case DevExpress.XtraLayout.Localization.LayoutStringId.HiddenItemsPageTitle:
					return "隐藏项目";
				case DevExpress.XtraLayout.Localization.LayoutStringId.HiddenItemsNodeText:
					return "隐藏项目";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TreeViewPageTitle:
					return "版面设计树状视图";
				case DevExpress.XtraLayout.Localization.LayoutStringId.RenameSelected:
					return "重命名";
				case DevExpress.XtraLayout.Localization.LayoutStringId.HideItemMenutext:
					return "隐藏项目";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LockItemSizeMenuText:
					return "锁定项目大小";
				case DevExpress.XtraLayout.Localization.LayoutStringId.UnLockItemSizeMenuText:
					return "解除项目大小锁定";
				case DevExpress.XtraLayout.Localization.LayoutStringId.GroupItemsMenuText:
					return "群组";
				case DevExpress.XtraLayout.Localization.LayoutStringId.UnGroupItemsMenuText:
					return "解除群组设定";
				case DevExpress.XtraLayout.Localization.LayoutStringId.CreateTabbedGroupMenuText:
					return "创建群组标签页";
				case DevExpress.XtraLayout.Localization.LayoutStringId.AddTabMenuText:
					return "增加标签页";
				case DevExpress.XtraLayout.Localization.LayoutStringId.UnGroupTabbedGroupMenuText:
					return "解除群组标签页设定";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TreeViewRootNodeName:
					return "最上层";
				case DevExpress.XtraLayout.Localization.LayoutStringId.ShowCustomizationFormMenuText:
					return "定制版面";
				case DevExpress.XtraLayout.Localization.LayoutStringId.HideCustomizationFormMenuText:
					return "隐藏定制表格";
				case DevExpress.XtraLayout.Localization.LayoutStringId.EmptySpaceItemDefaultText:
					return "空白区域项目";
				case DevExpress.XtraLayout.Localization.LayoutStringId.SplitterItemDefaultText:
					return "分隔器版面設計控制器的群組標籤頁項目";
				case DevExpress.XtraLayout.Localization.LayoutStringId.SimpleLabelItemDefaultText:
					return "标签";
				case DevExpress.XtraLayout.Localization.LayoutStringId.SimpleSeparatorItemDefaultText:
					return "分隔符";
				case DevExpress.XtraLayout.Localization.LayoutStringId.ControlGroupDefaultText:
					return "群组";
				case DevExpress.XtraLayout.Localization.LayoutStringId.EmptyRootGroupText:
					return "在这里放置控件";
				case DevExpress.XtraLayout.Localization.LayoutStringId.EmptyTabbedGroupText:
					return "将群组拖放到群组标签页区域";
				case DevExpress.XtraLayout.Localization.LayoutStringId.ResetLayoutMenuText:
					return "重设版面";
				case DevExpress.XtraLayout.Localization.LayoutStringId.RenameMenuText:
					return "重命名";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TextPositionMenuText:
					return "文本位置";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TextPositionLeftMenuText:
					return "左边";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TextPositionRightMenuText:
					return "右边";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TextPositionTopMenuText:
					return "上方";
				case DevExpress.XtraLayout.Localization.LayoutStringId.TextPositionBottomMenuText:
					return "下方";
				case DevExpress.XtraLayout.Localization.LayoutStringId.ShowTextMenuItem:
					return "显示文本";
				case DevExpress.XtraLayout.Localization.LayoutStringId.HideTextMenuItem:
					return "隐藏文本";
				case DevExpress.XtraLayout.Localization.LayoutStringId.ShowSpaceMenuItem:
					return "显示占位符";
				case DevExpress.XtraLayout.Localization.LayoutStringId.HideSpaceMenuItem:
					return "隐藏占位符";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LockSizeMenuItem:
					return "锁定大小";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LockWidthMenuItem:
					return "锁定宽度";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LockHeightMenuItem:
					return "锁定高度";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LockMenuGroup:
					return "强制限定大小";
				case DevExpress.XtraLayout.Localization.LayoutStringId.ResetConstraintsToDefaultsMenuItem:
					return "重设为默认值";
				case DevExpress.XtraLayout.Localization.LayoutStringId.FreeSizingMenuItem:
					return "允许改变大小";
				case DevExpress.XtraLayout.Localization.LayoutStringId.CreateEmptySpaceItem:
					return "创建空白区域项目";
				case DevExpress.XtraLayout.Localization.LayoutStringId.UndoHintCaption:
					return "撤消(Ctrl+Z)";
				case DevExpress.XtraLayout.Localization.LayoutStringId.RedoHintCaption:
					return "重复(Ctrl+Y)";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LoadHintCaption:
					return "加载布局(Ctrl+O)";
				case DevExpress.XtraLayout.Localization.LayoutStringId.SaveHintCaption:
					return "保存布局(Ctrl+S)";
				case DevExpress.XtraLayout.Localization.LayoutStringId.UndoButtonHintText:
					return "撤消到最后动作";
				case DevExpress.XtraLayout.Localization.LayoutStringId.RedoButtonHintText:
					return "重复到最后动作";
				case DevExpress.XtraLayout.Localization.LayoutStringId.SaveButtonHintText:
					return "保存布局为XML文件";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LoadButtonHintText:
					return "从XML文件加载布局";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LayoutResetConfirmationText:
					return "你将要重置布局定制。是否继续？";
				case DevExpress.XtraLayout.Localization.LayoutStringId.LayoutResetConfirmationDialogCaption:
					return "布局重置确认";
			}
			return base.GetLocalizedString(id);
		}
	}
}
