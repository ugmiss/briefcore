using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace MultiTheadDemo
{
    public static class Program
    {
        public static void Main()
        {
            ConcurrentBag<string> list = new ConcurrentBag<string>();
            int[] array = Enumerable.Range(0, 5).ToArray();
            int[] array2 = Enumerable.Range(5, 10).ToArray();

            Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(array, i =>
                {
                    list.Add(i.ToString());
                });
            });

            Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(array2, i =>
                {
                    var x = i.ToString();

                    Console.WriteLine("remove before:" + x);
                    
                    list.TryTake(out x);
                    Console.WriteLine("remove:" + x);
                    Console.WriteLine("count:" + list.Count);
                });
            });
            Console.ReadLine();
        }
    }
}
