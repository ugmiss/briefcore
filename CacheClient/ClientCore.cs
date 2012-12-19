using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CacheBusiness;
using CacheBusiness.Model;

namespace CacheClient
{
    public class ClientCore : ICacheWCF
    {
        CacheClient.ServiceReference1.CacheWCFClient client;
        public static ClientCore Instance { get; set; }
        public ClientCore()
        {
            InstanceContext instanceContext = new InstanceContext(new CacheCallBack());
            client = new CacheClient.ServiceReference1.CacheWCFClient(instanceContext);

        }

        public UserInfo[] GetUserData()
        {
            List<UserInfo> li = new List<UserInfo>();
            foreach(var u in client.GetUserData())
                li.Add((UserInfo)u);
            return li.ToArray();
        }
        public void AddUser(UserInfo u)
        {
            client.AddUser(u);
        }
        public void DeleteUser(UserInfo u)
        {
            client.DeleteUser(u);
        }
        public void ModifyUser(UserInfo u)
        {
            client.ModifyUser(u);
        }

    }
}
