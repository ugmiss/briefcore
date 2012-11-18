using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class Column
    {
        public string TableName { get; set; }
        public string ColName { get; set; }
        public string TypeName { get; set; }
        public int Length { get; set; }
        public string DescValue { get; set; }
        public bool Is_Primary_Key { get; set; }
        public bool Is_Identity { get; set; }
    }
}
