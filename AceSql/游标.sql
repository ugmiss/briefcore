--想要这样的存储过程:
--A表有一个字段 title ,文本类型
--B表有一个字段 key ,文本类型

--现在给A表title字段中添加了五条记录
--1.中国有飞机
--2.我家养母鸡
--3.公鸡飞天
--4.小鸡乱跑
--5.炸鸡在肯德鸡里有卖

--而B表的key字段中加了三条记录
--1.飞机
--2.小鸡
--3.炸鸡

-------------------------
--要求: 检索出A表title字段包含了B表中的key所有的关键字的记录.
--预期结果是:
--中国有飞机
--小鸡乱跑
--炸鸡在肯德鸡里有卖

-- 用游标 
create table a
(
 title nvarchar(20)
)
create table b
(
 [key] nvarchar(20)
)
insert into a values('中国有飞机')
insert into a values('我家养母鸡')
insert into a values('公鸡飞天')
insert into a values('小鸡乱跑 ')
insert into a values('炸鸡在肯德鸡里有卖 ')

insert into b values('飞机')
insert into b values('小鸡')
insert into b values('炸鸡')

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