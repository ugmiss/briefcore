using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class View : DataBaseAbstractObject
    {
        public string ViewName { get; set; }
        public List<Column> Columns;
        public View()
        {
            Columns = new List<Column>();
        }
    }
}
