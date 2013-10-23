using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common
{
    /// <summary>
    /// 异常处理。
    /// </summary>
    public class ExceptionManager
    {

    }
    /// <summary>
    /// 数据访问异常。
    /// </summary>
    //[DataContractAttribute]
    public class DataAccessException : Exception
    {
        public DataAccessException(string msg)
            : base(msg)
        {

        }
        public string Sql { get; set; }
    }
    /// <summary>
    /// 业务逻辑异常。
    /// </summary>
    public class DomainServiceException : Exception
    {
        public string Business { get; set; }
    }
    /// <summary>
    /// 客户端异常。
    /// </summary>
    public class ClientUIException : Exception
    {
    }
}
