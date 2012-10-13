using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthDemo
{
    public class Data
    {
        [Auth]
        public string GetData()
        {
            return "none";
        }
    }
}
