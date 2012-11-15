using System;
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
drop database {0}", setting.DBName, setting.DBPath);
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
            return string.Format(@"if  exists (select name from sys.databases where name = N'{0}_Partition_Range')
drop partition function {0}_Partition_Range", setting.DBName, setting.DBPath);
        }

        public static string CreatePartitionFunc(InitSetting setting)
        {
            string sql = @"create partition function {0}_Partition_Range(datetime) 
as range left for values ".FormatWith(setting.DBName);
            List<string> li = new List<string>();
            for (int i = 0; i < setting.PartitionCount; i++)
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
            var q = from c in li select @"
'{0}'".FormatWith(c);
            sql += "({0})".FormatWith(string.Join(",", q.ToArray()));
            return sql;
        }
    }
}
