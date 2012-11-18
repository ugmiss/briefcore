using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MyCutter
{
    public class ActionDetail
    {
        [XmlAttribute]
        public string ID { get; set; }
        [XmlAttribute]
        public string ActionType { get; set; }
        [XmlAttribute]
        public int TagX { get; set; }
        [XmlAttribute]
        public int TagY { get; set; }
        [XmlAttribute]
        public int Speed { get; set; }
        [XmlAttribute]
        public int Angle { get; set; }
        [XmlAttribute]
        public bool Replay { get; set; }
        [XmlAttribute]
        public string ActionResoure { get; set; }
    }
}
