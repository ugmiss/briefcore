using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCutter
{
    /// <summary>
    /// 书。
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 书名。
        /// </summary>
        [XmlAttribute]
        public string BookName { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        [XmlAttribute]
        public string Background { get; set; }
        [XmlAttribute]
        public string[] PageInfoList { get; set; }
    }

    public class PageInfo
    {
        [XmlAttribute]
        public int Index { get; set; }
        [XmlAttribute]
        public string PageJsonFileName { get; set; }
    }
}
