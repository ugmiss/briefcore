using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CacheBusiness;
using CacheBusiness.Model;

namespace CacheWCFService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CacheWCFService : ICacheWCF
    {
        public CacheWCFService()
        {
            BusinessManager.StartUp(new StaticBusiness());
        }
        public UserInfo[] GetUserData()
        {
            return BusinessManager.GetUserData();
        }
        public void AddUser(UserInfo u)
        {
            BusinessManager.AddUser(u);
        }
        public void ModifyUser(UserInfo u)
        {
            BusinessManager.ModifyUser(u);
        }
        public void DeleteUser(UserInfo u)
        {
            BusinessManager.DeleteUser(u);
        }
    }
}
