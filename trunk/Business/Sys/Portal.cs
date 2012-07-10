using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Portal
    {
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        public string Parentid { set; get; }
        public int? Sortindex { set; get; }
        public string Itemtext { set; get; }
        public string Itemurl { set; get; }
        public string Imagepath { set; get; }
    }
}