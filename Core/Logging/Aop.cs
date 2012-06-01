using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aop;

namespace System
{
    public class LogStartAttribute : PreAspectAttribute
    {
        public override void Action(InvokeContext context)
        {
            Console.WriteLine("log start!");
        }
    }

    public class LogEndAttribute : PostAspectAttribute
    {
        public override void Action(InvokeContext context)
        {
            Console.WriteLine("log end!");
        }
    }

    public class LogExAttribute : ExceptionAspectAttribute
    {
        public override void Action(InvokeContext context)
        {
            Console.WriteLine("ex:" + context.Ex.Ex.Message);
            Logger.Write(context.Ex.Ex);
        }
    }
}
