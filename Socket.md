### 客户端Client ###
```
Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint epServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1999);
clientSock.Connect(epServer);
clientSock.Send(Encoding.ASCII.GetBytes("Hello!"));
clientSock.Shutdown(SocketShutdown.Send);
clientSock.Close();
```
### 服务端Server ###
```
// 简单Socket异步
class SimpleSocket
{
    public Socket TcpSocket = null; //这里定义Public级别是为在类外访问
    public byte[] bb = new byte[1024];
    int num = 0;
    LocateManagementMainForm thisform = null;
    public SimpleSocket(string ip, int port)
    {
        Ip = ip;
        Port = port;
    }
    public string Ip { get; set; }
    public int Port { get; set; }
    public void GetSocket(LocateManagementMainForm form)
    {
        thisform = form;
        //用Parse方法转换字符
        IPAddress ipname = IPAddress.Parse(Ip);
        //设置终结点，为以后Socket绑定做准备
        IPEndPoint iphost = new IPEndPoint(ipname, Port);
        //我用Tcp协议，所以用SocketType.Stream，如果你用Udp,那么请用SocketType.Dgram
        Socket mysock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        mysock.Bind(iphost);　//绑定，这是Socket基本用法
        mysock.Listen(100);   //开始监听
        //这个异步表示如果mysock一旦发现有客户断连接，那么异步方法将会被立即调用
        //有很多网友称之为“启动后不管”
        mysock.BeginAccept(new AsyncCallback(Shu), mysock);
    }
    public void Shu(IAsyncResult e)
    {
        Socket s = (Socket)e.AsyncState;//这里是异步的通用做法,用异步接口的AsyncState返回自身
        TcpSocket = s.EndAccept(e);//这里是接收的真正的异步Socket对象
        //下面继续用异步接收信息，之所以用异步接收，是因为程序
        //运行在UI里面，以妨程序出现假死状态，后面我将会用线程来控制这个问题
        AsyncCallback mm = new AsyncCallback(GetShu);
        TcpSocket.BeginReceive(bb, 0, bb.Length, SocketFlags.None, mm, TcpSocket);//这里把自身做为参数给下个异步方法调用
        s.BeginAccept(new AsyncCallback(Shu), s);
    }
    public delegate void MyDeleTe(string txt);
    public void GetShu(IAsyncResult e)
    {
        Socket list = (Socket)e.AsyncState;
        num = list.EndReceive(e);
        string message = "";
        if (num > 0)
        {
            message = Encoding.ASCII.GetString(bb, 0, num);//　message是返回的Client发来的信息
            if (message.NotNullOrEmpty())
                thisform.ShowLocateCardControl(message);
        }
    }
}
```