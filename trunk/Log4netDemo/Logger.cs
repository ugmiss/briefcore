using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Appender;
using System.IO;
using System.Threading;
using System.Collections;

namespace Log4netDemo
{
    /// <summary>
    /// 程序运行日志记录接口, FATAL > ERROR > WARN > INFO > DEBUG
    /// </summary>
    public static class Logger
    {
        /*
         * -------------------Level---------------------------
         * 日志从高到低的五个级别 : FATAL ERROR WARN INFO DEBUG
         * 错误日志:FATAL ERROR WARN 
         * 消息日志:INFO DEBUG
         * -------------------ConversionPattern---------------
         * %m(message):输出的日志消息，如ILog.Debug(…)输出的一条消息
         * %n(new line):换行
         * %d(datetime):输出当前语句运行的时刻
         * %r(run time):输出程序从运行到执行到当前语句时消耗的毫秒数
         * %t(thread id):当前语句所在的线程ID
         * %p(priority): 日志的当前优先级别，即DEBUG、INFO、WARN…等
         * %c(class):当前日志对象的名称，例如：
         * %L：输出语句所在的行号
         * %F：输出语句所在的文件名
         * %-数字：表示该项的最小长度，如果不够，则用空格填充
         */
        private static log4net.ILog log = null;
        static Logger()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
            log = log4net.LogManager.GetLogger("CommonLogger");
        }
        public static void Debug(string format, params object[] args)
        {
            if (log.IsDebugEnabled)
                log.DebugFormat(format, args);
        }
        public static void Info(string format, params object[] args)
        {
            if (log.IsInfoEnabled)
                log.InfoFormat(format, args);
        }
        public static void Warn(string format, params object[] args)
        {
            if (log.IsWarnEnabled)
                log.WarnFormat(format, args);
        }
        public static void Error(string format, params object[] args)
        {
            if (log.IsErrorEnabled)
                log.ErrorFormat(format, args);
        }
        public static void Fatal(string format, params object[] args)
        {
            if (log.IsFatalEnabled)
                log.FatalFormat(format, args);
        }
    }

}
