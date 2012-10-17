using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Spring.Aop;

namespace Spring.Aop
{
    /// <summary>
    /// 异常通知
    /// </summary>
    public class ThrowsAdvise : IThrowsAdvice 
    {
        public void AfterThrowing(Exception ex)
        {
            string errorMsg = string.Format("     异常通知： 方法抛出的异常 : {0}", ex.Message);
            Console.Error.WriteLine(errorMsg);

            Console.WriteLine();
        }
    }
}
