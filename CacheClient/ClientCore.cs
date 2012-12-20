using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CacheBusiness;
using CacheBusiness.Model;
using System.Caching.Expirations;

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
            string typeName = typeof(UserInfo).ToString();
            if (CacheHelper.MemCache.Contains(typeName) && CacheHelper.MemCache.GetData(typeName) != null)
            {
                return CacheHelper.MemCache.GetData(typeName) as UserInfo[];
            }
            else
            {
                List<UserInfo> li = new List<UserInfo>();
                foreach (UserInfo u in client.GetUserData())
                    li.Add((UserInfo)u);
                CacheHelper.MemCache.Add(typeName, li.ToArray(), null, new CallBackExpiration(typeName));
                return li.ToArray();
            }
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
