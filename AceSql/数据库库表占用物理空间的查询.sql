--������ʱ���ѯ������¼���Ϳռ�ռ�õ�����
create table #tb(���� sysname,��¼�� int ,�����ռ� varchar(10),ʹ�ÿռ� varchar(10) ,����ʹ�ÿռ� varchar(10),δ�ÿռ� varchar(10))
insert into #tb exec sp_MSForEachTable 'EXEC sp_spaceused ''?'''
select * from #tb
drop table #tb
go
--sp_spaceused
--sp_spaceused�õ��Ľ��������ϸ���ֲ᣺
--database_size  �����ݿ�ռ� 
--sp_helpdb     --251.75 MB
--reserved = data  + index_size + unused
--���ݿ�ʣ��ռ� = database_size - reserved = sp_helpdb�г���������ʣ��ռ�+log free 
--reserved��ָ�Ѿ����������Ŀռ䣬���а�������+���� + unused(��ָ�����˻�û��д�����ݵĿռ�)
----40 tb

--sql��ѯ�����������ļ�
select db.name,sf.name,sf.filename,sf.size from sys.sysaltfiles sf 
inner join sys.databases db on sf.dbid=db.database_id where db.database_id>4
and db.name <>'distribution' order by db.name
