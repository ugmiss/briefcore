using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// 日志切面。
    /// </summary>
    public class LoggingAdvice : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            string successLog = " 执行：" +target.ToString()+"."+ method.Name;
            //Logger.Info(successLog);//自定义的日志管理类
        }
    }
}
