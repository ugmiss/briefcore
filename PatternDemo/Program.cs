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

            ICar car;
            Console.WriteLine("需要一辆商务轿车");
            car = SimpleFactory.CreateCoat("business");
            Console.WriteLine("轿车出厂：" + car.GetCarName());
            Console.ReadKey();
        }
    }
}
