|audiodg.exe|windows音频设备管理器  可以结束但是一旦打开音频文件还是会重新加载|
|:----------|:--------------------------------------------------------------------------------------------|
|csrss.exe  |是微软客户端、服务端运行时子系统，管理Windows图形相关任务  可以被强制结束但是会蓝屏 数量为2个|
|dwm.exe    |桌面窗口管理器 可以被结束，结束无法显示win7 Aero效果，无法重新新建任务还原,打开Services.msc禁用Desktop Window Manager |
|explorer.exe|windows任务管理器 可以被结束但是任务栏和桌面图标会消失掉 可以重新新建|
|lsass.exe  |lsass.exe是一个系统进程，用于本地安全授权.关闭该进程会系统关闭防火墙同时系统会1分钟后重启|
|lsm.exe    | 本地会话管理器服务，关闭该进程系统会1分钟后重启（所谓本地会话个人理解应该是本地用户或者程序提出一系列请求，系统调用相应服务与之响应|
|services.exe|服务和控制器应用 用于管理启动和停止服务  关闭该进程系统会1分钟后重启|
|spoolsv.exe|后台处理程序子系统应用程序 管理所有打印工作 可以关闭                |
|svchost.exe|windows服务主进程 有很多个（大约9-13个具体看加载的服务） window很多服务都要用到该进程  local的可以关闭 ，其他关闭自动回复|
|system     |windows页面内存管理进程  无法关闭                                                |
|system Idle Process|系统虚拟进程 显示cpu空闲占有率  无法关闭                                    |
|ssms       |window对话管理器，关闭会重启                                                      |
|taskhost.exe|win7计划任务程序 可以被结束 但是定时任务就会失效                        |
|taskmgr.exe|按ctrl+shift+esc调出的任务管理器                                                    |
|wininit.exe|windows启动初始化进程 会启动services.exe                                           |
|winlogon.exe|windows用户登录管理器  结束该进程导致只有桌面背景和鼠标指针，无法进行任何其他操作|
|WMIADAP.exe|用于通过WinMgmt.exe程序处理WMI操作 开机一段时间会消失                     |
|WmiPrvSE.exe|微软Windows操作系统的一部分。用于通过WinMgmt.exe程序处理WMI操作(不知道Wmi是啥,应该是管理工具类的）数量为2个，开机一段时间会消失|
|WUDFHost.exe|windows驱动程序基础，查了网上资料应该是当连接新设备时，该进程会自动调用驱动，无法被结束。|

以上是win7基本的进程 如果还有多出来的系统进程可以根据描述关闭相关的服务。对于程序进程则可以右击该进程，查看文件位置是否是你安装的程序，不需要的也可以去掉。如果类似系统进程的，当文件位置又不是在system32下的 那很可能就是病毒.

|CDASrv|Common Desktop Agent msconfig中禁用|
|:-----|:-------------------------------------|
|wmpnetwk.exe|用户名为NETWORK SERVICE.Windows Media Player Network Sharing Service:为其他网络播放器或者使用UPnP(Universal Plug and Play，通用即插即用)标准的媒体设备共享Windows Media Player的媒体库。该服务的默认运行方式是手动，不过如果你没有这类设备或者应用的话则可以放心将其禁用。 |
|hkcmd,igfxtray.exe|intel集成显卡诊断程序 msconfig中禁用|
|FlashUtilXXXXXX|Adobe更新程序，跟随IE启动启动，在IE高级设置中禁用加载项|
|imeutil|sogou输入法，将 启用细胞词库自动更新 和 开启流行新词更新 两个选项的复选框的勾去掉，然后 应用--确定 就可以了。 |
|sogoucloud|sogou输入法，关闭云计算      |
|Ssms  |Sqlserver Manager Studio，SqlServer客户端|