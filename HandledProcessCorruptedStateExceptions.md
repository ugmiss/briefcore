# HandledProcessCorruptedStateExceptions #

.NET 4.0 中对异常级别做了处理，不能让如会让系统崩溃的异常也可以给吃掉。

Corrupted State Exceptions

自4.0以后，CLR不会主动给你抛出所有异常了，对于那些它认为是危险的，可能导致进程崩溃的异常它会标记为CorruptedStateException并自己处理掉而不是抛给程序员来做，如AccessViolationException这种继承自SystemException的异常就会被当做CorruptedStateException来处理。不过这里要注意的是，仅仅异常类型是可能会危险级别的异常还不够，CLR还会判断抛出异常的所有者，如果它发现是由操作系统抛出的访问冲突则会认为这是状态崩溃异常，但如果异常是由用户代码抛出，则CLR不会对其做特殊处理，它仍然会像以前一样将其正常抛出。

  * 如果你想把以往旧的代码在.NETFramework4.0下编译但又不想改代码的话，配置节点：        legacyCorruptedState--ExceptionsPolicy=true
  * .NET4.0中增加了一个新的命名空间：System.Runtime.ExceptionServices，类HandleProcessCorruptedStateExceptionsAttribute，你只需要在相应方法上添加这个属性，CLR就会把所有的异常处理交给你。
```
[HandledProcessCorruptedStateExceptions]
public static int FunctionName()
{
　　try
    {
        //code maybe throw exception
    }
    catch(Exception ex)
    {
        //can get all the Exception
    }
}
```