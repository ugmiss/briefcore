using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class XtraSchedulerExtensions_zhchs : DevExpress.XtraScheduler.Localization.SchedulerExtensionsResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId id)
		{
			switch (id)
			{
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Caption_RecurringAppointment:
					return "重复预约";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Caption_RecurringEvent:
					return "重复事件";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Caption_Appointment:
					return "预约";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Caption_Event:
					return "事件";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Caption_ViewSelector:
					return "视图选择器";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Msg_AppointmentOccurs:
					return "发生 {0}.\r\n";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Msg_AppointmentOccursInThePast:
					return "预约出现在过去";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Msg_AllOccurrencesInThePast:
					return "发生在过去的所有重复预约的实例.";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Msg_AppointmentConflictsWithAnother:
					return "与其他在您计划中的预约冲突.";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Msg_PatternOccurrencesConflictsWithOtherAppointments:
					return "重复预约的实例{0}与您计划中的其他预约冲突.\r\n";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Msg_Some:
					return "若干";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.Caption_ViewNavigator:
					return "视图导航";
				case DevExpress.XtraScheduler.Localization.SchedulerExtensionsStringId.CaptionViewNavigator_Today:
					return "今天";
			}
			return base.GetLocalizedString(id);
		}
	}
}
