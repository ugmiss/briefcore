## 从循环说起 ##
我们使用的循环有for，foreach，和linq方式
其中for是非线程安全的
foreach和linq是线程安全的，执行是会锁定整个集合
就是说，串行方式遍历集合的元素，仍然是单核执行，这样效率不高。
## 并行遍历 ##
4.0提供了Parallel
针对多核处理器可以并发式的遍历集合元素，非有序的遍历，所以效率高于foreach
```
Parallel.For(0, N, i =>
{
    //类似for循环
    //使用Stop会立即停止循环，使用Break会执行完毕所有符合条件的项。
    LoopState.Stop();
    LoopState.Break();
});

Parallel.ForEach(list,item =>
{
    //类似foreach循环
});
```
## 结合ConcurrentDictionary ##
ConcurrentDictionary本身是支持多线程的
Parallel遍历ConcurrentDictionary是很好的一种方式