using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthDemo
{

    public interface IDataProvider
    {
        List<string> GetData();
    }

    
    public class DataProvider : IDataProvider
    {
        public List<string> GetData()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            var x = random.Next(100);
            if (x > 80)
                throw new Exception("数据访问异常");
            List<string> li = new List<string>();
            li.Add("eo:1000");
            li.Add("eo:2000");
            li.Add("eo:3000");
            li.Add("mo:5000");
            li.Add("mo:4000");
            return li;
        }
    }
}
