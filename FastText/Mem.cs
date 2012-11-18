using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FastText
{
    public class Mem
    {
        public string Id { get; set; }
        public string Assort { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public byte[] Files { get; set; }
        public bool IsCommon { get; set; }
        public DateTime LastAccessDate { get; set; }
        public Mem() { }
        public Mem(DataRow dr)
        {
            if (!(dr["id"] is DBNull))
                Id = dr["id"].ToString();
            if (!(dr["Assort"] is DBNull))
                Assort = dr["Assort"].ToString();
            if (!(dr["Title"] is DBNull))
                Title = dr["Title"].ToString();
            try
            {
                if (!(dr["Context"] is DBNull))
                    Context = dr["Context"].ToString();
            }
            catch
            { 
            }
            if (!(dr["Type"] is DBNull))
                Type = dr["Type"].ToString();
            if (!(dr["Tag"] is DBNull))
                Tag = dr["Tag"].ToString();
            if (!(dr["IsCommon"] is DBNull))
                IsCommon = Convert.ToBoolean(dr["IsCommon"]);
            if (!(dr["lastaccessdate"] is DBNull))
                LastAccessDate = Convert.ToDateTime(dr["lastaccessdate"]);
        }
    }
    public class AssortInfo
    {
        public string AssortId { get; set; }
        public string AssortText { get; set; }
        public AssortInfo() { }
        public AssortInfo(DataRow dr)
        {
            if (!(dr["AssortId"] is DBNull))
                AssortId = dr["AssortId"].ToString();
            if (!(dr["AssortText"] is DBNull))
                AssortText = dr["AssortText"].ToString();
        }

    }
}
