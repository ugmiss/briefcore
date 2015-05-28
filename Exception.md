### 4.0中如何捕捉未处理的异常 ###
```
AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
```