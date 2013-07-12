using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class XtraBarsLocalizer_zhchs : DevExpress.XtraBars.Localization.BarResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraBars.Localization.BarString id)
		{
			switch (id)
			{
				case DevExpress.XtraBars.Localization.BarString.None:
					return "";
				case DevExpress.XtraBars.Localization.BarString.PopupMenuEditor:
					return "弹出菜单编辑器";
				case DevExpress.XtraBars.Localization.BarString.AddOrRemove:
					return "添加或删除按钮(&A)";
				case DevExpress.XtraBars.Localization.BarString.ResetBar:
					return "确定要对 '{0}' 工具栏所做的改动进行重置吗？";
				case DevExpress.XtraBars.Localization.BarString.ResetBarCaption:
					return "自定义";
				case DevExpress.XtraBars.Localization.BarString.ResetButton:
					return "重设工具栏(&R)";
				case DevExpress.XtraBars.Localization.BarString.CustomizeButton:
					return "自定义...(&C)";
				case DevExpress.XtraBars.Localization.BarString.CancelButton:
					return "取消";
				case DevExpress.XtraBars.Localization.BarString.ToolBarMenu:
					return "重新设定(&R)$刪除(&D)$!重新命名(&N)$!默认格式(&L)$全文字模式(&T)$文字菜单(&O)$图片及文字(&A)$!启用组(&G)$可见的(&V)$最近使用的(&M)";
				case DevExpress.XtraBars.Localization.BarString.ToolbarNameCaption:
					return "工具栏名称(&T)";
				case DevExpress.XtraBars.Localization.BarString.NewToolbarCaption:
					return "新建工具栏";
				case DevExpress.XtraBars.Localization.BarString.NewToolbarCustomNameFormat:
					return "自定义{0}";
				case DevExpress.XtraBars.Localization.BarString.RenameToolbarCaption:
					return "重命名工具栏";
				case DevExpress.XtraBars.Localization.BarString.CustomizeWindowCaption:
					return "自定义";
				case DevExpress.XtraBars.Localization.BarString.MenuAnimationSystem:
					return "(系统默认值)";
				case DevExpress.XtraBars.Localization.BarString.MenuAnimationNone:
					return "无";
				case DevExpress.XtraBars.Localization.BarString.MenuAnimationSlide:
					return "片";
				case DevExpress.XtraBars.Localization.BarString.MenuAnimationFade:
					return "减弱";
				case DevExpress.XtraBars.Localization.BarString.MenuAnimationUnfold:
					return "展开";
				case DevExpress.XtraBars.Localization.BarString.MenuAnimationRandom:
					return "随机";
				case DevExpress.XtraBars.Localization.BarString.RibbonToolbarAbove:
					return "将快速访问工具栏显示在功能区上方(&S)";
				case DevExpress.XtraBars.Localization.BarString.RibbonToolbarBelow:
					return "将快速访问工具栏显示在功能区下方(&S)";
				case DevExpress.XtraBars.Localization.BarString.RibbonToolbarAdd:
					return "添加快速访问工具栏(&A)";
				case DevExpress.XtraBars.Localization.BarString.RibbonToolbarMinimizeRibbon:
					return "最小化功能区(&N)";
				case DevExpress.XtraBars.Localization.BarString.RibbonToolbarRemove:
					return "移除快速访问工具栏(&R)";
				case DevExpress.XtraBars.Localization.BarString.RibbonGalleryFilter:
					return "所有组";
				case DevExpress.XtraBars.Localization.BarString.RibbonGalleryFilterNone:
					return "无";
				case DevExpress.XtraBars.Localization.BarString.BarUnassignedItems:
					return "(未设定项)";
				case DevExpress.XtraBars.Localization.BarString.BarAllItems:
					return "(所有项)";
				case DevExpress.XtraBars.Localization.BarString.RibbonUnassignedPages:
					return "(未设定页)";
				case DevExpress.XtraBars.Localization.BarString.RibbonAllPages:
					return "(所有页)";
				case DevExpress.XtraBars.Localization.BarString.NewToolbarName:
					return "工具";
				case DevExpress.XtraBars.Localization.BarString.NewMenuName:
					return "主菜单";
				case DevExpress.XtraBars.Localization.BarString.NewStatusBarName:
					return "状态栏";
				case DevExpress.XtraBars.Localization.BarString.CloseButton:
					return "关闭";
				case DevExpress.XtraBars.Localization.BarString.MinimizeButton:
					return "最小化";
				case DevExpress.XtraBars.Localization.BarString.MaximizeButton:
					return "最大化";
				case DevExpress.XtraBars.Localization.BarString.RestoreButton:
					return "向下还原";
				case DevExpress.XtraBars.Localization.BarString.HelpButton:
					return "帮助";
				case DevExpress.XtraBars.Localization.BarString.SkinsMain:
					return "标准皮肤";
				case DevExpress.XtraBars.Localization.BarString.SkinsOffice:
					return "Office皮肤";
				case DevExpress.XtraBars.Localization.BarString.SkinsTheme:
					return "主题皮肤";
				case DevExpress.XtraBars.Localization.BarString.SkinsBonus:
					return "附加皮肤";
				case DevExpress.XtraBars.Localization.BarString.SkinsCustom:
					return "自定义皮肤";
				case DevExpress.XtraBars.Localization.BarString.SkinCaptions:
                    return "|默认风格|褐色|双子星座|棕黑色|幻想|莉莲|黑色|蓝色|Office 2010蓝色|Office 2010黑色|Office 2010银色|Office 2007蓝色|Office 2007黑色|Office 2007银色|Office 2007绿色|Office 2007粉红色|Windows 7|Windows 7 经典|深黑色|Mac皮肤|尖锐|尖锐附加|雾蒙蒙|暗边|圣诞节(蓝色)|春季|夏季|南瓜|情人节|魔幻|咖啡色|海洋水族馆|高对比度|明亮的天空|伦敦明亮的天空|沥青色|蓝图|白色图|VS2010|都市|";
				case DevExpress.XtraBars.Localization.BarString.ShowScreenTipsOnToolbarsName:
					return "在工具栏上显示屏幕提示";
				case DevExpress.XtraBars.Localization.BarString.ShowShortcutKeysOnScreenTipsName:
                    return "在屏幕提示中显示快捷键";
				case DevExpress.XtraBars.Localization.BarString.ExpandRibbonSuperTipHeader:
					return "展开功能区(Ctrl+F1)";
				case DevExpress.XtraBars.Localization.BarString.CollapseRibbonSuperTipHeader:
					return "功能区最小化(Ctrl+F1)";
				case DevExpress.XtraBars.Localization.BarString.ExpandRibbonSuperTipText:
					return "显示功能区，以便它始终展开，甚至在单击命令后也展开。";
				case DevExpress.XtraBars.Localization.BarString.CollapseRibbonSuperTipText:
					return "仅显示功能区上的选项卡名称。";
				case DevExpress.XtraBars.Localization.BarString.MoreCommands:
					return "其他命令(&M)...";
				case DevExpress.XtraBars.Localization.BarString.CustomizeRibbon:
					return "自定义功能区(&R)...";
				case DevExpress.XtraBars.Localization.BarString.CustomizeQuickAccessToolbar:
					return "自定义快速访问工具栏(&C)...";
				case DevExpress.XtraBars.Localization.BarString.CustomizeToolbarText:
                    return "自定义工具栏";
				case DevExpress.XtraBars.Localization.BarString.CustomizeToolbarSuperTipText:
                    return "自定义快速访问工具栏";
			}
			return base.GetLocalizedString(id);
		}
	}
}
