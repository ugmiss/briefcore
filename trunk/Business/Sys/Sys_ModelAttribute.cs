using System;
using System.ComponentModel;

namespace Business
{
    [Description("IsTable")]
    public class Sys_ModelAttribute
    {
        [Description("IsPrimaryKey")]
        public string Id { get; set; }
        public string ModelId { get; set; }
        public int AttrIndex { get; set; }
        public bool IsPk { get; set; }
        public bool AllowNull { get; set; }
        public string AttrName { get; set; }
        public string AttrTitle { get; set; }
        public string AttrType { get; set; }
        public string AttrLenth { get; set; }
        public string AttrControl { get; set; }
    }
}
