对于随机种子来说，通常会使用的三种方式：Ticks，Millisecond，Guid的Hash
如果时间不是连续取的话，都没有什么问题。但是如果短时间内取的话，会发现Ticks，Millisecond基本没变，取到的数字都是相同的，如果去写一行Thread.Sleep(N)也能缓解这个问题，但是势必降低了程序的性能，而GUID方式就没有这个问题，每次取到的Guid不同，Guid的Hash也就不同，短时间也不同。

另外用循环变量i乘上Ticks，Millisecond也可以，不过可能在短时间内的数据是有规律性的，而Guid完全不需要担心这个问题。
演示代码如下：
```
using System;
using System.Threading.Tasks;

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
            Console.ReadKey();
        }
    }
}
```