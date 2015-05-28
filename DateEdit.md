## Mask ##
```
//RepositoryItemDateEdit和DataEdit都有Mask属性（RepositoryItemDateEdit.Mask）
//DataEdit在Properties中 （DataEdit.Properties.Mask）
//以下代码实现的是RepositoryItemDateEdit的年月模式。
date.Mask.EditMask = "y";
date.Mask.UseMaskAsDisplayFormat = true;
```
|标准掩码|名称|说明|
|:-----------|:-----|:-----|
|d           | 短日期模式|  此掩码与由 DateTimeFormatInfo.ShortDatePattern 属性指定的模式相匹配。|
|D           | 长日期模式|  此掩码与由 DateTimeFormatInfo.LongDatePattern 属性指定的模式相匹配。|
|t           | 短时间模式 | 此掩码与由 DateTimeFormatInfo.ShortTimePattern 属性指定的模式相匹配。  |
|T           | 长时间模式|  此掩码与由 DateTimeFormatInfo.LongTimePattern 属性指定的模式相匹配。|
|f           | 完整日期/时间模式(短时间)|  此掩码组合了长日期和短时间模式，由空格符分隔。|
|F           | 完整日期/时间模式(长时间) | 此掩码与由 DateTimeFormatInfo.FullDateTimePattern 属性指定的模式相匹配。 |
|g           |常规日期/时间模式(短时间)|  此掩码组合了短日期和短时间模式，由空格符分隔。|
|G           | 常规日期/时间模式(长时间) | 此掩码组合了短日期和长时间模式，由空格符分隔。|
|M 或 m     | 月日模式|  此掩码与由 DateTimeFormatInfo.MonthDayPattern 属性指定的模式相匹配。    |
|R 或 r     |RFC1123 模式|  此掩码与由 DateTimeFormatInfo.RFC1123Pattern 属性指定的模式相匹配。   |
|s           |可排序的日期/时间模式；|符合 ISO 8601  由掩码与由 DateTimeFormatInfo.SortableDateTimePattern 属性指定的模式相匹配。|
|u           |通用的可排序日期/时间模式|  此掩码与由 DateTimeFormatInfo.UniversalSortableDateTimePattern 属性指定的模式相匹配。 |
|Y 或 y     | 年月模式 | 此掩码与由 DateTimeFormatInfo.YearMonthPattern 属性指定的模式相匹配。|