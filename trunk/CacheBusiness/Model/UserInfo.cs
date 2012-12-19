using System;
using System.ComponentModel;
using System.Runtime.Serialization;
/* Code Generate Time 2012/10/23 9:38:15*/
namespace  CacheBusiness.Model
{
    [Description("IsTable")]
    [DataContract]
    public class UserInfo
    {
        [Description("DataField,IsPrimaryKey")]
        [DataMember]
        public string ID { set; get; }
        [Description("DataField")]
        [DataMember]
        public string Name { set; get; }
        [Description("DataField")]
        [DataMember]
        public int  Age { set; get; }
    }
}