using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MyCutter
{
    /// <summary>
    /// 页。
    /// </summary>
    public class Page
    {
        [XmlAttribute]
        public int Index { get; set; }
        /// <summary>
        /// 页名称。
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 背景图
        /// </summary>
        [XmlAttribute]
        public string BackgroundImage { get; set; }
        /// <summary>
        /// 块信息集合。
        /// </summary>
        public List<Block> Blocks { get; set; }
    }
}
