--创建分区数据库，分区文件夹需要手动创建
use master
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'AceDataBase')
DROP DATABASE [AceDataBase] 
GO
CREATE DATABASE [AceDataBase] ON
        PRIMARY (NAME='Data Partition DB Primary FG', FILENAME='D:\AceData\Primary\AceDataBase Primary FG.mdf',SIZE=5,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG1](NAME = 'AceDataBase FG1',FILENAME ='D:\AceData\FG1\AceDataBase FG1.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG2](NAME = 'AceDataBase FG2',FILENAME ='D:\AceData\FG2\AceDataBase FG2.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG3](NAME = 'AceDataBase FG3',FILENAME ='D:\AceData\FG3\AceDataBase FG3.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG4](NAME = 'AceDataBase FG4',FILENAME ='D:\AceData\FG4\AceDataBase FG4.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG5](NAME = 'AceDataBase FG5',FILENAME ='D:\AceData\FG5\AceDataBase FG5.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG6](NAME = 'AceDataBase FG6',FILENAME ='D:\AceData\FG6\AceDataBase FG6.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG7](NAME = 'AceDataBase FG7',FILENAME ='D:\AceData\FG7\AceDataBase FG7.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG8](NAME = 'AceDataBase FG8',FILENAME ='D:\AceData\FG8\AceDataBase FG8.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG9](NAME = 'AceDataBase FG9',FILENAME ='D:\AceData\FG9\AceDataBase FG9.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG10](NAME = 'AceDataBase FG10',FILENAME ='D:\AceData\FG10\AceDataBase FG10.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG11](NAME = 'AceDataBase FG11',FILENAME ='D:\AceData\FG11\AceDataBase FG11.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 ),
FILEGROUP [AceDataBase FG12](NAME = 'AceDataBase FG12',FILENAME ='D:\AceData\FG12\AceDataBase FG12.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1 )
GO
--创建分区函数(11个拆分点切12个分区空间)
USE [AceDataBase]
GO
CREATE PARTITION FUNCTION [AceDataBase_Partition_Range](DATETIME)
AS RANGE left FOR VALUES 
('2012-09-01','2012-10-01','2012-11-01','2012-12-01','2013-01-01','2013-02-01',
 '2013-03-01','2013-04-01','2013-05-01','2013-06-01','2013-07-01');
GO
--创建分区架构
CREATE PARTITION SCHEME [AceDataBase_Partition_Scheme]
AS PARTITION [AceDataBase_Partition_Range]
TO ([AceDataBase FG1],[AceDataBase FG2],[AceDataBase FG3],[AceDataBase FG4],[AceDataBase FG5],[AceDataBase FG6],
    [AceDataBase FG7],[AceDataBase FG8],[AceDataBase FG9],[AceDataBase FG10],[AceDataBase FG11],[AceDataBase FG12]);
GO
--创建一个使用[AceDataBase_Partition_Scheme]架构的表 
CREATE TABLE [dbo].[FlightInfo](
[CityPair] [varchar](6)  NOT NULL,
[FlightNo] [varchar](10)  NULL,
[FlightDate] [datetime] NOT NULL,
[CacheTime] [datetime] NOT NULL   DEFAULT (getdate()),
[AVNote] [varchar](300)  NULL
)  ON [AceDataBase_Partition_Scheme] (FlightDate); --FlightDate被设置为分区参照列
GO
insert into [FlightInfo] Values('SH-XM','MU9878','2012-9-5',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('SH-BJ','SH2818','2012-10-15',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('SH-BJ','SH0808','2012-12-3',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('JZ-SH','CQ1177','2013-4-5',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('BJ-XM','DF6712','2012-9-7',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('JZ-XM','DF6712','2013-7-30',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('JZ-XM','DF6712','2013-7-30',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('JZ-XM','DF6712','2013-7-30',getdate(),'9:00起-10:55降')
insert into [FlightInfo] Values('JZ-XM','DF6712','2013-7-30',getdate(),'9:00起-10:55降')
--查看
--SELECT *, $PARTITION.[AceDataBase_Partition_Range](FlightDate) 数据所在分区号 FROM dbo.[FlightInfo]
----查看表数据在分区中的分布
--SELECT distinct ps.partition_number,ps.row_count
--FROM sys.dm_db_partition_stats ps
--INNER JOIN sys.partitions p
--ON ps.partition_id = p.partition_id
--AND p.[object_id] = OBJECT_ID('FlightInfo')
--创建目标表用于迁移分区数据
--CREATE TABLE [dbo].[FlightInfoBak](
--[CityPair] [varchar](6)  NOT NULL,
--[FlightNo] [varchar](10)  NULL,
--[FlightDate] [datetime] NOT NULL,
--[CacheTime] [datetime] NOT NULL   DEFAULT (getdate()),
--[AVNote] [varchar](300)  NULL
--)  ON [AceDataBase_Partition_Scheme] (FlightDate); --FlightDate被设置为分区参照列
--GO
--创建非聚集分区索引
--先设计一个已分区表，然后为该表创建索引。
--执行此操作时，SQL Server 将使用与该表相同的分区方案和分区依据列自动对索引进行分区。
--因此，索引的分区方式实质上与表的分区方式相同。这将使索引与表“对齐”。
--如果在创建时指定了不同的分区方案或单独的文件组来存储索引，则 SQL Server 不会将索引与表对齐。
--drop INDEX IX_FlightInfo_FlightDate ON FlightInfo 
--drop INDEX IX_FlightInfoBak_FlightDate ON FlightInfoBak
--CREATE NONCLUSTERED INDEX IX_FlightInfo_FlightDate ON FlightInfo(FlightDate)
--go 
--CREATE NONCLUSTERED INDEX IX_FlightInfoBak_FlightDate ON FlightInfoBak(FlightDate) 
--go
--迁移
--可迁移的条件：
--1.在 SWITCH 操作之前两个表必须都存在
--2.接收分区必须存在并且必须是空的
--3.不分区的接收表必须存在且必须是空的
--4.各分区必须依据同一列
--5.源表和目标表必须共享同一个文件组
--ALTER TABLE [FlightInfoBak] SWITCH partition 3 TO  [FlightInfo] partition 3
--ALTER TABLE [FlightInfo] SWITCH partition 3 TO  [FlightInfoBak] partition 3
--合并分区
ALTER PARTITION FUNCTION AceDataBase_Partition_Range()
MERGE RANGE ('2012-9-01');
--拆分分区前
--指定要由分区方案标记为 NEXT USED 的文件组。
--文件组将接受使用 ALTER PARTITION FUNCTION 语句创建的新分区
ALTER PARTITION SCHEME [AceDataBase_Partition_Scheme] 
NEXT USED [AceDataBase FG1] 
--拆分分区
ALTER PARTITION FUNCTION AceDataBase_Partition_Range() SPLIT RANGE ('2013-08-01');

--查看分区与文件组的对应关系
--select fg.name,sp.destination_id
--from sys.destination_data_spaces sp
--inner join sys.partition_schemes scm on sp.partition_scheme_id = scm.data_space_id
--inner join sys.filegroups fg on fg.data_space_id = sp.data_space_id
--where scm.name='AceDataBase_Partition_Scheme'


--综合查询
select  ps.name partition_scheme,
p.partition_number, 
 ds2.name as filegroup, 
convert(varchar(19), isnull(v.value, ''), 120) as range_boundary, 
str(p.rows, 9) as rows
from sys.indexes i 
join sys.partition_schemes ps on i.data_space_id = ps.data_space_id 
join sys.destination_data_spaces dds
on ps.data_space_id = dds.partition_scheme_id 
join sys.data_spaces ds2 on dds.data_space_id = ds2.data_space_id 
join sys.partitions p on dds.destination_id = p.partition_number
and p.object_id = i.object_id and p.index_id = i.index_id 
join sys.partition_functions pf on ps.function_id = pf.function_id 
LEFT JOIN sys.Partition_Range_values v on pf.function_id = v.function_id
and v.boundary_id = p.partition_number - pf.boundary_value_on_right 
WHERE i.object_id = object_id('FlightInfo')
and i.index_id in (0, 1) 
order by p.partition_number
--SlideWindow滑动窗口方案