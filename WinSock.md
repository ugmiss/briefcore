## 在linux系统通过tcp传输字节流的时候 ##
byte位序列与window位序列是相反的，所以需要通过htonl转换
```
lpDevice->TimeStamp = htonl(locateDevices[i]->TimeStamp);
```