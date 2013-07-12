using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class XtraWizardLocalizer_zhchs : DevExpress.XtraWizard.Localization.WizardResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraWizard.Localization.WizardStringId id)
		{
			switch (id)
			{
				case DevExpress.XtraWizard.Localization.WizardStringId.CancelText:
					return "取消";
				case DevExpress.XtraWizard.Localization.WizardStringId.FinishText:
					return "结束";
				case DevExpress.XtraWizard.Localization.WizardStringId.HelpText:
					return "帮助";
				case DevExpress.XtraWizard.Localization.WizardStringId.NextText:
					return "下一步 >";
				case DevExpress.XtraWizard.Localization.WizardStringId.PreviousText:
					return "< 上一步";
				case DevExpress.XtraWizard.Localization.WizardStringId.WelcomePageProceedText:
					return "单击下一步继续";
				case DevExpress.XtraWizard.Localization.WizardStringId.CompletionPageProceedText:
					return "单击结束关闭向导";
				case DevExpress.XtraWizard.Localization.WizardStringId.WelcomePageTitleText:
					return "欢迎使用向导";
				case DevExpress.XtraWizard.Localization.WizardStringId.CompletionPageTitleText:
					return "完成向导";
				case DevExpress.XtraWizard.Localization.WizardStringId.WelcomePageIntroductionText:
					return "该向导简单地指导用户通过一系列步骤来执行一个复杂的任务设置";
				case DevExpress.XtraWizard.Localization.WizardStringId.CompletionPageFinishText:
					return "您已经顺利完成向导";
				case DevExpress.XtraWizard.Localization.WizardStringId.InteriorPageTitleText:
					return "向导页标题";
				case DevExpress.XtraWizard.Localization.WizardStringId.PageDescriptionText:
					return "向导页说明:帮助拥护完成子任务";
				case DevExpress.XtraWizard.Localization.WizardStringId.WizardTitle:
					return "向导标题";
				case DevExpress.XtraWizard.Localization.WizardStringId.CaptionError:
					return "错误";
			}
			return base.GetLocalizedString(id);
		}
	}
}
