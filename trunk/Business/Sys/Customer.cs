using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Customer
    {
        public decimal? Cprice { set; get; }
        public string Cremark { set; get; }
        public string Cheight { set; get; }
        public string Ctype { set; get; }
        [Description("IsPrimaryKey")]
        public string Cname { set; get; }
    }
}