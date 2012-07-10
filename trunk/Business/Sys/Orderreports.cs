using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsView")]

    public class Orderreports
    {
        public string Id { set; get; }
        public DateTime? Orderdate { set; get; }
        public decimal? Cost { set; get; }
        public string Productname { set; get; }
        public string Name { set; get; }
    }
}