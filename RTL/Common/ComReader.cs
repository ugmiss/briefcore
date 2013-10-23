using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReaderB;

namespace Common
{
    /// <summary>
    /// functions for D2180 COM reader
    /// </summary>
    public static class ComReader
    {
        public static bool ComOpen = false;
        public static byte fBaud;
        public static int openresult = 30;
        public static byte fComAdr;
        public static int frmcomportindex = 0, fOpenComIndex, fCmdRet, ferrorcode = 0;

        public static void openComPort()
        {

            string comPort = null;
            try
            {
                comPort = ConfigManager.Get(SystemKeys.ComPort);
                //MessageBox.Show(comPort);
                int port = Convert.ToInt32(comPort.Substring(3, comPort.Length - 3));
                fComAdr = Convert.ToByte("FF", 16); // $FF;
                //MainSettingForm.ComOpen = false;
                for (int i = 6; i >= 0; i--)
                {
                    fBaud = Convert.ToByte(i);
                    if (fBaud == 3)
                        continue;
                    openresult = StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    /*
                    if (openresult == 0x35)
                    {
                        //MessageBox.Show("串口已打开", "信息提示");
                        ComOpen = true;
                        return;
                    }
                     */
                    if (openresult == 0)
                    {
                        //MainSettingForm.ComOpen = true;
                        byte[] TrType = new byte[2];
                        byte[] VersionInfo = new byte[2];
                        byte ReaderType = 0;
                        byte ScanTime = 0;
                        byte dmaxfre = 0;
                        byte dminfre = 0;
                        byte powerdBm = 0;

                        fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime, frmcomportindex);
                        if ((fCmdRet == 0x35) || (fCmdRet == 0x30))
                        {
                            //MainSettingForm.ComOpen = false;
                            MessageBox.Show("串口通讯错误", "信息提示");
                            StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                            return;
                        }
                        
                        if ( fCmdRet == 0 )
                        {
                            ComOpen = true;
                        }
                        else //show message when read failed
                        {
                            MessageBox.Show(GetReturnCodeDesc(fCmdRet));
                        }
                        break;
                    }

                }
            }
            catch (System.Exception ex)
            {
                ComOpen = false;
                MessageBox.Show("打开串口失败！" + ex.Message);
            }

        }

        public static void closeComPort()
        {
            string comPort = ConfigManager.Get(SystemKeys.ComPort);
            int port = Convert.ToInt32(comPort.Substring(3, comPort.Length - 3));
            try
            {
                int fCmdRet = StaticClassReaderB.CloseSpecComPort(port);
                if (fCmdRet == 0)
                {
                    //MessageBox.Show(comPort + " is closed!");
                    System.Console.WriteLine(comPort + " is closed!");
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("******************************" + ex.Message);
            }

        }


        public static bool WriteEPC_G2(string epcID)
        {
            byte[] WriteEPC = new byte[100];
            byte[] fPassWord = new byte[4];
            byte WriteEPClen;
            bool bFlag = false;
            try
            {
                if (epcID.Length % 2 == 1)
                {
                    epcID = "0" + epcID; //add 0 if the lenght is odd
                }
                //convert to byte[]
                ASCIIEncoding AE1 = new ASCIIEncoding();

                byte[] EPC = AE1.GetBytes(epcID);


                WriteEPClen = Convert.ToByte(EPC.Length);
                //ENum = Convert.ToByte(epcID.Length / 4);
                //byte[] EPC = new byte[ENum];
                //EPC = HexStringToByteArray(epcID);
                fPassWord = HexStringToByteArray("00000000");
                fCmdRet = StaticClassReaderB.WriteEPC_G2(ref fComAdr, fPassWord, EPC, WriteEPClen, ref ferrorcode, frmcomportindex);

                if (fCmdRet == 0)
                {
                    bFlag = true;
                    MessageBox.Show("写入芯片成功：" + epcID);
                }
                else
                {
                    MessageBox.Show(GetReturnCodeDesc(fCmdRet));
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeComPort();
                // reopen COM port
                openComPort();
                bFlag = false;
            }

            return bFlag;
        }


        public static string Inventory()
        {
            int CardNum = 0;
            int Totallen = 0;
            int EPClen, m;
            byte[] EPC = new byte[5000];
            int CardIndex;
            string sAllContent, sEPC = "";
            byte AdrTID = 0;
            byte LenTID = 0;
            byte TIDFlag = 0;
            
            try
            {
                fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum, frmcomportindex);
                if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))//代表已查找结束，
                {
                    byte[] daw = new byte[Totallen];
                    Array.Copy(EPC, daw, Totallen);

                    ASCIIEncoding AE2 = new ASCIIEncoding();
                    char[] CharArray = AE2.GetChars(daw);
                    sAllContent = new string(CharArray);

                    m = 0;
                    if (CardNum == 0)
                    {
                        MessageBox.Show("没有读到标签！");
                        return "";
                    }
                    for (CardIndex = 0; CardIndex < CardNum; CardIndex++)
                    {
                        EPClen = daw[m];
                        sEPC += sAllContent.Substring(m + 1, EPClen) + ";";
                        m = m + EPClen + 1;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            	//reopen COM port
                closeComPort();
                openComPort();
            }

            return sEPC;
        }


        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        public static string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "操作成功";
                case 0x01:
                    return "询查时间结束前返回";
                case 0x02:
                    return "指定的询查时间溢出";
                case 0x03:
                    return "本条消息之后，还有消息";
                case 0x04:
                    return "读写模块存储空间已满";
                case 0x05:
                    return "访问密码错误";
                case 0x09:
                    return "销毁密码错误";
                case 0x0a:
                    return "销毁密码不能为全0";
                case 0x0b:
                    return "电子标签不支持该命令";
                case 0x0c:
                    return "对该命令，访问密码不能为全0";
                case 0x0d:
                    return "电子标签已经被设置了读保护，不能再次设置";
                case 0x0e:
                    return "电子标签没有被设置读保护，不需要解锁";
                case 0x10:
                    return "有字节空间被锁定，写入失败";
                case 0x11:
                    return "不能锁定";
                case 0x12:
                    return "已经锁定，不能再次锁定";
                case 0x13:
                    return "参数保存失败,但设置的值在读写模块断电前有效";
                case 0x14:
                    return "无法调整";
                case 0x15:
                    return "询查时间结束前返回";
                case 0x16:
                    return "指定的询查时间溢出";
                case 0x17:
                    return "本条消息之后，还有消息";
                case 0x18:
                    return "读写模块存储空间已满";
                case 0x19:
                    return "电子不支持该命令或者访问密码不能为0";
                case 0xFA:
                    return "有电子标签，但通信不畅，无法操作";
                case 0xFB:
                    return "无电子标签可操作";
                case 0xFC:
                    return "电子标签返回错误代码";
                case 0xFD:
                    return "命令长度错误";
                case 0xFE:
                    return "不合法的命令";
                case 0xFF:
                    return "参数错误";
                case 0x30:
                    return "通讯错误";
                case 0x31:
                    return "CRC校验错误";
                case 0x32:
                    return "返回数据长度有错误";
                case 0x33:
                    return "通讯繁忙，设备正在执行其他指令";
                case 0x34:
                    return "繁忙，指令正在执行";
                case 0x35:
                    return "端口已打开";
                case 0x36:
                    return "端口已关闭";
                case 0x37:
                    return "无效句柄";
                case 0x38:
                    return "无效端口";
                case 0xEE:
                    return "返回指令错误";
                default:
                    return "";
            }
        }
        public static string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "其它错误";
                case 0x03:
                    return "存储器超限或不被支持的PC值";
                case 0x04:
                    return "存储器锁定";
                case 0x0b:
                    return "电源不足";
                case 0x0f:
                    return "非特定错误";
                default:
                    return "";
            }
        }

    }
}
