using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Spring.Aop;
using System.Reflection;

namespace AuthDemo
{
    // 数据过滤
    public class DataFilterAdvise : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = null;
            returnValue = invocation.Proceed();
            List<string> li = (List<string>)returnValue;
            switch (Environment.CurrrentUser)
            {
                case "admin":
                    returnValue = li;
                    break;
                default:
                    returnValue = li.FindAll(p => p.StartsWith(Environment.CurrrentUser)).ToList();
                    break;
            }
            return returnValue;
        }
    }

    // 权限验证。
    public class AuthVarifyAdvise : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            if (!Environment.CurrrentUser.In("admin,eo,mo".Split(',')))
            {
                throw new Exception("没有权限访问");
            }
        }
    }
}
