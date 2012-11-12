using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(Singleton.Instance == null);
            Console.ReadKey();
        }
    }
}
