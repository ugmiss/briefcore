using System;
using System.ComponentModel;

namespace Business
{
    public class Product
    {
        [Description("IsPrimaryKey")]
        public string Id { get; set; }
        public string Productname { get; set; }
    }
}
