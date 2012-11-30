﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFContract
{
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void BroadCast(string msg);
    }
}
