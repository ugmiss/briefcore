### 建立时间维度表并初始化时间数据一建表： ###
```
 CREATE TABLE [dbo].[Dim_Date](
 [DateID] [int] NOT NULL,
 [Date] [smalldatetime] NULL,
 [Year] [smallint] NULL,
 [Month] [tinyint] NULL,
 [WeekDay] [tinyint] NULL,
 [DayOfMonth] [tinyint] NULL,
 [Quarter] [tinyint] NULL,
 [MonthName] [varchar](9) NULL,
 [DayName] [varchar](9) NULL,
 [QuarterName] [varchar](9) NULL,
 [DayOfYear] [smallint] NULL,
 CONSTRAINT [PK_Dim_Date] PRIMARY KEY CLUSTERED 
(
 [DateID] ASC
)
）
```
## 初始化函数： ##
```
CREATE FUNCTION [dbo].[DATELIST](@BEGIN_DATE AS DATETIME, @END_TIME   AS DATETIME) 
RETURNS @TEMPTABLE  TABLE 
        ( 
   DateID   INT,
   Date    SMALLDATETIME,
   Year    SMALLINT,
   Month   TINYINT,
   WeekDay  TINYINT,
   DayOfMonth TINYINT,
   Quarter   TINYINT,
   MonthName  varchar     (9),
   DayName  varchar     (9),
   QuarterName varchar     (9),
   DayOfYear  smallint
        ) 
AS 
BEGIN 
     WHILE(@BEGIN_DATE<)
         BEGIN
             INSERT INTO @TEMPTABLE
(DateID,Date,Year,Month,WeekDay,DayOfMonth,
Quarter,MonthName,DayName,QuarterName,DayOfYear)
    VALUES(
       CONVERT(INT,CONVERT(VARCHAR, @BEGIN_DATE, 112)),
       @BEGIN_DATE,
       DATEPART(YEAR,@BEGIN_DATE),
       DATEPART(MONTH,@BEGIN_DATE),
       DATEPART(DW,@BEGIN_DATE),
       DATEPART(D,@BEGIN_DATE),
       DATEPART(Q,@BEGIN_DATE),
       CASE DATEPART(MM,@BEGIN_DATE)
  WHEN 1 THEN '一月'   
        WHEN 2 THEN '二月'
        WHEN 3 THEN '三月'
        WHEN 4 THEN '四月'
        WHEN 5 THEN '五月'
        WHEN 6 THEN '六月'
        WHEN 7 THEN '七月'
        WHEN 8 THEN '八月'
        WHEN 9 THEN '九月'
        WHEN 10 THEN '十月'
        WHEN 11 THEN '十一月'
        ELSE '十二月'
       END,
       CASE DATEPART(DW,@BEGIN_DATE)
        WHEN 1 THEN '星期日'   
        WHEN 2 THEN '星期一'
        WHEN 3 THEN '星期二'
        WHEN 4 THEN '星期三'
        WHEN 5 THEN '星期四'
        WHEN 6 THEN '星期五'
        ELSE '星期六'
       END,
       CASE DATEPART(Q,@BEGIN_DATE)
        WHEN 1 THEN '第一季度'   
        WHEN 2 THEN '第二季度'
        WHEN 3 THEN '第三季度'
        ELSE '第四季度'
       END,
       DATEPART(DY,@BEGIN_DATE)
       )
             SET @BEGIN_DATE=@BEGIN_DATE+1
         END 
    RETURN 
END 
```
### 初始化 ###
```
insert dim_date select * from dbo.DATELIST('2000-01-01','2001-02-02')
```