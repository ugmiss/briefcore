using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;

namespace AuthDemo
{

    public class DataAroundAdvise : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = null;
            returnValue = invocation.Proceed();
            switch (Environment.CurrrentUser)
            {
                case "admin":
                    returnValue = "all";
                    break;
                case "eo":
                    returnValue = "eo only";
                    break;
                case "mo":
                    returnValue = "mo only";
                    break;
                default:
                    break;
            }
            return returnValue;
        }
    }
}
