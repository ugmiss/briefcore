using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Orders
    {
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        public string Userid { set; get; }
        public DateTime? Orderdate { set; get; }
    }
}