using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternDemo
{
    public class Singleton
    {
        private Singleton()
        {
        }
        public static readonly Singleton Instance = new Singleton();
    }
    public class SingletonDemo
    {
        public static void Show()
        {
            Console.WriteLine(Singleton.Instance.ToString());
            Console.ReadKey();
        }
    }
}
