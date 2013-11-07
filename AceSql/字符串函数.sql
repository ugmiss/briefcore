/* 字符函数 */
/* 返回字符表达式中最左侧字符的ASCII代码值 */
select Ascii('a') --a:97,A:65
 
/* 将整数ASCII代码转换为字符 */
select Char(97)--97:a,65:A
 
/* 返回表达式中指定字符的开始位置 */
select Charindex('b','abcdefg',5)
 
/* 以整数返回两个字符表达式的SOUNDEX值之差 */
select Difference('bet','bit')--3
 
/* 返回字符表达式最左侧指定数目的字符 */
select Left('abcdefg',3)--abc
 
/* 返回给定字符串表达的字符数 */
select Len('abcdefg')--7
 
/* 返回将大写字符转换为小字符的字符表达式 */
select Lower('ABCDEFG')--abcdefg
 
/* 返回删除了前导空格之后字符表达式 */
select Ltrim('　　 abcdefg')--abcdefg
 
/* 返回具有给定的整数代码的UNICODE字符 */
select Nchar(65)--A
 
/* 返回指定表达式中模式第一次出现的开始位置 */
select Patindex('%_cd%','abcdefg')--2
 
/* 返回为成为有效的SQL SERVER分隔标识符而添加了分隔符的UNICODE字符串 */
select Quotename('create table')
 
/* 用第三个表达式替换第一个表达式中出现的第二个表达式 */
select Replace('abcdefg','cd','xxx')--abxxxefg
 
/* 按指定次数重复表达式 */
select Replicate('abc|',4)--abc|abc|abc|abc|
 
/* 返回字符表达式的逆向表达式 */
select Reverse('abc')--cba
 
/* 返回字符表达式右侧指定数目的字符 */
select Right('abcd',3)--bcd
 
/* 返回截断了所有尾随空格之后的字符表达式 */
select Rtrim('abcd　　　 ')--abcd
 
/* 返回由四个字符表达的SOUNDEX代码 */
select Soundex('abcd')--A120
 
/* 返回由重复空格组成的字符串 */
select Space(10)--[　　　　　　　　　 ]
 
/* 返回从默认表达转换而来的字符串 */
select Str(100)--[　　　　　　 100]
 
/*　 */
select Str(100,3)--[100]
 
/*　 */
select Str(14.4444,5,4)--[14.44]
 
/* 删除指定长度的字符,并在指定的起点处插入另一组字符 */
select Stuff('abcdefg',2,4,'xxx')--axxxfg
 
/* 返回字符表达式,二进制,文本表达式或图像表达的一部分 */
select Substring('abcdefg',2,3)--bcd
 
/* 返回表达第一个字符的UNICODE整数值 */
select Unicode('a')--97
 
/* 返回将小写字符转换为大写字符的字符表达式 */
select Upper('a')--'A'
