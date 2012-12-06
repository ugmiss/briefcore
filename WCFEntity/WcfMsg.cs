using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WCFEntity
{
    public enum MsgType
    {
        Login,
        LoginOK,
        ReLogin,
        LogOff,
        System,
        SystemMsg,
        Command,
        AllChat,
        SingleChat,
    }
    [DataContract]
    public class WcfMsg
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public MsgType MsgType { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public Guid? FromID { get; set; }
        [DataMember]
        public Guid? ToID { get; set; }
        [DataMember]
        public DateTime MsgTime { get; set; }
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public string DataType { get; set; }
    }
}
