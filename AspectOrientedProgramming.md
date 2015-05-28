可以通过预编译方式和运行期动态代理实现在不修改源代码的情况下给程序动态统一添加功能的一种技术。
# Aop优点 #
  * 减少重复代码段的复制
  * 方便对切入部分统一替换和修改
  * 松散代码耦合
# Aop切入方式 #
  * 前置切入，方法执行前
  * 后置切入，方法执行后 （return后）
  * 环绕切入，方法拦截，可对方法参数，返回值修改
  * 抛出异常切入，方法异常时切入
# Aop应用和实现方式 #
## Aop应用 ##
  * _权限管理_
  * _日志管理_
  * _异常管理_
  * _事务管理_
  * _缓存管理_
## .Net中Aop的实现方式 ##
  * Spring .NET (基于动态代理的\*消息拦截**)
```
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
```
app.Config方式
```
<?xml version="1.0"?>
<configuration>
  <configSections>
    <!--spring配置-->
    <sectionGroup name="spring">
      <section name="context" type="Spring.Context.Support.ContextHandler, Spring.Core"/>
      <section name="objects" type="Spring.Context.Support.DefaultSectionHandler, Spring.Core"/>
    </sectionGroup>
    <!-- 日志配置 -->
    <sectionGroup name="common">
      <section name="logging" type="Common.Logging.ConfigurationSectionHandler, Common.Logging" />
    </sectionGroup>
  </configSections>
  <!--spring配置项-->
  <spring>
    <context>
      <resource uri="config://spring/objects"/>
    </context>
    <objects xmlns="http://www.springframework.net">
      <!--Repository开始-->
      <object id="IGIS_MineRepository" type="vOrient.MPS.Repository.GIS_MineRepository, vOrient.MPS.Repository"/>
      <!--Repository结束-->
      <!--Aop切点配置-->
      <object id="afterAdvisor" type="Spring.Aop.Support.NameMatchMethodPointcutAdvisor, Spring.Aop">
        <property name="Advice">
          <!--切点类型-->
          <object type="vOrient.Common.LoggingAdvice, vOrient.Common"/>
        </property>
        <property name="MappedNames">
          <list>
            <!--方法名匹配-->
            <value>Add*</value>
            <value>Remove*</value>
            <value>Modify*</value>
            <value>GetAll*</value>
          </list>
        </property>
      </object>
      <!--按object id匹配-->
      <object type="Spring.Aop.Framework.AutoProxy.ObjectNameAutoProxyCreator, Spring.Aop">
        <property name="ObjectNames">
          <list>
            <value>*Repository</value>
          </list>
        </property>
        <property name="InterceptorNames">
          <list>
            <value>afterAdvisor</value>
          </list>
        </property>
      </object>
      <!--按类型名称匹配-->
      <!--<object type="Spring.Aop.Framework.AutoProxy.TypeNameAutoProxyCreator, Spring.Aop">
        <property name="TypeNames">
          <list>
            <value>vOrient.MPS.Repository.*</value>
          </list>
        </property>
        <property name="InterceptorNames">
          <list>
            <value>afterAdvisor</value>
          </list>
        </property>
      </object>-->
    </objects>
  </spring>
  <!--common.logging配置项-->
  <common>
    <logging>
      <factoryAdapter type="Common.Logging.Simple.TraceLoggerFactoryAdapter, Common.Logging">
        <arg key="level" value="DEBUG" />
        <arg key="showLogName" value="true" />
        <arg key="showDataTime" value="true" />
        <arg key="dateTimeFormat" value="yyyy/MM/dd HH:mm:ss:fff" />
      </factoryAdapter>
    </logging>
  </common>
</configuration>

```
app.config代码实现部分
```
public class RepositoryFactory
{
    public static T GetProxy<T>()
    {
        string repositoryid = typeof(T).Name;
        IApplicationContext ctx = ContextRegistry.GetContext();
        return (T)ctx[repositoryid];
    }
}
    
public class GIS_MineService
{
    IGIS_MineRepository ir = RepositoryFactory.GetProxy<IGIS_MineRepository>();
    public GIS_MineService()
    {
    }
    public bool AddGIS_Mine(string mineName)
    {
        return ir.Add(new GIS_Mine() { IsValid = true, Name = mineName });
    }
    public List<GIS_Mine> GetGIS_Mine()
    {
        return ir.GetAll();
    }
}

```**


  * 企业库Unity(基于动态代理的\*消息拦截**)
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;

namespace UnityDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer().AddNewExtension<Interception>().RegisterType<IOrderManage, OrderManage>();
            container.Configure<Interception>().SetInterceptorFor<IOrderManage>(new InterfaceInterceptor());
            var iorderManage = container.Resolve<IOrderManage>();
            iorderManage.AddNewOrder(11);
            Console.ReadLine();
        }
    }
    public interface IOrderManage { void AddNewOrder(int orderID);     }
    public class RoundHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation methodInfo, GetNextHandlerDelegate GetNext)
        {
            Console.WriteLine("方法名: {0}", methodInfo.MethodBase.Name);
            Console.WriteLine("参数:");
            for (var i = 0; i < methodInfo.Arguments.Count; i++)
            {
                Console.WriteLine("{0}: {1}", methodInfo.Arguments.ParameterName(i), methodInfo.Arguments[i]);
            }
            //执行原始方法
            var retvalue = GetNext()(methodInfo, GetNext);
            return retvalue;
        }
        public int Order { get; set; }
    }
    public class RoundHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new RoundHandler();
        }
    }
    [RoundHandler]
    public class OrderManage : IOrderManage
    {
        public void AddNewOrder(int x)
        {
            Console.WriteLine("添加新订单的订单号为:{0}", x);
        }
    }
}
```
  * Emit IL(基于中间语言IL的\*动态编译**）