using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlideWindowDemo
{
    public class SqlTexts
    {
        //删除数据库
        public static string RemoveDB(InitSetting setting)
        {
            return string.Format(@"use master 
if  exists (select name from sys.databases where name = N'{0}')
begin
drop database {0}
declare @dbname sysname 
end 
", setting.DBName, setting.DBPath);
        }
        //创建数据库
        public static string CreateDB(InitSetting setting)
        {
            string sql = string.Format(@"create database {0} on
primary (name='{0}Primary', filename='{1}{0}Primary.mdf',size=5,maxsize=500,filegrowth=1 ),", setting.DBName, setting.DBPath);
            List<string> li = new List<string>();
            for (int i = 1; i <= setting.PartitionCount + 1; i++)
            {
                li.Add(@"
filegroup {0}FG{1}(name = '{0}FG{1}',filename ='{2}{0}FG{1}.ndf',size = 5,maxsize=500,filegrowth=1)".FormatWith(setting.DBName, i, setting.DBPath));
            }
            sql += string.Join(",", li);
            return sql;
        }
        //追加文件组
        public static string AppendGroup(InitSetting setting)
        {
            List<string> li = new List<string>();
            for (int i = 1; i <= setting.PartitionCount + 1; i++)
            {
                li.Add(@"alter database {0}
add filegroup {0}FG{1}
alter database {0}
add file
(
 name={0}FG{1},
 filename='{2}{0}FG{1}.ndf',
 size=1,
 maxsize=5,
 filegrowth=1
)
to filegroup {0}FG{1}
".FormatWith(setting.DBName, i, setting.DBPath));
            }
            return string.Join("", li);
        }
        //移除分区函数
        public static string RemovePartitionFunc(InitSetting setting)
        {
            return string.Format(@"use {0}
if  exists (select name from sys.databases where name = N'{0}_Partition_Func')
drop partition function {0}_Partition_Func", setting.DBName, setting.DBPath);
        }
        //创建分区函数
        public static string CreatePartitionFunc(InitSetting setting)
        {
            string sql = @"create partition function {0}_Partition_Func(datetime) 
as range right for values ".FormatWith(setting.DBName);
            List<string> li = new List<string>();
            for (int i = 0; i <= setting.PartitionCount - 1; i++)
            {
                switch (setting.IntervalType)
                {
                    case EnumIntervalType.Month:
                        li.Add(setting.StartTime.AddMonths(setting.Interval * i).ToString(SystemKeys.SqlDateTime));
                        break;
                    case EnumIntervalType.Day:
                        li.Add(setting.StartTime.AddDays(setting.Interval * i).ToString(SystemKeys.SqlDateTime));
                        break;
                    case EnumIntervalType.Hour:
                        li.Add(setting.StartTime.AddHours(setting.Interval * i).ToString(SystemKeys.SqlDateTime));
                        break;
                    case EnumIntervalType.Minute:
                        li.Add(setting.StartTime.AddMinutes(setting.Interval * i).ToString(SystemKeys.SqlDateTime));
                        break;
                    case EnumIntervalType.Second:
                        li.Add(setting.StartTime.AddSeconds(setting.Interval * i).ToString(SystemKeys.SqlDateTime));
                        break;
                }
            }
            var q = from c in li
                    select @"
'{0}'".FormatWith(c);
            sql += "({0})".FormatWith(string.Join(",", q.ToArray()));
            return sql;
        }
        //创建分区方案
        public static string CreatePartitionScheme(InitSetting setting)
        {
            string sql = @"create partition scheme {0}_Partition_Scheme
as partition [{0}_Partition_Func] TO ".FormatWith(setting.DBName);
            List<string> li = new List<string>();
            for (int i = 1; i <= setting.PartitionCount + 1; i++)
            {
                li.Add("{0}FG{1}".FormatWith(setting.DBName, i));
            }
            sql += string.Format("({0})", string.Join(",", li));
            return sql;
        }
        //创建分区表
        public static string CreatePartitionTable(InitSetting setting, bool IsBak)
        {
            if (IsBak)
                return setting.TScript.FormatWith(setting.TNameBak, setting.DBName, setting.ColName);
            return setting.TScript.FormatWith(setting.TName, setting.DBName, setting.ColName); ;
        }
        //创建分区索引
        public static string CreatePartitionTableIndex(InitSetting setting, bool IsBak)
        {
            if (IsBak)
                return @"create nonclustered index IX_{0} on {0}({1})".FormatWith(setting.TNameBak, setting.ColName);
            return @"create nonclustered index IX_{0} on {0}({1})".FormatWith(setting.TName, setting.ColName);
        }

        //获取分区统计信息 
        public static string GetInfo(InitSetting setting)
        {//ps.name partition_scheme,
            return @"select  
p.partition_number No, 
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
".FormatWith(setting.TName);
        }
        //切换1号文件组
        public static string Switch1(string table, string table2)
        {
            return "alter table  {0} switch partition 1 to {1} partition 1".FormatWith(table, table2);
        }
        //切换2号文件组
        public static string Switch2(string table, string table2)
        {
            return "alter table  {0} switch partition 2 to {1} partition 2".FormatWith(table, table2);
        }
        //指定下次使用的文件组
        public static string NextUsed(InitSetting setting, string FGname)
        {
            return "alter partition scheme {0}_Partition_Scheme next used {1}".FormatWith(setting.DBName, FGname);
        }
        //拆分
        public static string Split(InitSetting setting, DateTime endtime)
        {
            switch (setting.IntervalType)
            {
                case EnumIntervalType.Month:
                    endtime = endtime.AddMonths(setting.Interval);
                    break;
                case EnumIntervalType.Day:
                    endtime = endtime.AddDays(setting.Interval);
                    break;
                case EnumIntervalType.Hour:
                    endtime = endtime.AddHours(setting.Interval);
                    break;
                case EnumIntervalType.Minute:
                    endtime = endtime.AddMinutes(setting.Interval);
                    break;
                case EnumIntervalType.Second:
                    endtime = endtime.AddSeconds(setting.Interval);
                    break;
            }
            return "alter partition function {0}_Partition_Func()split range ('{1}')".FormatWith(setting.DBName, endtime.ToString(SystemKeys.SqlDateTime));
        }
        //合并
        public static string Merge(InitSetting setting, DateTime starttime)
        {
            return "alter partition function {0}_Partition_Func()merge range ('{1}')".FormatWith(setting.DBName, starttime.ToString(SystemKeys.SqlDateTime));
        }
        //取表信息
        public static string GetTableInfo(string tablename)
        {//ps.name partition_scheme,
            return @"select isnull(identity_columns.is_identity ,0) is_identity,
                 isnull(pk.is_primary_key,0) pk,Col.is_nullable is_nullable,
                 (CASE
                 WHEN Type.name = 'uniqueidentifier' THEN 'string'
                 WHEN Type.name = 'char' THEN 'string'
                 WHEN Type.name = 'nchar' THEN 'string'
                 WHEN Type.name = 'varchar' THEN 'string'
                 WHEN Type.name = 'nvarchar' THEN 'string'
                 WHEN Type.name = 'text' THEN 'string'
                 WHEN Type.name = 'ntext' THEN 'string'
                 WHEN Type.name = 'xml' THEN 'string'
                 WHEN Type.name = 'image' THEN 'byte[]'
                 WHEN Type.name = 'timestamp' THEN 'byte[]'
                 WHEN Type.name = 'binary' THEN 'byte[]'
                 WHEN Type.name = 'varbinary' THEN 'byte[]'
                 WHEN Type.name = 'tinyint' THEN 'byte'
                 WHEN Type.name = 'int' THEN 'int'
                 WHEN Type.name = 'smallint' THEN 'short'
                 WHEN Type.name = 'bigint' THEN 'long'
                 WHEN Type.name = 'float' THEN 'double'
                 WHEN Type.name = 'real' THEN 'float'
                 WHEN Type.name = 'money' THEN 'decimal'
                 WHEN Type.name = 'smallmoney' THEN 'decimal'
                 WHEN Type.name = 'decimal' THEN 'decimal'
                 WHEN Type.name = 'numeric' THEN 'decimal'
                 WHEN Type.name = 'time' THEN 'DateTime'
                 WHEN Type.name = 'datetime' THEN 'DateTime'
                 WHEN Type.name = 'smalldatetime' THEN 'DateTime'
                 WHEN Type.name = 'bit' THEN 'bool'
                 WHEN Type.name = 'sql_variant' THEN 'object'
                 ELSE Type.name
                 END) [type], 
                 STUFF(Col.Name,1,1,UPPER(SUBSTRING(Col.Name,1,1))) [propname] 
                 from sys.objects Tab inner join sys.columns Col on Tab.object_id =Col.object_id
                 inner join sys.types Type on Col.system_type_id = Type.system_type_id
                 left join sys.identity_columns identity_columns on  Tab.object_id = identity_columns.object_id and Col.column_id = identity_columns.column_id
                 left join(
                 select index_columns.object_id,index_columns.column_id,indexes.is_primary_key 
                 from sys.indexes  indexes inner join sys.index_columns index_columns 
                 on indexes.object_id = index_columns.object_id and indexes.index_id = index_columns.index_id
                 where indexes.is_primary_key = 1
               ) PK on Tab.object_id = PK.object_id AND Col.column_id = PK.column_id
               where Type.Name <> 'sysname' and (Tab.type = 'U' or Tab.type='V')  and Tab.Name<>'sysdiagrams' and Tab.Name='" + tablename + "'  order by Col.object_id";
        }
        //取表索引信息
        public static string GetTableIndexInfo(string tablename)
        {
            return @"select    indexs.Tab_Name  as Tab_Name,indexs.Index_Name as Index_Name ,indexs.[Co_Names] as Co_Names,
        Ind_Attribute.is_primary_key as is_primary_key,Ind_Attribute.is_unique AS is_unique,
        Ind_Attribute.is_disabled AS is_disabled,Ind_Attribute.type_desc cluster,Co_Names
 from (
    select Tab_Name,Index_Name, [Co_Names]=stuff((select ','+[Co_Name] from 
    (    select tab.Name as Tab_Name,ind.Name as Index_Name,Col.Name as Co_Name from sys.indexes ind 
        inner join sys.tables tab on ind.Object_id = tab.object_id and ind.type in (1,2)/*索引的类型：0=堆/1=聚集/2=非聚集/3=XML*/
        inner join sys.index_columns index_columns on tab.object_id = index_columns.object_id and ind.index_id = index_columns.index_id
        inner join sys.columns Col on tab.object_id = Col.object_id and index_columns.column_id = Col.column_id
    ) t where Tab_Name=tb.Tab_Name and Index_Name=tb.Index_Name  for xml path('')), 1, 1, '')
    from (
        select tab.Name as Tab_Name,ind.Name as Index_Name,Col.Name as Co_Name from sys.indexes ind 
        inner join sys.tables tab on ind.Object_id = tab.object_id and ind.type in (1,2)/*索引的类型：0=堆/1=聚集/2=非聚集/3=XML*/
        inner join sys.index_columns index_columns on tab.object_id = index_columns.object_id and ind.index_id = index_columns.index_id
        inner join sys.columns Col on tab.object_id = Col.object_id and index_columns.column_id = Col.column_id
    )tb
    where Tab_Name not like 'sys%'
    group by Tab_Name,Index_Name
) indexs inner join sys.indexes  Ind_Attribute on indexs.Index_Name = Ind_Attribute.name
where Tab_Name='{0}'
".FormatWith(tablename);
        }
        //更新主键
        public static string UpdatePK(string tablename, string pkname, string cols)
        {
            return @"alter table {0} drop constraint {1}
alter table {0} add  constraint {1} primary key nonclustered 
(
	{2} asc
)with (pad_index  = off, statistics_norecompute  = off, sort_in_tempdb = off, ignore_dup_key = off, online = off, allow_row_locks  = on, allow_page_locks  = on) on [primary]
".FormatWith(tablename, pkname, cols);
        }
        //更新索引并设置分区
        public static string UpdateIX(string tablename, string pkname, string cols,
            string DbName, string col)
        {
            return @"
CREATE CLUSTERED INDEX {0} ON {1} 
(
	{2} ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = ON, 
ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON {3}_Partition_Scheme({4})"
.FormatWith(pkname, tablename, cols, DbName, col);
        }
    }
}
