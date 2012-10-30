using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class WCFServiceHandler : IWCFService
    {
        IWCFServiceCallback callbackChannel = null;
        private void SetupCallback(IWCFServiceCallback callback)
        {
            if (callbackChannel == null)
            {
                callbackChannel = callback;
                BusinessManager.StatusChanged += new StatusChanged(BusinessManager_StatusChanged);
            }
        }

        void BusinessManager_StatusChanged()
        {
            DoCallbackInvoke(delegate()
            {
                callbackChannel.StateChanged("state");
            });
        }

        private void ReleaseCallback()
        {
            if (callbackChannel != null)
            {
                callbackChannel = null;

                BusinessManager.StatusChanged -= BusinessManager_StatusChanged;
            }
        }

        private void DoCallbackInvoke(ThreadStart action)
        {
            try
            {
                action();
            }
            catch (CommunicationObjectAbortedException)
            {
                ReleaseCallback();
            }
        }

        public string GetStatus()
        {
            return "";
        }
    }
}
