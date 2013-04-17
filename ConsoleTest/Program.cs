using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string ssssss = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Console.WriteLine(ssssss);
            Console.ReadKey();
            BussinessExecuter exec = new BussinessExecuter("");
            exec.Modify<Person>(p => p.ID == "1", o => new Person() { Name = "AAA" });
        }
    }
    public class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
    }
}
