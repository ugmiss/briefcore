using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class Proc : DataBaseAbstractObject
    {
        public string ProcName { get; set; }
    }
}
