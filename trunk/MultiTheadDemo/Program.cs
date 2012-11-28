using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTheadDemo
{
    public static class Program
    {
        static ConcurrentDictionary<int, string> Cache = new ConcurrentDictionary<int, string>();
        public static void Main()
        {
            Utility.Hanzi.CnNameFactory fac = new Utility.Hanzi.CnNameFactory();
            int N = 100000;
            int th1count = 0;
            int th2count = 0;
            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < N; i++)
                {
                    Thread.Sleep(1);
                    bool NotExist = Cache.TryAdd(i, fac.GetBoyName());
                    if (NotExist)
                    {
                        //Console.WriteLine("Exist:name" + i + " thr0:" + Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        th1count++;
                        //Console.WriteLine("NotExist:name" + i + " thr0:" + Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });

            tasks[1] = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < N; i++)
                {
                    Thread.Sleep(1);
                    bool NotExist = Cache.TryAdd(i, fac.GetGirlName());
                    if (NotExist)
                    {
                        //Console.WriteLine("Exist:name" + i + " thr1:" + Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        th2count++;
                        //Console.WriteLine("NotExist:name" + i + " thr1:" + Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });

            // Output results so far.
            Task.WaitAll(tasks);

            Console.WriteLine("Count:" + Cache.Count);
            Console.WriteLine("1 add:" + th1count);
            Console.WriteLine("2 add:" + th2count);

            Console.ReadLine();
        }
    }
}
