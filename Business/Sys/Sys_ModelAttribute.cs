using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Sys_ModelAttribute
    {
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        public string ModelId { set; get; }
        public int? AttrIndex { set; get; }
        public bool? AllowNull { set; get; }
        public bool? IsPk { set; get; }
        public string AttrName { set; get; }
        public string AttrTitle { set; get; }
        public string AttrType { set; get; }
        public string AttrLenth { set; get; }
        public string AttrControl { set; get; }
    }
}