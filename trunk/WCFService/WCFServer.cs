using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ServiceModel;

namespace WCFService
{
    public class WCFServer
    {
        private static WCFServer instance = new WCFServer();
        public static WCFServer Instance
        {
            get { return instance; }
        }

        private WCFServer()
        {
        }

        private bool isRunning = false;
        private ServiceHost serviceHost = null;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
            private set { isRunning = value; }
        }

        /// <summary>
        /// 启动客户端服务
        /// </summary>
        /// <returns>返回true表示启动成功</returns>
        public bool Start()
        {
            if (this.IsRunning)
                return true;

            serviceHost = new ServiceHost(typeof(WCFServiceHandler));

            try
            {
                serviceHost.Open();
                this.IsRunning = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                serviceHost = null;
            }

            return this.IsRunning;
        }

        /// <summary>
        /// 停止客户端服务
        /// </summary>
        public void Stop()
        {
            if (!this.IsRunning)
                return;

            if (serviceHost != null)
            {
                try
                {
                    serviceHost.Abort();
                    serviceHost.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    serviceHost = null;
                    this.IsRunning = false;
                }
            }
        }
    }
}
