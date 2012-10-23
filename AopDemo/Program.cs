using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AopAlliance.Intercept;
using Spring.Aop;
using Spring.Aop.Framework;

namespace AopDemo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //切换不用的用户看到的结果是不同的
            //Environment.CurrentUser = "admin";
            Environment.CurrentUser = "eo";
            //Environment.CurrentUser = "mo";
            //创建SpringAop代理工厂
            ProxyFactory proxyFactory = new ProxyFactory(new DataProvider());
            //添加通知
            proxyFactory.AddAdvice(new DataFilterAdvice());
            proxyFactory.AddAdvice(new AuthVarifyAdvice());
            proxyFactory.AddAdvice(new LoggingAdvice());
            proxyFactory.AddAdvice(new ExcetionAdvice());

            //动态代理接口
            IDataProvider idata = (IDataProvider)proxyFactory.GetProxy();
            try
            {
                List<string> res = idata.GetData();
                Console.WriteLine(string.Join("\n", res));
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常：" + ex.Message);
            }
            Console.ReadLine();
        }
    }
    // 全局变量当前用户
    class Environment
    {
        public static string CurrentUser { get; set; }
    }
    // 数据接口
    public interface IDataProvider
    {
        List<string> GetData();
    }
    // 数据实现
    public class DataProvider : IDataProvider
    {
        public List<string> GetData()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            var x = random.Next(100);
            if (x > 20)//添加随机取数据的异常
                throw new Exception("数据访问异常");
            List<string> li = new List<string>();
            li.Add("eo:1000");
            li.Add("eo:2000");
            li.Add("eo:3000");
            li.Add("mo:5000");
            li.Add("mo:4000");
            return li;
        }
    }
    // 数据过滤 环绕切入 IMethodInterceptor
    class DataFilterAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = invocation.Proceed();
            List<string> li = (List<string>)returnValue;
            switch (Environment.CurrentUser)
            {
                case "admin":
                    returnValue = li;
                    break;
                default://按用户过滤数据
                    returnValue = li.FindAll(p => p.StartsWith(Environment.CurrentUser)).ToList();
                    break;
            }
            return returnValue;
        }
    }
    // 权限验证 前置切入 IMethodBeforeAdvice
    class AuthVarifyAdvice : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            if (Environment.CurrentUser == null || "admin,eo,mo".IndexOf(Environment.CurrentUser) < 0)
            {
                throw new Exception("没有权限访问");
            }
        }
    }
    // 日志记录 异常处理 抛出切入 IThrowsAdvice 后置切入 IAfterReturningAdvice
    class LoggingAdvice : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            string successLog = Environment.CurrentUser + " 执行：" + method.Name + "方法成功";
            Logger.Info(successLog);//自定义的日志管理类
        }
    }
    class ExcetionAdvice : IThrowsAdvice
    {
        public void AfterThrowing(MethodInfo method, object[] args, object target, Exception exception)
        {
            string failedLog = Environment.CurrentUser + " 执行：" + method.Name + "方法失败";
            if (exception != null)
            {
                Logger.Error(failedLog, exception);//自定义的日志管理类
            }
        }
    }
}
