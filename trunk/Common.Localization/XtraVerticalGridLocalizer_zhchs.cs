using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Localizer
{
	public class XtraVerticalGridLocalizer_zhchs : DevExpress.XtraVerticalGrid.Localization.VGridResLocalizer
	{
		public override string Language
		{
			get
			{
				return "简体中文";
			}
		}
		public override string GetLocalizedString(DevExpress.XtraVerticalGrid.Localization.VGridStringId id)
		{
			switch (id)
			{
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.RowCustomizationText:
					return "定制";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.RowCustomizationNewCategoryFormText:
					return "新增数据类别";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.RowCustomizationNewCategoryFormLabelText:
					return "标题：";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.RowCustomizationNewCategoryText:
					return "新增";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.RowCustomizationDeleteCategoryText:
					return "删除";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.RowCustomizationTabPageCategoriesText:
					return "分类数据";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.RowCustomizationTabPageRowsText:
					return "数据列";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.InvalidRecordExceptionText:
					return "是否要修改不正确的数据值？";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.StyleCreatorName:
					return "风格定制器";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.MenuReset:
					return "重置";
				case DevExpress.XtraVerticalGrid.Localization.VGridStringId.MenuRowPropertiesExpressionEditor:
					return "表达式编辑器...";
			}
			return base.GetLocalizedString(id);
		}
	}
}
