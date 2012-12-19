using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheBusiness.Model;
using System.Caching.RefreshActions;
using System.ServiceModel;
using System.Caching.Expirations;

namespace CacheBusiness
{
    public class StaticBusiness : ICacheWCF
    {
        const string ConnString = "server=.;uid=sa;pwd=123456;database=CacheDemo";
        public UserInfo[] GetUserData()
        {
            string typeName = typeof(UserInfo).ToString();
            if (CacheHelper.MemCache.Contains(typeName))
            {
                return CacheHelper.MemCache.GetData(typeName) as UserInfo[];
            }
            else
            {
                BussinessExecuter exec = new BussinessExecuter(ConnString);
                UserInfo[] userdata = exec.GetAll<UserInfo>().ToArray();
                CacheHelper.MemCache.Add(typeName, userdata, null, new CallBackExpiration(typeName));
            }
            return null;
        }
        public void AddUser(UserInfo u)
        {
            BussinessExecuter exec = new BussinessExecuter(ConnString);
            exec.Add(u);
            ICacheRefreshCallback ICallback = OperationContext.Current.GetCallbackChannel<ICacheRefreshCallback>();
            ICallback.CacheTimeOut(typeof(UserInfo).ToString());
        }
        public void ModifyUser(UserInfo u)
        {
            BussinessExecuter exec = new BussinessExecuter(ConnString);
            exec.Modify(u);
            ICacheRefreshCallback ICallback = OperationContext.Current.GetCallbackChannel<ICacheRefreshCallback>();
            ICallback.CacheTimeOut(typeof(UserInfo).ToString());
        }
        public void DeleteUser(UserInfo u)
        {
            BussinessExecuter exec = new BussinessExecuter(ConnString);
            exec.Delete(u);
            ICacheRefreshCallback ICallback = OperationContext.Current.GetCallbackChannel<ICacheRefreshCallback>();
            ICallback.CacheTimeOut(typeof(UserInfo).ToString());
        }
    }
}
