using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class Logger
    {
        private static log4net.ILog log = null;

        static Logger()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
            log = log4net.LogManager.GetLogger("CommonLogger");
        }

        /// <summary>
        /// DEBUG, 帮助应用程序进行调试的信息。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Debug(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        /// <summary>
        /// INFO, 指出应用程序运行过程的信息。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Info(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        /// <summary>
        /// WARN, 表明应用程序可能存在潜在错误的信息。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Warn(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        /// <summary>
        /// ERROR, 表明应用程序出现错误(但不影响程序继续运行)。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Error(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }
        public static void Error(Exception ex)
        {
            log.ErrorFormat(ex.Message, null);
        }
        /// <summary>
        /// FATAL, 表明应用程序出现严重错误(程序无法继续运行)。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Fatal(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }
    }
}
