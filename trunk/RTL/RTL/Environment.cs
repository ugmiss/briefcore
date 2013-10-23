using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTL
{
    public class Environment
    {
        public static Dictionary<string, string> ExcelDic = new Dictionary<string, string>();
        static Environment()
        {
            ExcelDic.Add("PersonName", "A");
            ExcelDic.Add("CardNo", "B");
            ExcelDic.Add("PassWord", "C");
        }
    }
}
