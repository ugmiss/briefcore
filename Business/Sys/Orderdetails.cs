using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Orderdetails
    {
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        [Description("IsPrimaryKey")]
        public string Orderid { set; get; }
        public string Productid { set; get; }
        public decimal? Price { set; get; }
        public decimal? Count { set; get; }
    }
}