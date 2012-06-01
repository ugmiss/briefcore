using System;
using System.Collections.Generic;
using System.Text;

namespace Aop.Metadata
{
    /// <summary>
    /// 用于保存Exception相关信息
    /// </summary>
    public class ExceptionMetadata
    {
        /// <summary>
        /// 保存异常信息
        /// </summary>
        // Exception _ex;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ex">初始化异常</param>
        public ExceptionMetadata(Exception ex)
        {
            Ex = ex;
        }

        /// <summary>
        /// Property：异常信息
        /// </summary>
        public Exception Ex
        {
            get;
            set;
        }
    }
}
