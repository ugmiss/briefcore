using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class Tab : DataBaseAbstractObject
    {
        public string TableName { get; set; }
        public List<Column> Columns;
        public Tab() {
            Columns = new List<Column>();
        }
    }
}
