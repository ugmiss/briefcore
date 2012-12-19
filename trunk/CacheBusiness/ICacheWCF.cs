using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CacheBusiness.Model;

namespace CacheBusiness
{

    [ServiceContract(Namespace = "http://www.hujao.com/", SessionMode = SessionMode.Required, CallbackContract = typeof(ICacheRefreshCallback))]
    public interface ICacheWCF
    {
        [OperationContract(IsOneWay = false)]
        UserInfo[] GetUserData();
        [OperationContract(IsOneWay = true)]
        void AddUser(UserInfo u);
        [OperationContract(IsOneWay = true)]
        void DeleteUser(UserInfo u);
        [OperationContract(IsOneWay = true)]
        void ModifyUser(UserInfo u);
    }
}
