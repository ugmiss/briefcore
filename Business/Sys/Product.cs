using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Product
    {
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        public string Productname { set; get; }
    }
}