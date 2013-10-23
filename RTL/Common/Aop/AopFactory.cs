using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace Common
{
    /// <summary>
    /// 切面工厂。
    /// </summary>
    public class AopFactory
    {
        /// <summary>
        /// 生成接口代理类（spring中配置的object）。
        /// </summary>
        /// <typeparam name="T">泛型。</typeparam>
        /// <returns>泛型实例。</returns>
        public static T GetProxy<T>()
        {
            // objectid为spring配置中的objectid系统中默认使用接口名称。
            try
            {
                string objectid = typeof(T).Name;
                IApplicationContext _IApplicationContext = ContextRegistry.GetContext();
                return (T)_IApplicationContext[objectid];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
