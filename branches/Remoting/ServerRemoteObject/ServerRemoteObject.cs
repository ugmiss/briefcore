using System;
using System.Data;
using System.Configuration;
using ServerRemoteObjectInterface;
using Entities;
namespace ServerRemoteObject
{
    public class ServerRemoteObject : MarshalByRefObject,IServerRemoteObject
    {
        public Person GetPersonInfo(int id,string name, string password, int age)
        {
            Person person = new Person(id,name,password,age);
            return person;
        }      
    }
}
