using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caching;

namespace System.Caching.RefreshActions
{
    public class CallBackRefreshAction : ICacheItemRefreshAction
    {
        public void Refresh(string removedKey, object expiredValue, EnumRemovedReason removalReason)
        {
            switch (removedKey)
            { 
                case "UserInfo":
                    //BusinessManager
                    break;
            }
        }
    }
}
