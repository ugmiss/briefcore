--在SQL Server中,  我们有时需要在清空数据表之后，重新添加记录时，标识列重新从1开始计数。 
--我们只需要在插入记录之前，执行下面的命令： 
DBCC CHECKIDENT ('表名',  RESEED, 0) --从1开始
DBCC CHECKIDENT ('表名',  RESEED, 1) --从2开始
DBCC CHECKIDENT ('表名',  RESEED, 10) --从11开始

--insert into 后获得自动插入的id（select @@identity）
--当运行完插入语句后，执行select   @@identity就可得到自动生成的id
--如果是sql server 最好用select SCOPE_IDENTITY() as id
-- 因为@@identity全局的
--同类还有IDENT_CURRENT（‘table’）
--IDENT_CURRENT 返回为任何会话和任何作用域中的特定表最后生成的标识值。IDENT_CURRENT 不受作用域和会话的限制，而受限于指定的表。IDENT_CURRENT 返回为任何会话和作用域中的特定表所生成的值。
-- @@IDENTITY 返回为当前会话的所有作用域中的任何表最后生成的标识值。
-- SCOPE_IDENTITY 返回为当前会话和当前作用域中的任何表最后生成的标识值
--SCOPE_IDENTITY 和 @@IDENTITY 返回在当前会话中的任何表内所生成的最后一个标识值。但是，SCOPE_IDENTITY 只返回插入到当前作用域中的值；@@IDENTITY 不受限于特定的作用域。

