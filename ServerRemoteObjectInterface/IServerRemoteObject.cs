using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace ServerRemoteObjectInterface
{
    public interface IServerRemoteObject 
    {
        Person GetPersonInfo(int id,string name,string password,int age);

    }
}
