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

            ICoat food;
            Console.Write("我要的是时尚上衣\t");
            food = SimpleFactory.CreateCoat("fashion");
            food.GetYourCoat();
            Console.ReadKey();
        }
    }
}
