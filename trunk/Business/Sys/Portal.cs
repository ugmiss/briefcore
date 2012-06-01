using System;
using System.ComponentModel;

namespace Business
{
    public class Portal
    {
        [Description("IsPrimaryKey")]

        public string Id { get; set; }
        public string Parentid { get; set; }
        public int Sortindex { get; set; }
        public string Itemtext { get; set; }
        public string Itemurl { get; set; }
        public string Imagepath { get; set; }
    }
}
