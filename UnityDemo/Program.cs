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
            var container1 = new UnityContainer().AddNewExtension<Interception>().RegisterType<IOutput, OutputImplement1>();
            //container1 = new UnityContainer().AddNewExtension<Interception>().RegisterType<IOutput, OutputImplement2>();
            container1.Configure<Interception>().SetInterceptorFor<IOutput>(new InterfaceInterceptor());
            var op1 = container1.Resolve<IOutput>();
            op1.Output(11);
            Console.ReadLine();
        }
    }

    public interface IOutput { void Output(int x);     }

    public class RoundHandler : ICallHandler
    {
        public int Order { get; set; }//这是ICallHandler的成员，表示执行顺序
        public IMethodReturn Invoke(IMethodInvocation methodInfo, GetNextHandlerDelegate GetNext)
        {
            Console.WriteLine("方法名: {0}", methodInfo.MethodBase.Name);
            Console.WriteLine("参数:"); for (var i = 0; i < methodInfo.Arguments.Count; i++)
            {
                Console.WriteLine("{0}: {1}", methodInfo.Arguments.ParameterName(i), methodInfo.Arguments[i]);
            }
            Console.WriteLine("执行");
            var retvalue = GetNext()(methodInfo, GetNext);//在这里执行方法  
            Console.WriteLine("完成");
            return retvalue;
        }
    }
    public class RoundHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new RoundHandler();
        }
    }
    [RoundHandler]
    public class OutputImplement1 : IOutput
    {
        public void Output(int x)
        {
            Console.WriteLine("重典执行此方法输出:{0}", x);
        }
    }
    [RoundHandler]
    public class OutputImplement2 : IOutput
    {
        public void Output(int x) { Console.WriteLine("output:{0}", x); }
    }
}
