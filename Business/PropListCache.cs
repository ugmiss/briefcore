using System;
using System.Collections.Generic;

namespace Business
{
    public class PropListCache
    {
        public static List<Prop> Proplist = new List<Prop>();
        public static List<Prop> AllProplist = new List<Prop>();
        public PropListCache() { }
    }
    public class Prop
    {
        public string ID { get; set; }
        public string PropName { get; set; }
        public string PropField { get; set; }
        public string PropType { get; set; }
        public int PropCtrl { get; set; }
        public int PropHeight { get; set; }
        public Prop() { }
        public Prop(string Id, string name, string field, string ty, int ctrl, int height)
        {
            ID = Id;
            PropName = name;
            PropField = field;
            PropType = ty;
            PropCtrl = ctrl;
            PropHeight = height;
        }
    }
    public class Ctrl
    {
        public string ID { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public System.Drawing.Point p { get; set; }
        public System.Drawing.Size s { get; set; }
        public string tp { get; set; }
        public string txt { get; set; }
    }
    public class DiyForm
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<Ctrl> CtrlList { get; set; }
    }
}
