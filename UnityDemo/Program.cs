using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;

namespace UnityAopDemo
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
