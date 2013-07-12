using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class XtraSpellChecker_zhchs : DevExpress.XtraSpellChecker.Localization.SpellCheckerResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId id)
		{
			switch (id)
			{
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.ListBoxNoSuggestions:
					return "没有建议";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MsgBoxCheckNotSelectedText:
					return "已经完成所选部分的检查。是否继续检查其余项目？";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MsgBoxCaption:
					return "拼写检查器";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MsgBoxFinishCheck:
					return "已经完成拼写检查。";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MsgCanUseCurrentWord:
					return "你选择了一个在主字典或定制字典里不存在的字。是否使用该字并继续检查？";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MsgNotLoadedDictionaryException:
                    return "没有足够的资源来加载{0}字典.";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MnuItemCaption:
					return "拼写检查";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MnuIgnoreAllItemCaption:
					return "忽略全部";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MnuAddtoDictionaryCaption:
					return "增加到字典";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MnuNoSuggestionsCaption:
					return "(没有拼写建议)";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MnuDeleteRepeatedWord:
					return "删除重复字";
				case DevExpress.XtraSpellChecker.Localization.SpellCheckerStringId.MnuIgnoreRepeatedWord:
					return "忽略";
			}
			return base.GetLocalizedString(id);
		}
	}
}
