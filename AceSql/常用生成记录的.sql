--生成单条客户数据
--insert into Customer values(newid(),'Cst'+left(newid(),3))
--select * from Customer

--生成单条员工数据
--insert into Employee values(newid(),'Emp'+left(newid(),3))
--select * from Employee

--生成单条合同数据
declare @a uniqueidentifier
declare @b uniqueidentifier
select top 1 @a=EmployeeId from Employee order by newid()
select top 1 @b=CustomerId from Customer order by newid()
insert [Contract] values(newid(),'Crt'+left(newid(),3),rand()*100000,@a,@b,getdate())


select * from [Contract]
select ContractName,ContractMoney,EmployeeName,CustomerName from [Contract] a,Customer b, Employee c
where a.ManagerId=c.EmployeeId and a.CustomerId=b.CustomerId

declare @i intset @i=1while @i<9999begin--循环开始insert into Employee values(newid(),              --id'Emp'+left(newid(),3) --name  假数据以Emp开头 后3位随即)--循环结束set @i=@i+1end