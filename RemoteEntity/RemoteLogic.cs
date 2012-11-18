using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;

using System.Runtime.Remoting.Channels;
using System.Runtime;
using Entity;
using IService;
namespace RemoteEntity
{
    public class RemoteLogic:MarshalByRefObject,IRemoteLogic
    {
        public UserInfo getUserInfo() {
            UserInfo u = new UserInfo();
            return u;            
        }
        
    }
}
