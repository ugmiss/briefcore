### Linq技巧 ###
1 生成0到100的数字集合
```
int[]arr=Enumerable.Range(0,100).ToArray();
```




2 通过`List<T>`取类型`T`
```
Type t = typeof(List<T>).GetGenericArguments()[0];
```

3 求和尾数9
```
int sum= Enumerable.Range(0, 100).Where(o=>o%10==9).Sum();
```