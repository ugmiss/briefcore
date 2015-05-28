私有构造，静态只读的实例属性。
```
public class Singleton
{
    private Singleton()
    {
    }
    public static readonly Singleton Instance = new Singleton();
}
```