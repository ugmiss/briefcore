using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Sys_Model
    {
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        public string TableName { set; get; }
        public string TableTitle { set; get; }
        public string UIXml { set; get; }
    }
}