using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Quartz
{
    /// <summary>
    /// 仿Quartz的Cron表达式。
    /// </summary>
    public class CornManage
    {
        /// <summary>
        /// 验证Corn表达式的时间。5+1位，首位分
        /// </summary>
        /// <param name="cornRule"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool ValidateCornDateByMinute(string cornRule, DateTime datetime)
        {
            return ValidateCornDateBySecond("* " + cornRule, datetime);
        }
        /// <summary>
        /// 验证Corn表达式的时间。6+1位，首位秒
        /// </summary>
        /// <param name="cornRule"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool ValidateCornDateBySecond(string cornRule, DateTime datetime)
        {
            string[] arr = cornRule.Split(' ');
            //秒 0-59 , - * / 
            bool secRes = TestCommon(arr[0], datetime.Second);
            //分 0-59 , - * / 
            bool minuteRes = TestCommon(arr[1], datetime.Minute);
            //小时 0-23 , - * / 
            bool hourRes = TestCommon(arr[2], datetime.Hour);
            //月内日期 1-31 , - * ? / L W C 
            bool dateRes = TestDate(arr[3], datetime);
            //月 1-12 或者 JAN-DEC , - * / 
            bool MMRes = TestCommon(arr[4], datetime.Month);
            //周内日期 1-7 或者 SUN-SAT , - * ? / L C #  
            bool weekdayRes = TestWeekDay(arr[5], datetime);
            //年（可选） 留空, 1970-2099 , - * / 
            bool yyRes = TestCommon(arr.Length == 7 ? arr[6] : "*", datetime.Year);
            return secRes && minuteRes && hourRes && dateRes && MMRes && weekdayRes && yyRes;
        }

        static bool TestCommon(string rule, int datetimeValue)
        {
            if (rule == "*" || rule == "")
            {   // 完全匹配。
                return true;
            }
            if (rule.IndexOf("/") >= 0)
            {   // 间隔匹配。
                string[] arr = rule.Split('/');
                int start = int.Parse(arr[0]);
                int cycle = int.Parse(arr[1]);
                if (datetimeValue % cycle == start)
                    return true;
                else
                    return false;
            }
            if (rule.IndexOf("-") >= 0)
            {   // 范围匹配。
                string[] arr = rule.Split('-');
                int start = int.Parse(arr[0]);
                int end = int.Parse(arr[1]);
                if (start < end)
                {   // 12-54
                    if (datetimeValue >= start && datetimeValue <= end)
                        return true;
                    else
                        return false;
                }
                else
                {   // 54-12
                    if (datetimeValue >= start || datetimeValue <= end)
                        return true;
                    else
                        return false;
                }
            }
            // 枚举匹配。
            string[] nums = rule.Split(',');
            string[] ok = (from c in nums where int.Parse(c) == datetimeValue select c).ToArray();
            if (ok.Length != 0)
                return true;
            else
                return false;
        }

        static bool TestDate(string rule, DateTime datetime)
        {
            if (rule == "L")
            {   // 最后一天
                if (datetime.Day == DateTime.DaysInMonth(datetime.Year, datetime.Month))
                    return true;
                else
                    return false;
            }
            if (rule.IndexOf("L") > 0)
            {   // 倒数第N天  6L
                int x = int.Parse(rule.Replace("L", ""));
                if (datetime.Day + x == DateTime.DaysInMonth(datetime.Year, datetime.Month))
                    return true;
                else
                    return false;
            }
            if (rule == "*")
            {   // 完全匹配。
                return true;
            }
            if (rule.IndexOf("/") >= 0)
            {   // 间隔匹配。
                string[] arr = rule.Split('/');
                int start = int.Parse(arr[0]);
                int cycle = int.Parse(arr[1]);
                if (datetime.Day % cycle == start)
                    return true;
                else
                    return false;
            }
            if (rule.IndexOf("-") >= 0)
            {   // 范围匹配。
                string[] arr = rule.Split('-');
                int start = int.Parse(arr[0]);
                int end = int.Parse(arr[1]);
                if (start < end)
                {   // 12-54
                    if (datetime.Day >= start && datetime.Day <= end)
                        return true;
                    else
                        return false;
                }
                else
                {   // 54-12
                    if (datetime.Day >= start || datetime.Day <= end)
                        return true;
                    else
                        return false;
                }
            }
            // 枚举匹配。
            string[] nums = rule.Split(',');
            string[] ok = (from c in nums where int.Parse(c) == datetime.Day select c).ToArray();
            if (ok.Length != 0)
                return true;
            else
                return false;
        }

        static bool TestWeekDay(string rule, DateTime datetime)
        {
            if (rule == "*")
            {   // 完全匹配。
                return true;
            }
            if (rule.IndexOf("/") >= 0)
            {   // 间隔匹配。
                string[] arr = rule.Split('/');
                int start = int.Parse(arr[0]);
                int cycle = int.Parse(arr[1]);
                if ((int)datetime.DayOfWeek % cycle == start)
                    return true;
                else
                    return false;
            }
            if (rule.IndexOf("-") >= 0)
            {   // 范围匹配。
                string[] arr = rule.Split('-');
                int start = int.Parse(arr[0]);
                int end = int.Parse(arr[1]);
                if (start < end)
                {   // 12-54
                    if ((int)datetime.DayOfWeek >= start && (int)datetime.DayOfWeek <= end)
                        return true;
                    else
                        return false;
                }
                else
                {   // 54-12
                    if ((int)datetime.DayOfWeek >= start || (int)datetime.DayOfWeek <= end)
                        return true;
                    else
                        return false;
                }
            }
            // 枚举匹配。
            string[] nums = rule.Split(',');
            string[] ok = (from c in nums where int.Parse(c) == (int)datetime.DayOfWeek select c).ToArray();
            if (ok.Length != 0)
                return true;
            else
                return false;
        }
    }
}
