﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlideWindowDemo
{
    public class SqlTexts
    {
        public static string RemoveDB(InitSetting setting)
        {
            return string.Format(@"use master 
if  exists (select name from sys.databases where name = N'{0}')


declare @dbname sysname 
set @dbname='{0}' --这个是要删除的数据库库名 


declare @s nvarchar(1000) 
declare tb cursor local for 
select s='kill '+cast(spid as varchar) 
from master..sysprocesses 
where dbid=db_id(@dbname) 


open tb 
fetch next from tb into @s 
while @@fetch_status=0 
begin 
exec(@s) 
fetch next from tb into @s 
end 
close tb 
deallocate tb 
exec('drop database ['+@dbname+']')  
", setting.DBName, setting.DBPath);
        }
        public static string CreateDB(InitSetting setting)
        {
            string sql = string.Format(@"create database {0} on
primary (name='{0}Primary', filename='{1}{0}Primary.mdf',size=5,maxsize=500,filegrowth=1 ),", setting.DBName, setting.DBPath);
            List<string> li = new List<string>();
            for (int i = 1; i < setting.PartitionCount + 1; i++)
            {
                li.Add(@"
filegroup {0}FG{1}(name = '{0}FG{1}',filename ='{2}{0}FG{1}.ndf',size = 5,maxsize=500,filegrowth=1)".FormatWith(setting.DBName, i, setting.DBPath));
            }
            sql += string.Join(",", li);
            return sql;
        }
        public static string RemovePartitionFunc(InitSetting setting)
        {
            return string.Format(@"use {0}
if  exists (select name from sys.databases where name = N'{0}_Partition_Range')
drop partition function {0}_Partition_Range", setting.DBName, setting.DBPath);
        }
        public static string CreatePartitionFunc(InitSetting setting)
        {
            string sql = @"create partition function {0}_Partition_Range(datetime) 
as range left for values ".FormatWith(setting.DBName);
            List<string> li = new List<string>();
            for (int i = 0; i < setting.PartitionCount - 1; i++)
            {
                switch (setting.IntervalType)
                {
                    case EnumIntervalType.Month:
                        li.Add(setting.StartTime.AddMonths(setting.Interval * i).ToString("yyyy-MM-dd hh:mm:ss"));
                        break;
                    case EnumIntervalType.Day:
                        li.Add(setting.StartTime.AddDays(setting.Interval * i).ToString("yyyy-MM-dd hh:mm:ss"));
                        break;
                    case EnumIntervalType.Hour:
                        li.Add(setting.StartTime.AddHours(setting.Interval * i).ToString("yyyy-MM-dd hh:mm:ss"));
                        break;
                    case EnumIntervalType.Minute:
                        li.Add(setting.StartTime.AddMinutes(setting.Interval * i).ToString("yyyy-MM-dd hh:mm:ss"));
                        break;
                }
            }
            var q = from c in li
                    select @"
'{0}'".FormatWith(c);
            sql += "({0})".FormatWith(string.Join(",", q.ToArray()));
            return sql;
        }

        public static string CreatePartitionScheme(InitSetting setting)
        {
            string sql = @"create partition scheme {0}_Partition_Scheme
as partition [{0}_Partition_Range] TO ".FormatWith(setting.DBName);
            List<string> li = new List<string>();
            for (int i = 1; i < setting.PartitionCount + 1; i++)
            {
                li.Add("{0}FG{1}".FormatWith(setting.DBName, i));
            }
            sql += string.Format("({0})", string.Join(",", li));
            return sql;
        }

        public static string CreatePartitionTable(InitSetting setting, string tableName)
        {
            string sql = @"if  exists (select name from sys.objects where name = N'{0}')
drop table {0}
create table [dbo].{0}(
OrderID int  not null  identity(1,1),
OrderDate datetime not null,
ProductName nvarchar(20) not null,
ProductPrice decimal(18,2) not null,
ProductCount int not null
)  on [{1}_Partition_Scheme] (OrderDate); ".FormatWith(tableName, setting.DBName);
            return sql;
        }
        public static string CreatePartitionTableIndex(InitSetting setting, string tableName)
        {
            return @"create nonclustered index IX_{0} on {0}(OrderDate)".FormatWith(tableName);
        }


        public static string GetInfo(string table)
        {
            return @"select  ps.name partition_scheme,
p.partition_number, 
 ds2.name as filegroup, 
convert(varchar(19), isnull(v.value, ''), 120) as range_boundary, 
str(p.rows, 9) as rows
from sys.indexes i 
join sys.partition_schemes ps on i.data_space_id = ps.data_space_id 
join sys.destination_data_spaces dds
on ps.data_space_id = dds.partition_scheme_id 
join sys.data_spaces ds2 on dds.data_space_id = ds2.data_space_id 
join sys.partitions p on dds.destination_id = p.partition_number
and p.object_id = i.object_id and p.index_id = i.index_id 
join sys.partition_functions pf on ps.function_id = pf.function_id 
LEFT JOIN sys.Partition_Range_values v on pf.function_id = v.function_id
and v.boundary_id = p.partition_number - pf.boundary_value_on_right 
WHERE i.object_id = object_id('{0}')
and i.index_id in (0, 1) 
order by p.partition_number
".FormatWith(table);
        }
    }
}