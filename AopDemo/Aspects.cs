﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AopAlliance.Intercept;
using Spring.Aop;
using System.Windows.Forms;

namespace AopDemo
{
    // 数据过滤 环绕切入 IMethodInterceptor
    public class DataFilterAdvice : IMethodInterceptor
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
    // 权限验证 前置切入 IMethodBeforeAdvice
    public class AuthVarifyAdvice : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            if (!Environment.CurrrentUser.In("admin,eo,mo".Split(',')))
            {
                throw new Exception("没有权限访问");
            }
        }
    }
    // 日志记录 异常处理 抛出切入 IThrowsAdvice 后置切入 IAfterReturningAdvice
    public class LoggingAdvice : IAfterReturningAdvice, IThrowsAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            string successLog = Environment.CurrrentUser + " 执行：" + method.Name + "方法成功";
            Logger.Info(successLog); 
        }

        public void AfterThrowing(MethodInfo method, object[] args, object target, Exception exception)
        {
            string failedLog = Environment.CurrrentUser + " 执行：" + method.Name + "方法失败";
            if (exception != null)
            {
                string errorMsg = string.Format(failedLog + "{0}", exception.Message);
                Logger.Error(errorMsg);
            }
        }
    }
}