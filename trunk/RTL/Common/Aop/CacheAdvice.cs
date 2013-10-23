using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using System.Reflection;
using AopAlliance.Intercept;
using System.Collections.Concurrent;

namespace Common
{
    /// <summary>
    /// 缓存读取切面
    /// </summary>
    public class CacheReadAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = null;
            Type t = invocation.Method.ReturnType.GetGenericArguments()[0];
            if (DuplexCacheManager.IsTimeOut(t) || Caching.CacheManager.GetData(t.Name) == null)
            {
                returnValue = invocation.Proceed();
                Caching.CacheManager.Add(t.Name, returnValue);
                DuplexCacheManager.AddNotify(t);
            }
            else
            {
                returnValue = Caching.CacheManager.GetData(t.Name);
            }
            return returnValue;
        }
    }
    /// <summary>
    /// 缓存过期切面
    /// </summary>
    public class CacheExpireAdvice : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            Type t = args[0].GetType();
            //通知缓存改变
            DuplexCacheManager.NotifyTimeOut(t);
        }
    }
}
