--���ɵ����ͻ�����
--insert into Customer values(newid(),'Cst'+left(newid(),3))
--select * from Customer

--���ɵ���Ա������
--insert into Employee values(newid(),'Emp'+left(newid(),3))
--select * from Employee

--���ɵ�����ͬ����
declare @a uniqueidentifier
declare @b uniqueidentifier
select top 1 @a=EmployeeId from Employee order by newid()
select top 1 @b=CustomerId from Customer order by newid()
insert [Contract] values(newid(),'Crt'+left(newid(),3),rand()*100000,@a,@b,getdate())


select * from [Contract]
select ContractName,ContractMoney,EmployeeName,CustomerName from [Contract] a,Customer b, Employee c
where a.ManagerId=c.EmployeeId and a.CustomerId=b.CustomerId

declare @i intset @i=1while @i<9999begin--ѭ����ʼinsert into Employee values(newid(),              --id'Emp'+left(newid(),3) --name  ��������Emp��ͷ ��3λ�漴)--ѭ������set @i=@i+1end