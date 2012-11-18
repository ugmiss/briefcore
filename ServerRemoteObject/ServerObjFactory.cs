using System;
using System.Data;
using System.Configuration;
using Entities;
using ServerRemoteObjectInterface;

namespace ServerRemoteObject
{
    public class ServerObjFactory : MarshalByRefObject,IServerObjFactory
    {
        private int _connecttimes=0;

        public int Connecttimes
        {
            get { return _connecttimes; }
            set { _connecttimes = value; }
        }
        public IServerRemoteObject CreateInstance()
        {
            this.Connecttimes++;
            Console.WriteLine(Connecttimes);
            return new ServerRemoteObject();
        }

    }
}
