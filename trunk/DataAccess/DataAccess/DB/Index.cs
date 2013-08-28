using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class Index : DataBaseAbstractObject
    {
        public string Tab_Name{get;set;}
        public string Index_Name{get;set;}
        public string Co_Names{get;set;}
        public bool Is_Primary_Key{get;set;}
        public bool Is_Unique { get; set; }
        public bool Is_Disabled { get; set; }

        public string ClusterStr { get; set; }
    }
}
