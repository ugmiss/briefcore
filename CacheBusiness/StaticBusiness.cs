using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheBusiness.Model;
using System.ServiceModel;
using System.Caching.Expirations;

namespace CacheBusiness
{
    public class StaticBusiness : ICacheWCF
    {
        const string ConnString = "server=.;uid=sa;pwd=123456;database=CacheDemo";
        static List<ICacheRefreshCallback> ListCallBack = new List<ICacheRefreshCallback>();
        public UserInfo[] GetUserData()
        {
            ICacheRefreshCallback ICallback = OperationContext.Current.GetCallbackChannel<ICacheRefreshCallback>();
            if (!ListCallBack.Contains(ICallback))
                ListCallBack.Add(ICallback);
            string typeName = typeof(UserInfo).ToString();
            if (CacheHelper.MemCache.Contains(typeName) && CacheHelper.MemCache.GetData(typeName) != null)
            {
                return CacheHelper.MemCache.GetData(typeName) as UserInfo[];
            }
            else
            {
                BussinessExecuter exec = new BussinessExecuter(ConnString);
                List<UserInfo> li = exec.GetAll<UserInfo>();
                if (li != null)
                {
                    UserInfo[] userdata = li.ToArray();
                    CacheHelper.MemCache.Add(typeName, userdata, null, new CallBackExpiration(typeName));
                    return userdata;
                }
            }
            return null;
        }
        public void AddUser(UserInfo u)
        {
            BussinessExecuter exec = new BussinessExecuter(ConnString);
            exec.Add(u);
            CallBackExpiration.TimeOut[typeof(UserInfo).ToString()] = true;
            foreach (var ICallback in ListCallBack)
                ICallback.CacheTimeOut(typeof(UserInfo).ToString());
        }
        public void ModifyUser(UserInfo u)
        {
            BussinessExecuter exec = new BussinessExecuter(ConnString);
            exec.Modify(u);
            CallBackExpiration.TimeOut[typeof(UserInfo).ToString()] = true;
            foreach (var ICallback in ListCallBack)
                ICallback.CacheTimeOut(typeof(UserInfo).ToString());
        }
        public void DeleteUser(UserInfo u)
        {
            BussinessExecuter exec = new BussinessExecuter(ConnString);
            exec.Delete(u);
            CallBackExpiration.TimeOut[typeof(UserInfo).ToString()] = true;
            foreach (var ICallback in ListCallBack)
                ICallback.CacheTimeOut(typeof(UserInfo).ToString());
        }
    }
}
