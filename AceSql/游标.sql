--��Ҫ�����Ĵ洢����:
--A����һ���ֶ� title ,�ı�����
--B����һ���ֶ� key ,�ı�����

--���ڸ�A��title�ֶ��������������¼
--1.�й��зɻ�
--2.�Ҽ���ĸ��
--3.��������
--4.С������
--5.ը���ڿϵ¼�������

--��B���key�ֶ��м���������¼
--1.�ɻ�
--2.С��
--3.ը��

-------------------------
--Ҫ��: ������A��title�ֶΰ�����B���е�key���еĹؼ��ֵļ�¼.
--Ԥ�ڽ����:
--�й��зɻ�
--С������
--ը���ڿϵ¼�������

-- ���α� 
create table a
(
 title nvarchar(20)
)
create table b
(
 [key] nvarchar(20)
)
insert into a values('�й��зɻ�')
insert into a values('�Ҽ���ĸ��')
insert into a values('��������')
insert into a values('С������ ')
insert into a values('ը���ڿϵ¼������� ')

insert into b values('�ɻ�')
insert into b values('С��')
insert into b values('ը��')

DECLARE   cur1 cursor for select [key] from b
open cur1
DECLARE @k nvarchar(20)
DECLARE @t nvarchar(20)
fetch cur1 into @k
while(@@fetch_status=0)
begin 
  select @t=title from a where title like ('%'+@k+'%')
  print @t
fetch NEXT FROM  cur1 into @k
end
CLOSE cur1
DEALLOCATE cur1