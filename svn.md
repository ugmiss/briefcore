## code.google无法获取SVN密码的解决办法 ##
在用SVN提交文件时会要求输入密码，密码从项目主页的source标签页中获取，找到如下的文字链接，When prompted, enter your generated googlecode.com password.
但是经常打不开，解决方法如下：

在文件 C:\Windows\System32\drivers\etc\hosts 中加入 74.125.71.139 code.google.com



问题描述：访问https://code.google.com/hosting/settings查看google code托管的svn 工程的密码，连接被重置。

问题原因：可能是code.google.com与其他一些google服务共享ip，根据code.google.com解析得到ip属于GFWED的ip，因此导致服务访问失败。（似乎也不对，直接访问https://code.google.com并不会被重置，只有查看密码会被重置。）

解决方法：修改本地dns设置，添加“74.125.71.139 code.google.com”记录。74.125.71.139为google.com的dns解析结果。

1. 打开命令行窗口，执行"ping google.com"，得到当前可用的Google ip“74.125.71.139”。

2. 找到本地的dns配置文件路径，windows 7 下为“C:\Windows\System32\drivers\etc\hosts”,

3. 复制该文件到桌面，编辑该文件并添加一行配置“74.125.71.139 code.google.com”，保存文件，将修改后的文件覆盖“C:\Windows\System32\drivers\etc\hosts”。（可绕过windows 7下直接修改“C:\Windows\System32\drivers\etc\hosts”文件后因为没有配置用户对hosts文件的权限而无法保存的问题）。

4. 在命令行窗口执行“ipconfig /flushdns”刷新本地缓存。

## SVN 代理 ##
C:\Users\用户名\AppData\Roaming\Subversion下的servers文件
对于googlecode.com域的svn使用代理
```
[groups]
group1 = *.googlecode.com
[group1]
http-proxy-host = 127.0.0.1
http-proxy-port = 8087
http-proxy-username = ugmiss@gmail.com
http-proxy-password = 密码
```


### svn迁入失败的问题 ###
svn是区分文件夹大小写的，在有权限的前提下：
如果大小写不对，迁出没问题，但迁入就会失败。