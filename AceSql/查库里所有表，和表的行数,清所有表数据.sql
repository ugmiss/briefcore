--查库里所有表，和表的行数
select o.name,i.rows from sysobjects o,sysindexes i 
where o.id=i.id and o.Xtype='U' and i.indid<2
order by i.rows desc
--清除所有表的所有数据
--EXECUTE sp_msforeachtable 'truncate table ?'


