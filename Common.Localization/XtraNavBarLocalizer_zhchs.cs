using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class XtraNavBarLocalizer_zhchs : DevExpress.XtraNavBar.NavBarResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraNavBar.NavBarStringId id)
		{
			switch (id)
			{
				case DevExpress.XtraNavBar.NavBarStringId.NavPaneMenuShowMoreButtons:
					return "显示更多按钮（&M）";
				case DevExpress.XtraNavBar.NavBarStringId.NavPaneMenuShowFewerButtons:
					return "显示少量按钮（&F）";
				case DevExpress.XtraNavBar.NavBarStringId.NavPaneMenuAddRemoveButtons:
					return "添加或删除按钮（&A）";
				case DevExpress.XtraNavBar.NavBarStringId.NavPaneChevronHint:
					return "配置按钮";
			}
			return base.GetLocalizedString(id);
		}
	}
}
