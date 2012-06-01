using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace System
{
    public class Logger
    {
        // 日志路径。
        const string ERRORLOG = "error.log";
        // 资源访问的锁
        static ReaderWriterLockSlim lockx = new ReaderWriterLockSlim();
        // 写日志。
        public static void Write(string message)
        {
            lockx.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(ERRORLOG, true))
                {
                    string prefix = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : ";
                    sw.WriteLine(prefix);
                    sw.WriteLine(message);
                    sw.Flush();
                    sw.Close();
                }
            }
            finally
            {
                lockx.ExitWriteLock();
            }
        }
        // 写日志。
        public static void Write(Exception ex)
        {
            lockx.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(ERRORLOG, true))
                {
                    string prefix = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : ";
                    sw.WriteLine(ex.Message);
                    sw.WriteLine(ex.Source);
                    sw.WriteLine(ex.StackTrace);
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                }
            }
            finally
            {
                lockx.ExitWriteLock();
            }
        }
    }
}