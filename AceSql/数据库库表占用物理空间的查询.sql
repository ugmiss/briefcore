--创建临时表查询【表】记录数和空间占用的数据
create table #tb(表名 sysname,记录数 int ,保留空间 varchar(10),使用空间 varchar(10) ,索引使用空间 varchar(10),未用空间 varchar(10))
insert into #tb exec sp_MSForEachTable 'EXEC sp_spaceused ''?'''
select * from #tb
drop table #tb
go
--sp_spaceused
--sp_spaceused得到的结果请你仔细看手册：
--database_size  总数据库空间 
--sp_helpdb     --251.75 MB
--reserved = data  + index_size + unused
--数据库剩余空间 = database_size - reserved = sp_helpdb列出来的数据剩余空间+log free 
--reserved是指已经保留出来的空间，其中包括数据+索引 + unused(是指申请了还没有写入数据的空间)
----40 tb

--sql查询库名及物理文件
select db.name,sf.name,sf.filename,sf.size from sys.sysaltfiles sf 
inner join sys.databases db on sf.dbid=db.database_id where db.database_id>4
and db.name <>'distribution' order by db.name
