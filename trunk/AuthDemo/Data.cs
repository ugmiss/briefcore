using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthDemo
{
    public class Data : IData
    {
        public List<string> GetData()
        {
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
