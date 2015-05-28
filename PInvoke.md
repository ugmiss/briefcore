## Platform Invocation Services ##
在受控代码与非受控代码进行交互时会产生一个事务（transition） ，这通常发生在使用平台调用服务（Platform Invocation Services），即P/Invoke
平台调用是一种服务，它使托管代码能够调用 DLL 中实现的非托管函数
如调用系统的 API 或与 COM 对象打交道，通过 System.Runtime.InteropServices 命名空间

为了从托管代码中调用非托管的DLL中函数，你要创建一个P/Invoke包装（Wrapper)。一个P/Invoke包装是一个.net兼 容的方法声明，用来创建P/Invoke包装的语法与创建托管方法的声明语法本质上是一样的。唯一不同是P/Invoke包装不包含函数体，而只有方法 名、返回值类型和参数信息。并且，P/Invoke包装使用了DllImport属性。这个属性是用来定位包含有目标函数的非托管的DLL。

定义一个全局的锁
```
public static volatile object SecuLock = new object();
```
然后在所有调用同一个非托管dll里面方法的地方都加锁，然后问题就解决了。
```
lock (SeverCallBack.SecuLock)
{
    EMGFindSecu("300",result);
}
```