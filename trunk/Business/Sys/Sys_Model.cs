using System;
using System.ComponentModel;

namespace Business
{
    [Description("IsTable")]
    public class Sys_Model
    {
        [Description("IsPrimaryKey")]
        public string Id { get; set; }
        public string TableName { get; set; }
        public string TableTitle { get; set; }
        public string UIXml { get; set; }
    }
}
