using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFEntity;

namespace WCFContract
{
    public interface ICallback
    {
        /// <summary>
        /// 服务器推送消息。
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="msg"></param>
        [OperationContract(IsOneWay = true)]
        void PollServerMessage(Guid clientid, WcfMsg msg);
    }
}
