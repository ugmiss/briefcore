using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Spring.Aop;
using System.Reflection;

namespace AuthDemo
{

    public class DataAroundAdvise : IMethodInterceptor
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
                case "eo":
                    returnValue = li.FindAll(p => p.StartsWith("eo")).ToList();
                    break;
                case "mo":
                    returnValue = li.FindAll(p => p.StartsWith("mo")).ToList();
                    break;
                default:
                    break;
            }
            return returnValue;
        }
    }


    public class AuthPreAdvise : IMethodBeforeAdvice
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
