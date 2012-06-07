using System;
using System.ComponentModel;

namespace Business
{
    [Description("IsTable")]
    public class Test
    {
        [Description("IsPrimaryKey")]
        public string ID { set; get; }
        public string UserName { set; get; }
    }
}