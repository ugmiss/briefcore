--��������б��ͱ������
select o.name,i.rows from sysobjects o,sysindexes i 
where o.id=i.id and o.Xtype='U' and i.indid<2
order by i.rows desc
--������б����������
--EXECUTE sp_msforeachtable 'truncate table ?'


