using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataTransfer
{
    public class Enviroment
    {
        public string TempDir { get; set; }
        public string GetTempDir()
        {
            using (StreamReader sr = new StreamReader("Enviroment.xml"))
            {
                Enviroment env = Utility.Serializer.XmlDeserialize<Enviroment>(sr.ReadToEnd());
                return env.TempDir;
            }
        }
        public void SaveTempDir(string dir)
        {
            this.TempDir = dir;
            using (StreamWriter sr = new StreamWriter("Enviroment.xml",false,Encoding.UTF8))
            {
                sr.Write(Utility.Serializer.XmlSerialize<Enviroment>(this));
            }
        }
    }
}
