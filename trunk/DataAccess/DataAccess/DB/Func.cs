using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class Func : DataBaseAbstractObject
    {
        public string FuncName { get; set; }
    }
}
