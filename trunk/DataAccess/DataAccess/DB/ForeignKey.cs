using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    [Serializable]
    public class ForeignKey : DataBaseAbstractObject
    {
        public string FK_Name{get;set;}
        public string FK_Tab_Name{get;set;}
        public string FK_Col{get;set;}
        public string PK_Tab_Name{get;set;}
        public string PK_Col{get;set;}
        public bool Delete_Referential_Action { get; set; }
        public bool Update_Referential_Action { get; set; }
    }
}
