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
}
