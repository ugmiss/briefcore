using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WCFEntity
{
    [DataContract]
    public class WcfStreamMsg
    {
        [DataMember]
        public string MsgName { get; set; }
        [DataMember]
        public byte[] Bytes { get; set; }
    }
}
