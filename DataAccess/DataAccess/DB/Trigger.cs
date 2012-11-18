using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class Trigger:DataBaseAbstractObject
    {
        public string TriggerName { get; set; }
    }
}
