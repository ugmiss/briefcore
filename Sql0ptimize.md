  * 开启IO统计，在查询分析器中观察扫描和读取的次数
set statistics io on
  * 查看表的索引碎片
select index\_type\_desc,alloc\_unit\_type\_desc,avg\_fragmentation\_in\_percent,fragment\_count,avg\_fragment\_size\_in\_pages,page\_count,record\_count,avg\_page\_space\_used\_in\_percent
FROM sys.dm\_db\_index\_physical\_stats(DB\_ID(''),OBJECT\_ID('table1'),NULL,NULL,'Sampled')

avg\_fragmentation\_in\_percent超过40%需要索引重建，减少IO
  * 索引重建:
alter index IX\_table1 on table1 rebuild
  * 更新这些统计信息，当随着表数据量的增大，当执行计划中估计行数和实际表的行数有出入时使用:
update statistics 表名