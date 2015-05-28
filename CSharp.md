### string ###
```
// 空串
string.Empty
// 判断null或空串
string.IsNullOrEmpty()
// 格式化
string.Format("{0}{1}",a,b);
// 拼串
string.Join(",", (from c in dictPlan[mi.Name].LsFileInfo select c.MusicFileName).ToArray());
// 拆串
string s=" asd , asd,as|qw ";
s.Split(new char[] { ',', '|', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
```

### 格式串 ###
```

一、用{0:?}格式化

可通过 String.Format 方法或通过 Console.Write 方法格式化数值结果，其中后一种方法调用 String.Format。使用格式字符串指定格式。下表包含受支持的标准格式字符串。格式字符串采用的形式为 Axx，其中 A 为“格式说明符”，而 xx 为“精度说明符”。格式说明符控制应用于数值的格式化类型，而精度说明符控制格式化输出的有效位数或小数位数。

有关标准及自定义格式化字符串的更多信息，请参阅格式化概述。有关 String.Format 方法的更多信息，请参阅 String.Format 方法。

字符 说明 示例 输出 
C 或 c 货币 Console.Write("{0:C}", 2.5); 
Console.Write("{0:C}", -2.5);
 $2.50 
($2.50)
 
D 或 d 十进制 Console.Write("{0:D5}", 25); 00025 
E 或 e 科学型 Console.Write("{0:E}", 250000); 2.500000E+005 
F 或 f 固定点 Console.Write("{0:F2}", 25); 
Console.Write("{0:F0}", 25);
 25.00 
25
 
G 或 g 常规 Console.Write("{0:G}", 2.5); 2.5 
N 或 n 数字 Console.Write("{0:N}", 2500000); 2,500,000.00 
X 或 x 十六进制 Console.Write("{0:X}", 250); 
Console.Write("{0:X}", 0xffff);
 FA 
FFFF
 

二、日期格式化

如果格式字符串只包含下表列出的某个单个格式说明符，则它们被解释为标准格式说明符。如果指定的格式字符是单个字符并且不包含在下表中，则引发异常。如果格式字符串在长度上比单个字符长（即使多余的字符是空白），则格式字符串被解释为自定义格式字符串。请注意，这些格式说明符产生的模式受“区域选项”控制面板中的设置的影响。具有不同区域性或不同日期与时间设置的计算机将显示不同的模式。

格式字符串显示的时间和日期分隔符由与当前区域性的 DateTimeFormat 属性关联的 DateSeparator 和 TimeSeparator 字符定义。然而，如果 InvariantCulture 被“r”、“s”和“u”说明符引用，与 DateSeparator 和 TimeSeparator 字符关联的字符不随当前区域性更改。下表描述了格式化 DateTime 对象的标准格式字符串。

格式说明符 名称 说明 
d 短日期模式 显示由与当前线程关联的 DateTimeFormatInfo.ShortDatePattern 属性定义的模式或者由指定格式提供程序定义的模式。 
D 长日期模式 显示由与当前线程关联的 DateTimeFormatInfo.LongDatePattern 属性定义的模式或者由指定格式提供程序定义的模式。 
t 短时间模式 显示由与当前线程关联的 DateTimeFormatInfo.ShortTimePattern 属性定义的模式或者由指定格式提供程序定义的模式。 
T 长时间模式 显示由与当前线程关联的 DateTimeFormatInfo.LongTimePattern 属性定义的模式或者由指定格式提供程序定义的模式。 
f 完整日期/时间模式（短时间） 显示长日期和短时间模式的组合，由空格分隔。 
F 完整日期/时间模式（长时间） 显示由与当前线程关联的 DateTimeFormatInfo.FullDateTimePattern 属性定义的模式或者由指定格式提供程序定义的模式。 
g 常规日期/时间模式（短时间） 显示短日期和短时间模式的组合，由空格分隔。 
G 常规日期/时间模式（长时间） 显示短日期和长时间模式的组合，由空格分隔。 
M 或 m 月日模式 显示由与当前线程关联的 DateTimeFormatInfo.MonthDayPattern 属性定义的模式或者由指定格式提供程序定义的模式。 
R 或 r RFC1123 模式 显示由与当前线程关联的 DateTimeFormatInfo.RFC1123Pattern 属性定义的模式或者由指定格式提供程序定义的模式。这是定义的标准，并且属性是只读的；因此，无论所使用的区域性或所提供的格式提供程序是什么，它总是相同的。属性引用 CultureInfo.InvariantCulture 属性并遵照自定义模式“ddd, dd MMMM yyyy HH:mm:ss G\MT”。请注意，“GMT”中的“M”需要转义符，因此它不被解释。 
s 可排序的日期/时间模式；符合 ISO 8601 显示由与当前线程关联的 DateTimeFormatInfo.SortableDateTimePattern 属性定义的模式或者由指定格式提供程序定义的模式。属性引用 CultureInfo.InvariantCulture 属性，格式遵照自定义模式“yyyy-MM-ddTHH:mm:ss”。 
u 通用的可排序日期/时间模式 显示由与当前线程关联的 DateTimeFormatInfo.UniversalSortableDateTimePattern 属性定义的模式或者由指定格式提供程序定义的模式。因为它是定义的标准，并且属性是只读的，因此无论区域性或格式提供程序是什么，模式总是相同的。格式遵照自定义模式“yyyy-MM-dd HH:mm:ssZ”。 
U 通用的可排序日期/时间模式 显示由与当前线程关联的 DateTimeFormatInfo.FullDateTimePattern 属性定义的模式或者由指定格式提供程序定义的模式。请注意，显示的时间是通用时间，而不是本地时间。 
Y 或 y 年月模式 显示由与当前线程关联的 DateTimeFormatInfo.YearMonthPattern 属性定义的模式或者由指定格式提供程序定义的模式。 
任何其他单个字符 未知说明符  

下表显示了格式说明符示例的列表，这些示例应用于公开当前日期和时间信息的 DateTime.Now 的任意值。示例中给出了不同的区域性设置以阐释更改当前区域性的影响。这通常以下面几种方式更改：使用 Microsoft Windows 中的“日期/时间”控制面板，将您自己的 DateTimeFormatInfo 对象作为格式提供程序传递，或将 CultureInfo 对象设置传递给不同的区域性。请注意，对于“r”和“s”格式，更改区域性不影响输出。此表是说明标准日期和时间说明符如何影响格式化的快速指南。请参阅该表下面阐释这些说明符的代码示例部分。

格式说明符 当前区域性 输出 
d en-US 4/10/2001 
d en-NZ 10/04/2001 
d de-DE 10.04.2001 
D en-US Tuesday, April 10, 2001 
T en-US 3:51:24 PM 
T es-ES 15:51:24 
f en-US Tuesday, April 10, 2001 3:51 PM 
f fr-FR mardi 10 avril 2001 15:51 
r en-US Tue, 10 Apr 2001 15:51:24 GMT 
r zh-SG Tue, 10 Apr 2001 15:51:24 GMT 
s en-US 2001-04-10T15:51:24 
s pt-BR 2001-04-10T15:51:24 
u en-US 2001-04-10 15:51:24Z 
u sv-FI 2001-04-10 15:51:24Z 
m en-US April 10 
m ms-MY 10 April 
y en-US April, 2001 
y af-ZA April 2001 
L en-UZ 无法识别的格式说明符；引发格式异常。 

下面的代码示例阐释如何使用对 DateTime 对象使用自定义格式字符串。

[Visual Basic]Dim dt As DateTime = DateTime.NowDim dfi As DateTimeFormatInfo = New DateTimeFormatInfo()Dim ci As CultureInfo = New CultureInfo("de-DE")' Make up a new custom DateTime pattern, for demonstration.dfi.MonthDayPattern = "MM-MMMM, ddd-dddd"' Use the DateTimeFormat from the culture associated ' with the current thread.Console.WriteLine( dt.ToString("d") )    Console.WriteLine( dt.ToString("m") )' Use the DateTimeFormat from the specific culture passed.Console.WriteLine( dt.ToString("d", ci ) )' Use the settings from the DateTimeFormatInfo object passed.Console.WriteLine( dt.ToString("m", dfi ) )' Reset the current thread to a different culture.Thread.CurrentThread.CurrentCulture = New CultureInfo("fr-BE")Console.WriteLine( dt.ToString("d") )[C#]DateTime dt = DateTime.Now;DateTimeFormatInfo dfi = new DateTimeFormatInfo();CultureInfo ci = new CultureInfo("de-DE");// Make up a new custom DateTime pattern, for demonstration.dfi.MonthDayPattern = "MM-MMMM, ddd-dddd";// Use the DateTimeFormat from the culture associated // with the current thread.Console.WriteLine( dt.ToString("d") );    Console.WriteLine( dt.ToString("m") );// Use the DateTimeFormat from the specific culture passed.Console.WriteLine( dt.ToString("d", ci ) );// Use the settings from the DateTimeFormatInfo object passed.Console.WriteLine( dt.ToString("m", dfi ) );// Reset the current thread to a different culture.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-BE");Console.WriteLine( dt.ToString("d") );有时需要对 DateTime 对象的格式化有更多的控制。作为标准 DateTime 格式的替换方法，您可以使用自定义 DateTime 格式字符串构造您自己的 DateTime 格式化模式。实际上，标准格式就是从这些自定义格式说明符派生的。

下表显示了自定义格式说明符并描述了它们产生的值。这些字符串的输出受“区域选项”控制面板中的当前区域性和设置的影响。

格式说明符 说明 
d 显示月份的当前日期，以 1 到 31 之间的一个数字表示，包括 1 和 31。如果日期只有一位数字 (1-9)，则它显示为一位数字。 
请注意，如果“d”格式说明符单独使用，没有其他自定义格式字符串，则它被解释为标准短日期模式格式说明符。如果“d”格式说明符与其他自定义格式说明符一起传递，则它被解释为自定义格式说明符。
 
dd 显示月份的当前日期，以 1 到 31 之间的一个数字表示，包括 1 和 31。如果日期只有一位数字 (1-9)，则将其格式化为带有前导 0 (01-09)。 
ddd 显示指定 DateTime 对象的日部分缩写名称。如果未提供特定的有效格式提供程序（实现具有预期属性的 IFormatProvider 的非空对象），则使用 DateTimeFormat 对象的 AbbreviatedDayNames 属性及其与当前线程关联的当前区域性。否则，使用来自指定格式提供程序的 AbbreviatedDayNames 属性。 
dddd（外加任意数量的附加“d”字符） 显示指定 DateTime 对象的日部分全名。如果未提供特定的有效格式提供程序（实现具有预期属性的 IFormatProvider 的非空对象），则使用 DateTimeFormat 对象的 DayNames 属性及其与当前线程关联的当前区域性。否则，使用来自指定格式提供程序的 DayNames 属性。 
f 显示以一位数字表示的秒。 
请注意，如果“f”格式说明符单独使用，没有其他自定义格式字符串，则它被解释为完整的（长日期 + 短时间）格式说明符。如果“f”格式说明符与其他自定义格式说明符一起传递，则它被解释为自定义格式说明符。
 
ff 显示以二位数字表示的秒。 
fff 显示以三位数字表示的秒。 
ffff 显示以四位数字表示的秒。 
fffff 显示以五位数字表示的秒。 
ffffff 显示以六位数字表示的秒。 
fffffff 显示以七位数字表示的秒。 
g 或 gg（外加任意数量的附加“g”字符） 显示指定 DateTime 对象的年代部分（例如 A.D.）。如果未提供特定的有效格式提供程序（实现具有预期属性的 IFormatProvider 的非空对象），则年代由与 DateTimeFormat 对象关联的日历及其与当前线程关联的当前区域性确定。 
请注意，如果“g”格式说明符单独使用，没有其他自定义格式字符串，则它被解释为标准常规格式说明符。如果“g”格式说明符与其他自定义格式说明符一起传递，则它被解释为自定义格式说明符。
 
h 以 1 到 12 这一范围中的一个数字显示指定 DateTime 对象的小时部分。该小时部分表示自午夜（显示为 12）或中午（也显示为 12）后所经过的整小时数。如果单独使用这种格式，则无法区别某一小时是中午以前还是中午以后的时间。如果该小时是单个数字 (1-9)，则它显示为单个数字。显示小时时不发生任何舍入。例如，DateTime 为 5:43 时返回 5。 
hh, hh（外加任意数量的附加“h”字符） 以 1 到 12 这一范围中的一个数字显示指定 DateTime 对象的小时部分。该小时部分表示自午夜（显示为 12）或中午（也显示为 12）后所经过的整小时数。如果单独使用这种格式，则无法区别某一小时是中午以前还是中午以后的时间。如果该小时是单个数字 (1-9)，则将其格式化为前面带有 0 (01-09)。 
H 以 0 到 23 这一范围中的一个数字显示指定 DateTime 对象的小时部分。该小时部分表示自午夜（显示为 0）后所经过的整小时数。如果该小时是单个数字 (0-9)，则它显示为单个数字。 
HH, HH（外加任意数量的附加“H”字符） 以 0 到 23 这一范围中的一个数字显示指定 DateTime 对象的小时部分。该小时部分表示自午夜（显示为 0）后所经过的整小时数。如果该小时是单个数字 (0-9)，则将其格式化为前面带有 0 (01-09)。 
m 以 0 到 59 这一范围中的一个数字显示指定 DateTime 对象的分钟部分。该分钟部分表示自上个小时后所经过的整分钟数。如果分钟是一位数字 (0-9)，则它显示为一位数字。 
请注意，如果“m”格式说明符单独使用，没有其他自定义格式字符串，则它被解释为标准的月日模式格式说明符。如果“m”格式说明符与其他自定义格式说明符一起传递，则它被解释为自定义格式说明符。
 
mm, mm（外加任意数量的附加“m”字符） 以 0 到 59 这一范围中的一个数字显示指定 DateTime 对象的分钟部分。该分钟部分表示自上个小时后所经过的整分钟数。如果分钟是一位数字 (0-9)，则将其格式化为带有前导 0 (01-09)。 
M 显示当前月份，以 1 到 12 之间的一个数字表示，包括 1 和 12。如果月份是一位数字 (1-9)，则它显示为一位数字。 
请注意，如果“M”格式说明符单独使用，没有其他自定义格式字符串，则它被解释为标准的月天模式格式说明符。如果“M”格式说明符与其他自定义格式说明符一起传递，则它被解释为自定义格式说明符。
 
MM 显示当前月份，以 1 到 12 之间的一个数字表示，包括 1 和 12。如果月份是一位数字 (1-9)，则将其格式化为带有前导 0 (01-09)。 
MMM 显示指定 DateTime 对象的月部分缩写名称。如果未提供特定的有效格式提供程序（实现具有预期属性的 IFormatProvider 的非空对象），则使用 DateTimeFormat 对象的 AbbreviatedMonthNames 属性及其与当前线程关联的当前区域性。否则，使用来自指定格式提供程序的 AbbreviatedMonthNames 属性。 
MMMM 显示指定 DateTime 对象的月部分全名。如果未提供特定的有效格式提供程序（实现具有预期属性的 IFormatProvider 的非空对象），则使用 DateTimeFormat 对象的 MonthNames 属性及其与当前线程关联的当前区域性。否则，使用来自指定格式提供程序的 MonthNames 属性。 
s 以 0 到 59 这一范围中的一个数字显示指定 DateTime 对象的秒部分。该秒部分表示自上一分钟后经过的整秒数。如果秒是一位数字 (0-9)，则它仅显示为一位数字。 
请注意，如果“s”格式说明符单独使用，没有其他自定义格式字符串，则它被解释为标准的可排序日期/时间模式格式说明符。如果“s”格式说明符与其他自定义格式说明符一起传递，则它被解释为自定义格式说明符。
 
ss, ss（外加任意数量的附加“s”字符） 以 0 到 59 这一范围中的一个数字显示指定 DateTime 对象的秒部分。该秒部分表示自上一分钟后经过的整秒数。如果秒是一位数字 (0-9)，则将其格式化为带有前导 0 (01-09)。 
t 显示指定 DateTime 对象 A.M./P.M. 指示项的第一个字符。如果未提供特定的有效格式提供程序（实现具有预期属性的 IFormatProvider 的非空对象），则使用 DateTimeFormat 对象的 AMDesignator（或 PMDesignator）属性及其与当前线程关联的当前区域性。否则，使用来自指定 IFormatProvider 的 AMDesignator（或 PMDesignator）属性。如果对于指定的 DateTime 所经过的总整小时数小于 12，则使用 AMDesignator。否则，使用 PMDesignator。 
请注意，如果“t”格式说明符单独使用，没有其他自定义格式字符串，则它被解释为标准的长时间模式格式说明符。如果“t”格式说明符与其他自定义格式说明符一起传递，则它被解释为自定义格式说明符。
 
tt, tt（外加任意数量的附加“t”字符） 显示指定 DateTime 对象的 A.M./P.M. 指示项。如果未提供特定的有效格式提供程序（实现具有预期属性的 IFormatProvider 的非空对象），则使用 DateTimeFormat 对象的 AMDesignator（或 PMDesignator）属性及其与当前线程关联的当前区域性。否则，使用来自指定 IFormatProvider 的 AMDesignator（或 PMDesignator）属性。如果对于指定的 DateTime 所经过的总整小时数小于 12，则使用 AMDesignator。否则，使用 PMDesignator。 
y 将指定 DateTime 对象的年份部分显示为位数最多为两位的数字。忽略年的前两位数字。如果年份是一位数字 (1-9)，则它显示为一位数字。 
yy 将指定 DateTime 对象的年份部分显示为位数最多为两位的数字。忽略年的前两位数字。如果年份是一位数字 (1-9)，则将其格式化为带有前导 0 (01-09)。 
yyyy 显示指定 DateTime 对象的年份部分（包括世纪）。如果年份长度小于四位，则按需要在前面追加零以使显示的年份长度达到四位。 
z 仅以整小时数为单位显示系统当前时区的时区偏移量。偏移量总显示为带有前导或尾随符号（零显示为“+0”），指示早于格林威治时间 (+) 或迟于格林威治时间 (-) 的小时数。值的范围是 –12 到 +13。如果偏移量为一位数 (0-9)，则将其显示为带合适前导符号的一位数。该时区的设置指定为 +X 或 –X，其中 X 是相对 GMT 以小时为单位的偏移量。所显示的偏移量受夏时制的影响。 
zz 仅以整小时数为单位显示系统当前时区的时区偏移量。偏移量总显示为带有前导或尾随符号（零显示为“+00”），指示早于格林威治时间 (+) 或迟于格林威治时间 (-) 的小时数。值范围为 –12 到 +13。如果偏移量为单个数字 (0-9)，则将其格式化为前面带有 0 (01-09) 并带有适当的前导符号。该时区的设置指定为 +X 或 –X，其中 X 是相对 GMT 以小时为单位的偏移量。所显示的偏移量受夏时制的影响。 
zzz, zzz（外加任意数量的附加“z”字符） 以小时和分钟为单位显示系统当前时区的时区偏移量。偏移量总是显示为带有前导或尾随符号（零显示为“+00:00”），指示早于格林威治时间 (+) 或迟于格林威治时间 (-) 的小时和分钟数。值范围为 –12 到 +13。如果偏移量为单个数字 (0-9)，则将其格式化为前面带有 0 (01-09) 并带有适当的前导符号。该时区的设置指定为 +X 或 –X，其中 X 是相对 GMT 以小时为单位的偏移量。所显示的偏移量受夏时制的影响。 
: 时间分隔符。 
/ 日期分隔符。 
" 带引号的字符串。显示转义符 (/) 之后两个引号之间的任何字符串的文本值。 
' 带引号的字符串。显示两个“'”字符之间的任何字符串的文本值。 
%c 其中 c 是标准格式字符，显示与格式字符关联的标准格式模式。 
\c 其中 c 是任意字符，转义符将下一个字符显示为文本。在此上下文中，转义符不能用于创建转义序列（如“\n”表示换行）。 
任何其他字符 其他字符作为文本直接写入输出字符串。 

向 DateTime.ToString 传递自定义模式时，模式必须至少为两个字符长。如果只传递“d”，则公共语言运行库将其解释为标准格式说明符，这是因为所有单个格式说明符都被解释为标准格式说明符。如果传递单个“h”，则引发异常，原因是不存在标准的“h”格式说明符。若要只使用单个自定义格式进行格式化，请在说明符的前面或后面添加一个空格。例如，格式字符串“h”被解释为自定义格式字符串。

下表显示使用任意值 DateTime.Now（该值显示当前时间）的示例。示例中给出了不同的区域性和时区设置，以阐释更改区域性的影响。可以通过下列方法更改当前区域性：更改 Microsoft Windows 的“日期/时间”控制面板中的值，传递您自己的 DateTimeFormatInfo 对象，或将 CultureInfo 对象设置传递给不同的区域性。此表是说明自定义日期和时间说明符如何影响格式化的快速指南。请参阅该表下面阐释这些说明符的代码示例部分。

格式说明符 当前区域性 时区 输出 
d, M en-US GMT 12, 4 
d, M es-MX GMT 12, 4 
d MMMM en-US GMT 12 April 
d MMMM es-MX GMT 12 Abril 
dddd MMMM yy gg en-US GMT Thursday April 01 A.D. 
dddd MMMM yy gg es-MX GMT Jueves Abril 01 DC 
h , m: s en-US GMT 6 , 13: 12 
hh,mm:ss en-US GMT 06,13:12 
HH-mm-ss-tt en-US GMT 06-13-12-AM 
hh:mm, G\MT z en-US GMT 05:13 GMT +0 
hh:mm, G\MT z en-US GMT +10:00 05:13 GMT +10 
hh:mm, G\MT zzz en-US GMT 05:13 GMT +00:00 
hh:mm, G\MT zzz en-US GMT -9:00 05:13 GMT -09:00 

请注意，在某些语言（如 C#）中，“\”字符在与 ToString 方法共用时，它前面必须有转义符。

下面的代码示例阐释如何从 DateTime 对象创建自定义格式化字符串。此示例假定当前区域性是美国英语 (en-US)。

[Visual Basic]Dim MyDate As New DateTime(2000, 1, 1, 0, 0, 0)Dim MyString As String = MyDate.ToString("dddd - d - MMMM")//' In the U.S. English culture, MyString has the value: ' "Saturday - 1 - January".MyString = MyDate.ToString("yyyy gg")//' In the U.S. English culture, MyString has the value: "2000 A.D.".[C#]DateTime MyDate = new DateTime(2000, 1, 1, 0, 0, 0);String MyString = MyDate.ToString("dddd - d - MMMM");// In the U.S. English culture, MyString has the value:    "Saturday - 1 - January".MyString = MyDate.ToString("yyyy gg");// In the U.S. English culture, MyString has the value: "2000 A.D.".
```