using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFEntity;

namespace WCFContract
{
    [ServiceContract(Namespace = "http://www.hujao.com/", SessionMode = SessionMode.Required, CallbackContract = typeof(ICallback))]
    public interface IMessageService
    {
        /// <summary>
        /// 客户端发送消息。
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="msg"></param>
        [OperationContract(IsOneWay = true)]
        void SendClientMessage(Guid clientid, WcfMsg msg);
    }
}
