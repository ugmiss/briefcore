
```
--创建分区表过程一共分为三步:创建分区函数、创建分区方案、创建分区表
 
/*本实验涉及两个表:transactionhistory、transactionhistoryarchive，数据从adventureworks导过来，
下面要将这两张表分别建成分区表*/
--创建分区表transactionhistory
--创建分区函数
use wjz
go
create partition function transactionhistorypf1(datetime)
as range right for values(
'2003-9-1','2003-10-1','2003-11-1','2003-12-1','2004-1-1',
'2004-2-1','2004-3-1','2004-4-1','2004-5-1','2004-6-1',
'2004-7-1','2004-8-1'
);
go
 
--创建分区方案
create partition scheme transactionhistoryps1
as partition transactionhistorypf1 to
([primary],wjz2,wjz3,wjz4,wjz5,wjz6,wjz7,wjz8,wjz9,wjz10,wjz11,wjz12,wjz13,wjz14)
go
 
 
--创建分区表
CREATE TABLE [TransactionHistory](
[TransactionID] [int] IDENTITY(100000,1) NOT NULL,
[ProductID] [int] NOT NULL,
[ReferenceOrderID] [int] NOT NULL,
[ReferenceOrderLineID] [int] NOT NULL CONSTRAINT [DF_TransactionHistory_ReferenceOrderLineID]   DEFAULT ((0)),
[TransactionDate] [datetime] NOT NULL CONSTRAINT [DF_TransactionHistory_TransactionDate]   DEFAULT (getdate()),
[TransactionType] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Quantity] [int] NOT NULL,
[ActualCost] [money] NOT NULL,
[ModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_TransactionHistory_ModifiedDate]   DEFAULT (getdate()),
 
) ON transactionhistoryps1(TransactionDate);
go
 
 
--创建分区表transactionhistoryarchive
--创建分区函数
create partition function transactionhistoryarchivepf1(datetime)
as range right for values( '2003-9-1','2003-10-1')
go
 
--创建分区方案
create partition scheme transactionhistoryarchiveps1
as partition transactionhistoryarchivepf1 to(
[primary],wjz2,wjz3
);
go
 
--创建分区表
CREATE TABLE [TransactionHistoryArchive](
[TransactionID] [int] NOT NULL,
[ProductID] [int] NOT NULL,
[ReferenceOrderID] [int] NOT NULL,
[ReferenceOrderLineID] [int] NOT NULL CONSTRAINT [DF_TransactionHistoryArchive_ReferenceOrderLineID]   DEFAULT ((0)),
[TransactionDate] [datetime] NOT NULL CONSTRAINT [DF_TransactionHistoryArchive_TransactionDate]   DEFAULT (getdate()),
[TransactionType] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Quantity] [int] NOT NULL,
[ActualCost] [money] NOT NULL,
[ModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_TransactionHistoryArchive_ModifiedDate]   DEFAULT (getdate()),
 
) ON transactionhistoryarchiveps1(transactiondate);
go
 
 
--Load data use SSIS
 
--分区表创建完毕
 
--管理分区表
--将transactionhistory的第2个分区移动至transactionhistoryarchive的第2个分区
--注意，两张表的第2个分区刚好位于同一个文件组wjz2中
alter table transactionhistory
switch partition 2 to
transactionhistoryarchive
partition 2
go
--验证一下数据已经在一秒钟之内转移到新表里了
select * from transactionhistoryarchive
 
--为transactionhistory表在右端新增一个分区
--注意，因为我创建分区方案时多写了一个文件组，因此那个多余的文件组就成了next used，
--否则要先修改分区方案来增加新的可用文件组
alter partition function transactionhistorypf1()
split range ('2004-9-1')
 
--将transactionhistory表左端的两个空分区合而为一
alter partition function transactionhistorypf1()
merge range('2003-9-1')
go
 
--为transactionhistoryarchive表在右端新增一个分区，做好下次转移准备，注意现在要先修改分区方案
alter partition scheme transactionhistoryarchiveps1
next used wjz4
go
alter partition function transactionhistoryarchivepf1()
split range('2003-11-1')
go
 
--将transactionhistoryarchive表左端的两个分区合并（这个可选，至少是好是坏，目前还没想好）
alter partition function transactionhistoryarchivepf1()
merge range('2003-9-1')
 
 
/*删除用的
drop table transactionhistoryarchive
go
drop partition scheme transactionhistoryarchiveps1
go
drop partition function transactionhistoryarchivepf1
go
 
drop table transactionhistory
go
drop partition scheme transactionhistoryps1
go
drop partition function transactionhistorypf1
go*/
 
--以上情况可写成job进行循环
```
SQL大表转为分区表实例
-- 进行演示操作前, 先备份, 以便可以在演示完成后, 恢复到原始状态
USE master
-- 备份
BACKUP DATABASE AdventureWorks
> TO DISK = 'AdventureWorks.bak'
> WITH FORMAT

---- 恢复
--RESTORE DATABASE AdventureWorks
--    FROM DISK = 'AdventureWorks.bak'
--    WITH REPLACE
GO

--=========================================
-- 转换为分区表
--=========================================
USE AdventureWorks
GO

-- 1. 创建分区函数
--    a. 适用于存储历史存档记录的分区表的分区函数
DECLARE @dt datetime
SET @dt = '20020101'
CREATE PARTITION FUNCTION PF\_HistoryArchive(datetime)
AS RANGE RIGHT
FOR VALUES(
> @dt,
> DATEADD(Year, 1, @dt))

--    b. 适用于存储历史记录的分区表的分区函数
--DECLARE @dt datetime
SET @dt = '20030901'
CREATE PARTITION FUNCTION PF\_History(datetime)
AS RANGE RIGHT
FOR VALUES(
> @dt,
> DATEADD(Month, 1, @dt), DATEADD(Month, 2, @dt), DATEADD(Month, 3, @dt),
> DATEADD(Month, 4, @dt), DATEADD(Month, 5, @dt), DATEADD(Month, 6, @dt),
> DATEADD(Month, 7, @dt), DATEADD(Month, 8, @dt), DATEADD(Month, 9, @dt),
> DATEADD(Month, 10, @dt), DATEADD(Month, 11, @dt), DATEADD(Month, 12, @dt))
GO

-- 2. 创建分区架构
--    a. 适用于存储历史存档记录的分区表的分区架构
CREATE PARTITION SCHEME PS\_HistoryArchive
AS PARTITION PF\_HistoryArchive
TO([PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md))

--    b. 适用于存储历史记录的分区表的分区架构
CREATE PARTITION SCHEME PS\_History
AS PARTITION PF\_History
TO([PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md),
> [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md),
> [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md),
> [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md),
> [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md), [PRIMARY](PRIMARY.md))
GO

-- 3. 删除索引
--    a. 删除存储历史存档记录的表中的索引
DROP INDEX Production.TransactionHistoryArchive.IX\_TransactionHistoryArchive\_ProductID
DROP INDEX Production.TransactionHistoryArchive.IX\_TransactionHistoryArchive\_ReferenceOrderID\_ReferenceOrderLineID

--    b. 删除存储历史记录的表中的索引
DROP INDEX Production.TransactionHistory.IX\_TransactionHistory\_ProductID
DROP INDEX Production.TransactionHistory.IX\_TransactionHistory\_ReferenceOrderID\_ReferenceOrderLineID
GO

-- 4. 转换为分区表
--    a. 将存储历史存档记录的表转换为分区表
ALTER TABLE Production.TransactionHistoryArchive
> DROP CONSTRAINT PK\_TransactionHistoryArchive\_TransactionID
> WITH(
> > MOVE TO PS\_HistoryArchive(TransactionDate))

--    b.将存储历史记录的表转换为分区表
ALTER TABLE Production.TransactionHistory

> DROP CONSTRAINT PK\_TransactionHistory\_TransactionID
> WITH(
> > MOVE TO PS\_History(TransactionDate))
GO

-- 5. 恢复主键
--    a. 恢复存储历史存档记录的分区表的主键
ALTER TABLE Production.TransactionHistoryArchive

> ADD CONSTRAINT PK\_TransactionHistoryArchive\_TransactionID
> > PRIMARY KEY CLUSTERED(
> > > TransactionID,
> > > TransactionDate)

--    b. 恢复存储历史记录的分区表的主键
ALTER TABLE Production.TransactionHistory

> ADD CONSTRAINT PK\_TransactionHistory\_TransactionID
> > PRIMARY KEY CLUSTERED(
> > > TransactionID,
> > > TransactionDate)
GO

-- 6. 恢复索引
--    a. 恢复存储历史存档记录的分区表的索引
CREATE INDEX IX\_TransactionHistoryArchive\_ProductID

> ON Production.TransactionHistoryArchive(
> > ProductID)

CREATE INDEX IX\_TransactionHistoryArchive\_ReferenceOrderID\_ReferenceOrderLineID

> ON Production.TransactionHistoryArchive(
> > ReferenceOrderID,
> > ReferenceOrderLineID)

--    b. 恢复存储历史记录的分区表的索引
CREATE INDEX IX\_TransactionHistory\_ProductID

> ON Production.TransactionHistory(
> > ProductID)

CREATE INDEX IX\_TransactionHistory\_ReferenceOrderID\_ReferenceOrderLineID

> ON Production.TransactionHistory(
> > ReferenceOrderID,
> > ReferenceOrderLineID)
GO

-- 7. 查看分区表的相关信息
SELECT

> SchemaName = S.name,
> TableName = TB.name,
> PartitionScheme = PS.name,
> PartitionFunction = PF.name,
> PartitionFunctionRangeType = CASE
> > WHEN boundary\_value\_on\_right = 0 THEN 'LEFT'
> > ELSE 'RIGHT' END,

> PartitionFunctionFanout = PF.fanout,
> SchemaID = S.schema\_id,
> ObjectID = TB.object\_id,
> PartitionSchemeID = PS.data\_space\_id,
> PartitionFunctionID = PS.function\_id
FROM sys.schemas S
> INNER JOIN sys.tables TB
> > ON S.schema\_id = TB.schema\_id

> INNER JOIN sys.indexes IDX
> > on TB.object\_id = IDX.object\_id
> > > AND IDX.index\_id < 2

> INNER JOIN sys.partition\_schemes PS
> > ON PS.data\_space\_id = IDX.data\_space\_id

> INNER JOIN sys.partition\_functions PF
> > ON PS.function\_id = PF.function\_id
GO

--=========================================
-- 移动分区表数据
--=========================================
-- 1. 为存储历史存档记录的分区表增加分区, 并接受从历史记录分区表移动过来的数据
--    a. 修改分区架构, 增加用以接受新分区的文件组
ALTER PARTITION SCHEME PS\_HistoryArchive
NEXT USED [PRIMARY](PRIMARY.md)

--    b. 修改分区函数, 增加分区用以接受从历史记录分区表移动过来的数据
DECLARE @dt datetime
SET @dt = '20030901'
ALTER PARTITION FUNCTION PF\_HistoryArchive()
SPLIT RANGE(@dt)

--    c. 将历史记录表中的过期数据移动到历史存档记录表中
ALTER TABLE Production.TransactionHistory

> SWITCH PARTITION 2
> > TO Production.TransactionHistoryArchive PARTITION $PARTITION.PF\_HistoryArchive(@dt)

--    d. 将接受到的数据与原来的分区合并
ALTER PARTITION FUNCTION PF\_HistoryArchive()
MERGE RANGE(@dt)
GO

-- 2. 将存储历史记录的分区表中不包含数据的分区删除, 并增加新的分区以接受新数据
--    a. 合并不包含数据的分区
DECLARE @dt datetime
SET @dt = '20030901'
ALTER PARTITION FUNCTION PF\_History()
MERGE RANGE(@dt)

--    b.  修改分区架构, 增加用以接受新分区的文件组
ALTER PARTITION SCHEME PS\_History
NEXT USED [PRIMARY](PRIMARY.md)

--    c. 修改分区函数, 增加分区用以接受新数据
SET @dt = '20041001'
ALTER PARTITION FUNCTION PF\_History()
SPLIT RANGE(@dt)
GO

--=========================================
-- 清除历史存档记录中的过期数据
--=========================================
-- 1. 创建用于保存过期的历史存档数据的表
CREATE TABLE Production.TransactionHistoryArchive\_2001\_temp(

> TransactionID int NOT NULL,
> ProductID int NOT NULL,
> ReferenceOrderID int NOT NULL,
> ReferenceOrderLineID int NOT NULL
> > DEFAULT ((0)),

> TransactionDate datetime NOT NULL
> > DEFAULT (GETDATE()),

> TransactionType nchar(1) NOT NULL,
> Quantity int NOT NULL,
> ActualCost money NOT NULL,
> ModifiedDate datetime NOT NULL
> > DEFAULT (GETDATE()),

> CONSTRAINT PK\_TransactionHistoryArchive\_2001\_temp\_TransactionID
> > PRIMARY KEY CLUSTERED(
> > > TransactionID,
> > > TransactionDate)
)

-- 2. 将数据从历史存档记录分区表移动到第1步创建的表中
ALTER TABLE Production.TransactionHistoryArchive

> SWITCH PARTITION 1
> > TO Production.TransactionHistoryArchive\_2001\_temp

-- 3. 删除不再包含数据的分区
DECLARE @dt datetime
SET @dt = '20020101'
ALTER PARTITION FUNCTION PF\_HistoryArchive()
MERGE RANGE(@dt)

-- 4. 修改分区架构, 增加用以接受新分区的文件组
ALTER PARTITION SCHEME PS\_HistoryArchive
NEXT USED [PRIMARY](PRIMARY.md)

-- 5. 修改分区函数, 增加分区用以接受新数据
SET @dt = '20040101'
ALTER PARTITION FUNCTION PF\_HistoryArchive()
SPLIT RANGE(@dt)


查询分区信息：

;WITH
TBINFO AS(

> SELECT
> > SchemaName = S.name,
> > TableName = TB.name,
> > PartitionScheme = PS.name,
> > PartitionFunction = PF.name,
> > PartitionFunctionRangeType = CASE
> > > WHEN boundary\_value\_on\_right = 0 THEN 'LEFT'
> > > ELSE 'RIGHT' END,

> > PartitionFunctionFanout = PF.fanout,
> > SchemaID = S.schema\_id,
> > ObjectID = TB.object\_id,
> > PartitionSchemeID = PS.data\_space\_id,
> > PartitionFunctionID = PS.function\_id

> FROM sys.schemas S
> > INNER JOIN sys.tables TB
> > > ON S.schema\_id = TB.schema\_id

> > INNER JOIN sys.indexes IDX
> > > on TB.object\_id = IDX.object\_id
> > > > AND IDX.index\_id < 2

> > INNER JOIN sys.partition\_schemes PS
> > > ON PS.data\_space\_id = IDX.data\_space\_id

> > INNER JOIN sys.partition\_functions PF
> > > ON PS.function\_id = PF.function\_id
),
PF1 AS(

> SELECT PFP.function\_id, PFR.boundary\_id, PFR.value, Type = CONVERT(sysname,
> > CASE T.name
> > > WHEN 'numeric' THEN 'decimal'
> > > WHEN 'real' THEN 'float'
> > > ELSE T.name END

> > + CASE
> > > WHEN T.name IN('decimal', 'numeric')
> > > > THEN QUOTENAME(RTRIM(PFP.precision)
> > > > > + CASE WHEN PFP.scale > 0 THEN ',' + RTRIM(PFP.scale) ELSE '' END, '()')

> > > WHEN T.name IN('float', 'real')
> > > > THEN QUOTENAME(PFP.precision, '()')

> > > WHEN T.name LIKE 'n%char'
> > > > THEN QUOTENAME(PFP.max\_length / 2, '()')

> > > WHEN T.name LIKE '%char' OR T.name LIKE '%binary'
> > > > THEN QUOTENAME(PFP.max\_length, '()')

> > > ELSE '' END)

> FROM sys.partition\_parameters PFP
> > LEFT JOIN sys.partition\_range\_values PFR
> > > ON PFR.function\_id = PFP.function\_id
> > > > AND PFR.parameter\_id = PFP.parameter\_id

> > INNER JOIN sys.types T
> > > ON PFP.system\_type\_id = T.system\_type\_id
),
PF2 AS(

> SELECT **FROM PF1
> UNION ALL
> SELECT
> > function\_id, boundary\_id = boundary\_id - 1, value, type

> FROM PF1
> WHERE boundary\_id = 1
),
PF AS(
> SELECT
> > B.function\_id, boundary\_id = ISNULL(B.boundary\_id + 1, 1),
> > value = STUFF(
> > > CASE
> > > > WHEN A.boundary\_id IS NULL THEN ''
> > > > ELSE ' AND [partition\_column\_name](partition_column_name.md) ' + PF.LessThan + ' ' + CONVERT(varchar(max), A.value) END

> > > + CASE
> > > > WHEN A.boundary\_id = 1 THEN ''
> > > > ELSE ' AND [partition\_column\_name](partition_column_name.md) ' + PF.MoreThan + ' ' + CONVERT(varchar(max), B.value) END,
      1. 5, ''),

> > B.Type

> FROM PF1 A
> > RIGHT JOIN PF2 B
> > > ON A.function\_id = B.function\_id
> > > > AND (A.boundary\_id - 1 = B.boundary\_id
> > > > > OR(A.boundary\_id IS NULL AND B.boundary\_id IS NULL))

> > INNER JOIN(
> > > SELECT
> > > > function\_id,
> > > > LessThan = CASE
> > > > > WHEN boundary\_value\_on\_right = 0 THEN '<='
> > > > > ELSE '<' END,

> > > > MoreThan = CASE
> > > > > WHEN boundary\_value\_on\_right = 0 THEN '>'
> > > > > ELSE '>=' END

> > > FROM sys.partition\_functions

> > )PF
> > > ON B.function\_id = PF.function\_id
),
PS AS(

> SELECT
> > DDS.partition\_scheme\_id, DDS.destination\_id,
> > FileGroupName = FG.name, IsReadOnly = FG.is\_read\_only

> FROM sys.destination\_data\_spaces DDS
> > INNER JOIN sys.filegroups FG
> > > ON DDS.data\_space\_id = FG.data\_space\_id
),
PINFO AS(

> SELECT
> > RowID = ROW\_NUMBER() OVER(ORDER BY SchemaID, ObjectID, PS.destination\_id),
> > TB.SchemaName, TB.TableName,
> > TB.PartitionScheme, PS.destination\_id, PS.FileGroupName, PS.IsReadOnly,
> > TB.PartitionFunction, TB.PartitionFunctionRangeType, TB.PartitionFunctionFanout,
> > PF.boundary\_id, PF.Type, PF.value

> FROM TBINFO TB
> > INNER JOIN PS
> > > ON TB.PartitionSchemeID = PS.partition\_scheme\_id

> > LEFT JOIN PF
> > > ON TB.PartitionFunctionID = PF.function\_id
> > > > AND PS.destination\_id = PF.boundary\_id
)
SELECT

> RowID,
> SchemaName = CASE destination\_id
> > WHEN 1 THEN SchemaName
> > ELSE N'' END,

> TableName = CASE destination\_id
> > WHEN 1 THEN TableName
> > ELSE N'' END,

> PartitionScheme = CASE destination\_id
> > WHEN 1 THEN PartitionScheme
> > ELSE N'' END,

> destination\_id, FileGroupName, IsReadOnly,
> PartitionFunction = CASE destination\_id
> > WHEN 1 THEN PartitionFunction
> > ELSE N'' END,

> PartitionFunctionRangeType = CASE destination\_id
> > WHEN 1 THEN PartitionFunctionRangeType
> > ELSE N'' END,

> PartitionFunctionFanout = CASE destination\_id
> > WHEN 1 THEN CONVERT(varchar(20), PartitionFunctionFanout)
> > ELSE N'' END,

> boundary\_id = ISNULL(CONVERT(varchar(20), boundary\_id), ''),
> Type = ISNULL(Type, N''),
> value = CASE PartitionFunctionFanout
> > WHEN 1 THEN '<ALL Data>'
> > ELSE ISNULL(value, N'<NEXT USED>') END
FROM PINFO
ORDER BY RowID**




--＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
--drop database dbPartitionTest
--测试数据库
create database dbPartitionTest
go
use
dbPartitionTest
go
--增加分组
alter database dbPartitionTest ADD FILEGROUP P200801
alter database dbPartitionTest ADD FILEGROUP P200802
alter database dbPartitionTest ADD FILEGROUP P200803
go
--分区函数
CREATE PARTITION FUNCTION part\_Year(datetime)
AS RANGE LEFT FOR VALUES
(
’20080131 23:59:59.997’,’20080229 23:59:59.997’,’20080331 23:59:59.997’
)
go
--增加文件组
ALTER DATABASE dbPartitionTest ADD FILE (NAME = N’P200801’,FILENAME = N’c:tb\_P200801.ndf’,SIZE = 1MB,MAXSIZE = 500MB,FILEGROWTH = 1MB)TO FILEGROUP P200801
ALTER DATABASE dbPartitionTest ADD FILE (NAME = N’P200802’,FILENAME = N’c:tb\_P200802.ndf’,SIZE = 1MB,MAXSIZE = 500MB,FILEGROWTH = 1MB)TO FILEGROUP P200802
ALTER DATABASE dbPartitionTest ADD FILE (NAME = N’P200803’,FILENAME = N’c:tb\_P200803.ndf’,SIZE = 1MB,MAXSIZE = 500MB,FILEGROWTH = 1MB)TO FILEGROUP P200803
go
--分区架构
CREATE PARTITION SCHEME part\_YearScheme　AS PARTITION part\_Year　 TO (P200801,P200802,P200803,[PRIMARY](PRIMARY.md))
go
CREATE TABLE [dbo](dbo.md).t\_part
(name varchar(100) default newid(),date datetime NOT NULL)
ON part\_YearScheme (date)
go
--添加测试数据,每天1条
declare @date datetime
set @date=’2007-12-31’
while @date<=’2008-04-0’
1 begin
insert into t\_part(date)values(@date)
set @date=@date+1
end
go
--查询数据分布在哪些分区
select $partition.part\_Year(date) as 分区编号,