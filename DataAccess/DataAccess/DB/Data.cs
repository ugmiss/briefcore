using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace DataAccess
{
    public class Data
    {
        public static DataSet ds = new DataSet();

        public Data()
        {
        }

        public DataTable Get(string tablename)
        {
            foreach (DataTable dt in ds.Tables)
            {
                if(dt.TableName==tablename)
                     return dt;
            }
            return null;
        }

        public void Save(string tablename, DataTable datatable, string dirpath)
        {
            datatable.TableName = tablename;
            string datastr=Serializer.XmlSerialize<DataTable>(datatable);
            using (StreamWriter stream = new StreamWriter(dirpath + tablename + ".data.xml", false, Encoding.UTF8))
            {
                stream.WriteLine(datastr);
            }
        }

        public void Load(string dirpath,params string[]tablenames)
        {
            ds.Clear();
            foreach (string tablename in tablenames) 
            {
                using (StreamReader stream = new StreamReader(dirpath + tablename + ".data.xml",Encoding.UTF8))
                {
                    string tablestr= stream.ReadToEnd();
                   ds.Tables.Add(Serializer.XmlDeserialize<DataTable>(tablestr));
                }
            }
        }
    }
}
