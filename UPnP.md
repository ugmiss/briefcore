
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;  //提取IP时的正则
using System.Threading.Tasks;          //Task
using System.IO;                       //读取服务器信息用到StreamReader
using NATUPNPLib;                      //Windows UPnP COM组件

namespace mgen_upnp
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取Host Name
            var name = Dns.GetHostName();
            Console.WriteLine("用户：" + name);
            //从当前Host Name解析IP地址，筛选IPv4地址是本机的内网IP地址。
            var ipv4 = Dns.GetHostEntry(name).AddressList.Where(i => i.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            Console.WriteLine("内网IP：" + ipv4);


            Console.WriteLine("设置UPnP");
            //UPnP绑定信息
            var eport = 8733;
            var iport = 8733;
            var description = "NAT测试";

            //创建COM类型
            var upnpnat = new UPnPNAT();
            var mappings = upnpnat.StaticPortMappingCollection;

            //错误判断
            if (mappings == null)
            {
                Console.WriteLine("没有检测到路由器，或者路由器不支持UPnP功能。");
                return;
            }

            //添加之前的ipv4变量（内网IP），内部端口，和外部端口
            mappings.Add(eport, "TCP", iport, ipv4.ToString(), true, description);

            Console.WriteLine("外部端口：{0}", eport);
            Console.WriteLine("内部端口：{0}", iport);

            //外网IP变量
            string eip;
            //正则
            var regex = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
            using (var webclient = new WebClient())
            {
                var rawRes = webclient.DownloadString("http://checkip.dyndns.org/");
                eip = Regex.Match(rawRes, regex).Value;
            }

            Console.WriteLine("外网IP：" + eip);

            //在NAT下的服务器
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定内网IP和内部端口
            socket.Bind(new IPEndPoint(ipv4, iport));
            socket.Listen(1);
            //在另一个线程中运行客户端Socket
            Task.Factory.StartNew(() =>
                {
                    //TaskDelay (1000).Wait();
                    ClientSocket(eip, eport);
                });

            //成功连接
            var client = socket.Accept();
            //服务器向客户端发送信息
            client.Send(Encoding.Unicode.GetBytes("=== 欢迎来到Ace的服务器！===" + Environment.NewLine));

            Console.ReadKey(false);
        }

        //ip参数和port参数是公网的IP地址，和UPnP中的外部端口
        static void ClientSocket(string ip, int port)
        {
            try
            {
                Console.WriteLine("建立客户端TCP连接");
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                using (var ns = new NetworkStream(socket))
                using (var sr = new StreamReader(ns, Encoding.Unicode))
                {
                    Console.WriteLine("收到来自服务器的回应：");
                    Console.WriteLine(sr.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
```