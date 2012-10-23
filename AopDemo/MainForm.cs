using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spring.Aop.Framework;
using AopAlliance.Intercept;
using Spring.Aop;
using System.Reflection;

namespace AopDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "admin";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "eo";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "mo";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "lo";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //创建SpringAop代理工厂
            ProxyFactory proxyFactory = new ProxyFactory(new DataProvider());
            //添加通知
            proxyFactory.AddAdvice(new DataFilterAdvice());
            proxyFactory.AddAdvice(new AuthVarifyAdvice());
            proxyFactory.AddAdvice(new LoggingAdvice());
            //动态代理接口
            IDataProvider idata = (IDataProvider)proxyFactory.GetProxy();
            try
            {
                List<string> res = idata.GetData();
                MessageBox.Show(string.Join("\n", res));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
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
