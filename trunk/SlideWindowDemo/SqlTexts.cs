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
            return string.Format(@"
            use master
            IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}')
            DROP DATABASE {0} 
            ", setting.DBName, setting.DBPath);
        }
        public static string CreateDB(InitSetting setting)
        {
            string sql = string.Format(@"
            CREATE DATABASE {0} ON
            PRIMARY (NAME='{0}Primary', FILENAME='{1}{0}Primary.mdf',SIZE=5,MAXSIZE=500,FILEGROWTH=1 ),
            ", setting.DBName, setting.DBPath);
            List<string> li = new List<string>();
            for (int i = 1; i < setting.PartitionCount + 1; i++)
            {
                li.Add("FILEGROUP [{0}FG{1}](NAME = '{0}FG{1}',FILENAME ='{2}{0}FG{1}.ndf',SIZE = 5MB,MAXSIZE=500,FILEGROWTH=1)".FormatWith(setting.DBName, i, setting.DBPath));
            }
            sql += string.Join(",", li);
            return sql;
        }
        public static string CreatePartitionFunc(InitSetting setting)
        {
            string sql = @"CREATE PARTITION FUNCTION [{0}_Partition_Range](DATETIME) AS RANGE left FOR VALUES ".FormatWith(setting.DBName);
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
            var q = from c in li select "'{0}'".FormatWith(c);
            sql += "({0})".FormatWith(string.Join(",", q.ToArray()));
            return sql;
        }
    }
}
