#### 配置 ####
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <!--log4net配置项类型-->
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net"/>
  </configSections>
  <log4net>
    <!--log4net配置根,默认会加载引用的Appender-->
    <root>
      <!--日志从高到低的五个级别 : FATAL ERROR WARN INFO DEBUG-->
      <level value="ALL" />
      <!--消息日志:INFO DEBUG-->
      <appender-ref ref="DebugAppender" />
      <!--错误日志:FATAL ERROR WARN-->
      <appender-ref ref="ErrorAppender" />
    </root>
    <!--消息日志配置：RollingFileAppender滚动文件-->
    <appender name="DebugAppender" type="log4net.Appender.RollingFileAppender" >
      <!--追加文件-->
      <param name="AppendToFile" value="true" />
      <!--多进程共享日志文件-->
      <param name="LockingModel" type="log4net.Appender.FileAppender+MinimalLock" />
      <!--静态日志文件名-->
      <param name="StaticLogFileName" value="false" />
      <!--日志保存目录-->
      <param name="File" value="./Log/" />
      <!--日期格式文件名-->
      <param name="DatePattern" value="yyyy-MM-dd'.log'" />
      <!--滚动方式-->
      <param name="RollingStyle" value="Date" />
      <!--日志布局-->
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="[%-5p][%d{HH:mm:ss}][%t] - %m%n" />
        <!--
        %m(message):输出的日志消息，如ILog.Debug(…)输出的一条消息
        %n(new line):换行
        %d(datetime):输出当前语句运行的时刻
        %r(run time):输出程序从运行到执行到当前语句时消耗的毫秒数
        %t(thread id):当前语句所在的线程ID
        %p(priority): 日志的当前优先级别，即DEBUG、INFO、WARN…等
        %c(class):当前日志对象的名称，例如：
        %L：输出语句所在的行号
        %F：输出语句所在的文件名
        %-数字：表示该项的最小长度，如果不够，则用空格填充
        -->
      </layout>
      <!--日志级别过滤规则-->
      <filter type="log4net.Filter.LevelRangeFilter">
        <param name="LevelMin" value="DEBUG" />
        <param name="LevelMax" value="INFO" />
      </filter>
    </appender>
    <!--错误日志：RollingFileAppender滚动文件-->
    <appender name="ErrorAppender" type="log4net.Appender.RollingFileAppender,log4net" >
      <param name="AppendToFile" value="true" />
      <param name="LockingModel" type="log4net.Appender.FileAppender+MinimalLock" />
      <param name="StaticLogFileName" value="false" />
      <param name="File" value="./Log/" />
      <param name="DatePattern" value="yyyy-MM-dd'.error.log'" />
      <param name="RollingStyle" value="Date" />
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="[%-5p][%d{HH:mm:ss}][%t] - %m%n" />
      </layout>
      <filter type="log4net.Filter.LevelRangeFilter">
        <param name="LevelMin" value="WARN" />
        <param name="LevelMax" value="FATAL" />
      </filter>
    </appender>
  </log4net>
</configuration>
```

### 代码部分 ###
  * 包装
```
public static class Logger
{
    public static log4net.ILog Instance { get; private set; }
    static Logger()
    {
        // 文件监视
        log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
        // 传空串表示取root里引用的appender
        Instance = log4net.LogManager.GetLogger("");
    }
}
```
  * 调用
```
if (Logger.Instance.IsFatalEnabled)
    Logger.Instance.Fatal("严重错误：未捕获的异常XXX");
```