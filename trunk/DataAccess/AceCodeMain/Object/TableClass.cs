using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceCodeMain.Object
{
    public class TableClass
    {
        public string Classtext { get; set; }
        public string ClassnameBig { get; set; }
        public string ClassnameSmall { get; set; }
        public string PkeySmall { get; set; }
        public string UserTable { get; set; }
        public string Uid { get; set; }
        public string Upass { get; set; }
        public List<string> Proplisttext { get; set; }
        public List<string> ProplistBig { get; set; }
        public List<string> ProplistSmall { get; set; }
        public List<string> Proplist { get; set; }
        public List<string> ProplistType { get; set; }
        public List<string> ProplistConvert { get; set; }

        public TableClass()
        {
            Proplisttext = new List<string>();
            ProplistBig = new List<string>();
            ProplistSmall = new List<string>();
            Proplist = new List<string>();
            ProplistType = new List<string>();
            ProplistConvert = new List<string>();
        }
    }
}
