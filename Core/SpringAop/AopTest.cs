using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop.Framework;

namespace Spring.Aop
{
    public class AopTest
    {
        public void Test()
        {

            ProxyFactory factory = new ProxyFactory(new OrderService());

            factory.AddAdvice(new AroundAdvise());
            factory.AddAdvice(new BeforeAdvise());
            factory.AddAdvice(new AfterReturningAdvise());
            factory.AddAdvice(new ThrowsAdvise());

            IOrderService service = (IOrderService)factory.GetProxy();
        }
    }

    public class OrderService
    {
        public string GetName()
        {
            return "sds";
        }
    }
    public interface IOrderService
    {
        string GetName();
    }
}
