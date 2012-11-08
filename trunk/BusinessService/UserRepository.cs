using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;
using BusinessService.Data;

namespace BusinessService
{
    public class UserRepository
    {
        public UserInfo[] GetAllUserInfos()
        {
            List<User> list = new List<User>();
            list.Add(new User() { Name = "aaa", Pass = "111" });
            list.Add(new User() { Name = "bbb", Pass = "222" });
            list.Add(new User() { Name = "ccc", Pass = "33" });
            list.Add(new User() { Name = "ddd", Pass = "334" });
            Random r = new Random(DateTime.Now.Millisecond);
            return list.SelectMany<User, UserInfo>(p => from c in list select new UserInfo() { Name = p.Name, Age = r.Next(100) }).ToArray();
        }
    }
}
