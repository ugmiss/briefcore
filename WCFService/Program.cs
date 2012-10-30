using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            WCFServer.Instance.Start();
            Console.ReadLine();
        }
    }
}
