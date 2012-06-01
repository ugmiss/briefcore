using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace TopTools1._0
{
    /// <summary>
    /// 注册表操作。
    /// </summary>
    public class RegEdit
    {
        //string key=@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Services\SQL Server";
        /// <summary>
        /// 根据路径取key.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static RegistryKey GetKey(string dir)
        {
            string[] dirs = dir.Split("\\".ToCharArray());
            RegistryKey root = null;
            switch (dirs[0])
            {
                case "HKEY_LOCAL_MACHINE":
                    root = Registry.LocalMachine;
                    break;
                case "HKEY_CLASSES_ROOT":
                    root = Registry.ClassesRoot;
                    break;
                case "HKEY_CURRENT_USER":
                    root = Registry.CurrentUser;
                    break;
                case "HKEY_USERS":
                    root = Registry.Users;
                    break;
                case "HKEY_CURRENT_CONFIG":
                    root = Registry.CurrentConfig;
                    break;
            }
            RegistryKey temp = root;
            for (int i = 1; i < dirs.Length; i++)
            {
                temp = temp.OpenSubKey(dirs[i]);
            }
            return temp;
        }


        public static string[] GetKeyValue(string keydir, string name)
        {
            RegistryKey key = GetKey(keydir);
            return key.GetValue(name) as string[];
        }

        //.读取指定名称的注册表的值 
        private string GetRegistData(string name)
        {
            string registData;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("XXX", true);
            registData = aimdir.GetValue(name).ToString();
            return registData;
        }
        //以上是读取的注册表中HKEY_LOCAL_MACHINE\SOFTWARE目录下的XXX目录中名称为name的注册表值； 

        //2.向注册表中写数据 
        private void WTRegedit(string name, string tovalue)
        {
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.CreateSubKey("XXX");
            aimdir.SetValue(name, tovalue);
        }
        //以上是在注册表中HKEY_LOCAL_MACHINE\SOFTWARE目录下新建XXX目录并在此目录下创建名称为name值为tovalue的注册表项； 

        //3.删除注册表中指定的注册表项 
        private void DeleteRegist(string name)
        {
            string[] aimnames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("XXX", true);
            aimnames = aimdir.GetSubKeyNames();
            foreach (string aimKey in aimnames)
            {
                if (aimKey == name)
                    aimdir.DeleteSubKeyTree(name);
            }
        }
        //以上是在注册表中HKEY_LOCAL_MACHINE\SOFTWARE目录下XXX目录中删除名称为name注册表项； 

        //4.判断指定注册表项是否存在 
        private bool IsRegeditExit(string name)
        {
            bool _exit = false;
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("XXX", true);
            subkeyNames = aimdir.GetSubKeyNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == name)
                {
                    _exit = true;
                    return _exit;
                }
            }
            return _exit;
        }
    }
}
