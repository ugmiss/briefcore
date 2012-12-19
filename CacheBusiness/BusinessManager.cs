using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheBusiness.Model;

namespace CacheBusiness
{
    public class BusinessManager
    {
        static ICacheWCF _ICacheWCF;
        public static void StartUp(ICacheWCF ICacheWCF)
        {
            _ICacheWCF = ICacheWCF;
        }
        public static UserInfo[] GetUserData()
        {
            return _ICacheWCF.GetUserData();
        }
        public static void AddUser(UserInfo u)
        {
            _ICacheWCF.AddUser(u);
        }
        public static void DeleteUser(UserInfo u)
        {
            _ICacheWCF.DeleteUser(u);
        }
        public static void ModifyUser(UserInfo u)
        {
            _ICacheWCF.ModifyUser(u);
        }
    }
}
