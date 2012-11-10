using System;
using System.Threading.Tasks;
using System.Linq;

namespace RandomDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 10;
            Parallel.For(0, N, i =>
            {
                int seed = (int)DateTime.Now.Ticks;
                Random rnd = new Random(seed);
                int num = rnd.Next(100);
                Console.WriteLine("  {0:D2}  :  Ticks {1}", num, seed);
            });
            Parallel.For(0, N, i =>
            {
                int seed = DateTime.Now.Millisecond;
                Random rnd = new Random(seed);
                int num = rnd.Next(100);
                Console.WriteLine("  {0:D2}  :  Milli {1}", num, seed);
            });
            Parallel.For(0, N, i =>
            {
                int seed = Guid.NewGuid().GetHashCode();
                Random rnd = new Random(seed);
                int num = rnd.Next(100);
                Console.WriteLine("  {0:D2}  :  Guid  {1}", num, seed);
            });
            Parallel.For(0, N, i =>
            {
                int seed = Guid.NewGuid().GetHashCode();
                //Random rnd = new Random(seed);
                int num = seed % 50 + 50;// rnd.Next(100);
                Console.WriteLine("  {0:D2}  :  Guid2 {1}", num, seed);
            });
            Console.ReadKey();
        }
    }
}
