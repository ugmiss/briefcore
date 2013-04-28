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
    public static class Logger
    {
        public static log4net.ILog Instance { get; private set; }
        static Logger()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
            Instance = log4net.LogManager.GetLogger("");
        }
    }
}
