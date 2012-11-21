using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PartitionAndSlideWindow
{
    public class InitSetting
    {
        public string ConnectionString
        {
            get
            {
                return "server={0};uid={1};pwd={2};database={3}".FormatWith(Server, UID, Pwd, DBName);
            }
        }
        public string MasterConnectionString
        {
            get
            {
                return "server={0};uid={1};pwd={2};database={3}".FormatWith(Server, UID, Pwd, "master");
            }
        }
        public string Server { get; set; }

        public string UID { get; set; }

        public string Pwd { get; set; }

        public string TName { get; set; }
        public string TNameBak
        {
            get
            {
                return TName + "Bak";
            }
        }

        public string ColName { get; set; }

        public string TScript { get; set; }

        public string DBName { get; set; }
        public string DBPath { get; set; }
        public int PartitionCount { get; set; }
        public DateTime StartTime { get; set; }
        public int Interval { get; set; }
        public EnumIntervalType IntervalType { get; set; }


    }
    public enum EnumIntervalType
    {
        Month, Day, Hour, Minute, Second
    }
}
