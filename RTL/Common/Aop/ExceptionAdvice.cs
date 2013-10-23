using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// 异常切面。
    /// </summary>
    public class ExceptionAdvice : IThrowsAdvice
    {
        public void AfterThrowing(MethodInfo method, object[] args, object target, Exception exception)
        {
            string failedLog = " 执行：" + method.Name + "方法失败";
            if (exception != null)
            {
                Logger.Instance.Error(failedLog, exception);//自定义的日志管理类
            }
        }
    }
}
