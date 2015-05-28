### 线程安全 ###
如果你的代码所在的进程中有多个线程在同时运行，而这些线程可能会同时运行这段代码。如果每次运行结果和单线程运行的结果是一样的，而且其他的变量的值也和预期的是一样的，就是线程安全的
```
抛出异常InvalidOperationException：“集合已修改；可能无法执行枚举”
```
线程安全问题都是由全局变量及静态变量引起的

若每个线程中对全局变量、静态变量只有读操作，而无写操作，一般来说，这个全局变量是线程安全的；若有多个线程同时对一个变量执行读写操作，一般都需要考虑线程同步，否则就可能影响线程安全
### 线程阻塞 ###
winform中某些操作的持续时间可能会很长，比如单击一个按钮，导致整个界面处于未响应的假死状态，就是线程阻塞。针对这种情况我们会开启新的线程去处理，多线程的同步或者异步方式去处理复杂的，持续时间长的任务。
winform程序有主UI线程，控件在主UI线程上创建，当其他线程调用修改主线程创建的控件时，会抛出InvalidOperationException异常，所以又二种方式屏蔽：

1.在方法内通过this.Invoke()方式。
```
this.Invoke((ThreadStart)delegate()
{
});
```
2.关闭控件的跨线程访问检查
```
Control.CheckForIllegalCrossThreadCalls = false;
```

在第一种方式中的这个只是主线程调用，还不过完美。可能有其他的线程调用，这时就需要用
if (this.InvokeRequired)的方式。
```
if (this.InvokeRequired)
{
   this.Invoke(new DelegateChangeText(ChangeText));
}
else
{
   this.ChangeText();
}
```
### lock ###
lock 确保当一个线程位于代码的临界区时，另一个线程不进入临界区。如果其他线程试图进入锁定的代码，则它将一直等待（即被阻止），直到该对象被释放

### Interlocked ###
为多线程共享变量提供原子操作
```
int i = 0 ;
Interlocked.Increment( ref i);
Interlocked.Decrement( ref i);
Interlocked.Exchange( ref i, 100 );
Interlocked.CompareExchange( ref i, 10 , 100 );
```
### Mutex ###
### Monitor ###
```
lock (x)
{
DoSomething();
} 

等效于

object obj = ( object )x;
System.Threading.Monitor.Enter(obj);
try 
{
DoSomething();
}
finally 
{
System.Threading.Monitor.Exit(obj);
}
```
### System.Collections.Concurrent; ###
|`BlockingCollection<T>`|为实现 IProducerConsumerCollection<(Of <(T>)>) 的线程安全集合提供阻止和限制功能|
|:----------------------|:-------------------------------------------------------------------------------------------------|
|`ConcurrentBag<T>`     | 表示对象的线程安全的无序集合                                                       |
|`ConcurrentDictionary<key,value>`|表示可由多个线程同时访问的键值对的线程安全集合                             |
|`ConcurrentQueue<T>`   |表示线程安全的先进先出 (FIFO) 集合                                                   |
|`ConcurrentStack<T>`   | 表示线程安全的后进先出 (LIFO) 集合                                                  |
|`OrderablePartitioner<T>`|表示将一个可排序数据源拆分成多个分区的特定方式                             |
|`Partitioner`          |提供针对数组、列表和可枚举项的常见分区策略                                   |
|`Partitioner<T>`       | 表示将一个数据源拆分成多个分区的特定方式                                     |


ConcurrentBag 是无需集合，所以在取元素的时候没有提供移除指定元素的方法，所以适用于类似连接池的应用。

http://www.cnblogs.com/kain/archive/2010/08/10/1796524.html

### ThreadPool ###
ThreadPool是托管线程池，是由CLR管理的。

通常是将计算密集型的操作放在worker线程池中运行，而线程池的大小会根据当前的CPU使用量自动调整，通过下面两个方法，我们可以设置线程池的大小：
```
ThreadPool.SetMaxThreads(10, 200);
ThreadPool.SetMinThreads(2, 40);//两个参数分别是WorkThread和IO thread的限制。
```
### Task ###
Task的优势
ThreadPool相比Thread来说具备了很多优势，但是ThreadPool却又存在一些使用上的不方便。比如：
1: ThreadPool不支持线程的取消、完成、失败通知等交互性操作；
2: ThreadPool不支持线程执行的先后次序；
```
static void Main(string[] args)
{
     Task t = new Task(() =>
     {
           Console.WriteLine("任务开始工作……");
           //模拟工作过程
           Thread.Sleep(5000);
     });
     t.Start();
     t.ContinueWith((task) =>
     {
           Console.WriteLine("任务完成，完成时候的状态为：");
           Console.WriteLine("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}",
 task.IsCanceled, task.IsCompleted, task.IsFaulted);
     });
     Console.ReadKey();
}
```
**一、新建任务**
创建任务的方式有两种，一种是通过Task.Factory.StartNew方法来创建一个新任务，如：
```
//此行代码执行后，任务就开始执行
Task task = Task.Facotry.StartNew(()=>Console.WriteLine(“Hello, World!”));
```
另一种方法是通过Task类的构造函数来创建一个新任务，如：
```
//此处只把要完成的工作交给任务，但任务并未开始
Task task = new Task(()=>Console.WriteLine(“Hello, World!”));
//调用Start方法后，任务才会在将来某个时候开始执行。
task.Start();
```
**二、任务的取消**
```
using System; 
using System.Threading; 
using System.Threading.Tasks;

namespace TaskDemo 
{ 
    class Program 
    { 
        static void Main() 
        { 
            CancellationTokenSource cts = new CancellationTokenSource(); 
            Task t = new Task(() => LongRunTask(cts.Token)); 
            t.Start(); 
            Thread.Sleep(2000); 
            cts.Cancel(); 
            Console.Read(); 
        }
        static void LongRunTask(CancellationToken token) 
        {
            //此处方法模拟一个耗时的工作 
            for (int i = 0; i < 1000; i++) 
            { 
                if (!token.IsCancellationRequested) 
                { 
                    Thread.Sleep(500); 
                    Console.Write("."); 
                } 
                else 
                { 
                    Console.WriteLine("任务取消"); 
                    break; 
                } 
            } 
        } 
    } 
}
```

http://www.cnblogs.com/henllyee/archive/2011/06/06/net_parallel_programing.html

### Parrellel ###

### MTA STA ###
STA:   Single-Thread  Apartment，中文叫单线程套间。就是在COM库初始化的时候创建一个内存结构，然后让它和调用CoInitialize的线程相关联。这个内存结构针对每个线程都会有一个。支持STA的COM对象只能在创建它的线程里被使用，其它线程如果再创建它就会失败。

MTA:   Multi-Thread   Apartment，中文叫多线程套间。COM库在进程中创建一个内存结构，这个内存结构在整个进程中只能有一个，然后让它和调用CoInitializeEx的线程相关联。支持MTA的COM对象可以在任意线程里被使用。多有针对它的调用都会被封装成为消息。