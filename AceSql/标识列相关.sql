--��SQL Server��,  ������ʱ��Ҫ��������ݱ�֮��������Ӽ�¼ʱ����ʶ�����´�1��ʼ������ 
--����ֻ��Ҫ�ڲ����¼֮ǰ��ִ���������� 
DBCC CHECKIDENT ('����',  RESEED, 0) --��1��ʼ
DBCC CHECKIDENT ('����',  RESEED, 1) --��2��ʼ
DBCC CHECKIDENT ('����',  RESEED, 10) --��11��ʼ

--insert into �����Զ������id��select @@identity��
--���������������ִ��select   @@identity�Ϳɵõ��Զ����ɵ�id
--�����sql server �����select SCOPE_IDENTITY() as id
-- ��Ϊ@@identityȫ�ֵ�
--ͬ�໹��IDENT_CURRENT����table����
--IDENT_CURRENT ����Ϊ�κλỰ���κ��������е��ض���������ɵı�ʶֵ��IDENT_CURRENT ����������ͻỰ�����ƣ���������ָ���ı�IDENT_CURRENT ����Ϊ�κλỰ���������е��ض��������ɵ�ֵ��
-- @@IDENTITY ����Ϊ��ǰ�Ự�������������е��κα�������ɵı�ʶֵ��
-- SCOPE_IDENTITY ����Ϊ��ǰ�Ự�͵�ǰ�������е��κα�������ɵı�ʶֵ
--SCOPE_IDENTITY �� @@IDENTITY �����ڵ�ǰ�Ự�е��κα��������ɵ����һ����ʶֵ�����ǣ�SCOPE_IDENTITY ֻ���ز��뵽��ǰ�������е�ֵ��@@IDENTITY ���������ض���������

