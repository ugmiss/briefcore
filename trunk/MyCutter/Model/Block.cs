using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MyCutter
{
    /// <summary>
    /// 块。
    /// </summary>
    public class Block
    {
        /// <summary>
        /// 块类型。
        /// </summary>
        [XmlAttribute]
        public string BlockType { get; set; }
        /// <summary>
        /// 块资源。
        /// </summary>
        [XmlAttribute]
        public string BlockResource { get; set; }
        [XmlAttribute]
        public int X { get; set; }
        [XmlAttribute]
        public int Y { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        /// <summary>
        /// 定义动作集合。
        /// </summary>
        public List<ActionDetail> Actions { get; set; }
    }
    public enum EnumBlockType
    {
        //图片
        Image,
        //文字
        Word,
        //视频
        Video
    }

}
