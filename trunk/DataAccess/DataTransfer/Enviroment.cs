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
                Enviroment env = sr.ReadToEnd().XmlDeserialize<Enviroment>();
                return env.TempDir;
            }
        }
        public void SaveTempDir(string dir)
        {
            this.TempDir = dir;
            using (StreamWriter sr = new StreamWriter("Enviroment.xml",false,Encoding.UTF8))
            {
                sr.Write(this.XmlSerialize<Enviroment>());
            }
        }
    }
}
