--当前系统日期、时间 
SELECT GETDATE()  
--DATEADD  在向指定日期加上一段时间的基础上，返回新的 datetime 值;例如：向日期加上2天 
SELECT DATEADD(DAY,2,'2004-10-15')  --返回：2004-10-17 00:00:00.000
--DATEDIFF 返回跨两个指定日期的日期和时间边界数：
SELECT DATEDIFF(DAY,'2004-09-01','2004-09-18')   --返回：17
--DATEPART 返回代表指定日期的指定日期部分的整数：
SELECT DATEPART(MONTH, '2004-10-15')  --返回 10
--DATENAME 返回代表指定日期的指定日期部分的字符串：
SELECT DATENAME(WEEKDAY, '2004-10-15')  --返回：星期五
--DAY(), month(),year() --可以与datepart对照一下：
SELECT 当前日期=CONVERT(VARCHAR(10),GETDATE(),120) 
,当前时间=CONVERT(VARCHAR(8),GETDATE(),114) 
SELECT DATENAME(dw,'2004-10-15') 
SELECT 本年第多少周=DATENAME(WEEK,'2004-10-15')
       ,今天是周几=DATENAME(WEEKDAY,'2004-10-15')
--GETDATE()  返回系统目前的日期与时间 
--DATEDIFF (interval,date1,date2) 以interval 指定的方式，返回date2 与date1两个日期之间的差值 date2-date1 
--DATEADD (interval,number,date) 以interval指定的方式，加上number之后的日期 
--DatePart (interval,date) 返回日期date中，interval指定部分所对应的整数值 
--DATENAME (interval,date) 返回日期date中，interval指定部分所对应的字符串名称 
--参数 interval的设定值如下：
--值 缩 写（Sql Server） (Access 和 ASP) 说明 
--Year Yy yyyy 年 1753 ~ 9999 
--Quarter Qq q   季 1 ~ 4 
--Month Mm m   月1 ~ 12 
--DAY of year Dy y  一年的日数,一年中的第几日 1-366 
--DAY Dd d   日，1-31 
--WeekDAY Dw w 一周的日数，一周中的第几日 1-7 
--Week Wk ww  周，一年中的第几周 0 ~ 51 
--Hour Hh h   时0 ~ 23 
--Minute Mi n  分钟0 ~ 59 
--Second Ss s 秒 0 ~ 59 
--Millisecond Ms - 毫秒 0 ~ 999 
--一个月第一天的
SELECT DATEADD(mm, DATEDIFF(mm,0,GETDATE()), 0)
--本周的星期一
SELECT DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0)
--一年的第一天
SELECT DATEADD(yy, DATEDIFF(yy,0,GETDATE()), 0)
--季度的第一天
SELECT DATEADD(qq, DATEDIFF(qq,0,GETDATE()), 0)
--当天的半夜
SELECT DATEADD(dd, DATEDIFF(dd,0,GETDATE()), 0)
--上个月的最后一天
SELECT DATEADD(ms,-3,DATEADD(mm, DATEDIFF(mm,0,GETDATE()), 0))
--去年的最后一天
SELECT DATEADD(ms,-3,DATEADD(yy, DATEDIFF(yy,0,GETDATE()), 0))
--本月的最后一天
SELECT DATEADD(ms,-3,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1, 0))
--本年的最后一天
SELECT DATEADD(ms,-3,DATEADD(yy, DATEDIFF(yy,0,GETDATE())+1, 0))
--本月的第一个星期一
SELECT DATEADD(wk, DATEDIFF(wk,0,DATEADD(dd,6-datepart(DAY,GETDATE()),GETDATE())), 0) 

 
