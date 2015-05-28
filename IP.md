# 获取本机IP #
```
string GetHostIP()
{
    string hostIP = string.Empty;
    foreach (IPAddress ipAddr in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
    {
        if (ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        {
            hostIP = ipAddr.ToString();
            break;
        }
    }
    return hostIP;
}

```