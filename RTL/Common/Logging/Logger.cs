using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public delegate void LogWrite(string str, string color);
    public static class Logger
    {
        public static event LogWrite OnLogWrite;
        // 实例
        public static log4net.ILog Instance { get; private set; }
        // 构造方法
        static Logger()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
            // 取log4net.config的root默认引用的Appender
            Instance = log4net.LogManager.GetLogger(string.Empty);
        }
        /// <summary>
        /// DEBUG, 帮助应用程序进行调试的信息。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Debug(string format, params object[] args)
        {
            Instance.DebugFormat(format, args);
            if (OnLogWrite != null)
            {
                OnLogWrite(format, "Green");
            }
        }
        /// <summary>
        /// INFO, 指出应用程序运行过程的信息。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Info(string format, params object[] args)
        {
            Instance.InfoFormat(format, args);
            if (OnLogWrite != null)
            {
                OnLogWrite(format,"Blue");
            }
        }
        /// <summary>
        /// WARN, 表明应用程序可能存在潜在错误的信息。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Warn(string format, params object[] args)
        {
            Instance.WarnFormat(format, args);
            if (OnLogWrite != null)
            {
                OnLogWrite(format, "Red");
            }
        }
        /// <summary>
        /// WARN, 表明应用程序可能存在潜在错误的信息。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Warn(Exception ex)
        {
            Instance.WarnFormat(ex.Message, ex);
            if (OnLogWrite != null)
            {
                OnLogWrite(ex.Message, "Red");
            }
        }
        /// <summary>
        /// ERROR, 表明应用程序出现错误(但不影响程序继续运行)。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Error(string format, params object[] args)
        {
            Instance.ErrorFormat(format, args);
        }
        /// <summary>
        /// ERROR, 表明应用程序出现错误(但不影响程序继续运行)。
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            Instance.Error(ex.Message, ex);
            if (OnLogWrite != null)
            {
                OnLogWrite(ex.Message,"Red");
            }
        }
        /// <summary>
        /// FATAL, 表明应用程序出现严重错误(程序无法继续运行)。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Fatal(string format, params object[] args)
        {
            Instance.FatalFormat(format, args);
        }
        /// <summary>
        /// FATAL, 表明应用程序出现严重错误(程序无法继续运行)。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Fatal(Exception ex)
        {
            Instance.FatalFormat(ex.Message, ex);
        }
    }
}
