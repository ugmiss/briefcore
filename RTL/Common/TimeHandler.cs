using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class TimeHandler
    {
        public static int ConvertDateTimeToInt(DateTime time)
        {
            int intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            intResult = (int)(time - startTime).TotalSeconds;
            return intResult;
        }
        public static DateTime ConvertIntToDatetime(int utc)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            startTime = startTime.AddSeconds(utc);
            //startTime = startTime.AddHours(8);//转化为北京时间(北京时间=UTC时间+8小时 )
            return startTime;
        }

    }
}
