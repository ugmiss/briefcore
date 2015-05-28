在.net4以前的字典集合为 Dictionary
但是Dictionary不是线程安全的，就是说有多线程同时访问Dictionary时会有安全问题。
所以通常情况下我们在对Dictionary 添加修改删除操作时会加锁。
代码如下
```
object diclock;
Dictionary<int,string>myDic=new Dictionary<int,string>();
lock(diclock)
{
   if(!myDic.ContainsKey(1))
      myDic.Add(1,"Ace");
}
```
但是上面的代码操作会锁整个集合，效率不高。
而在4.0中加入的ConcurrentDictionary本身就是线程安全的。
```
ConcurrentDictionary<int,string>myDic=new ConcurrentDictionary<int,string>();
myDic.TryAdd(1,"Ace");
```
减少了锁操作后，效率大大提高。