using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.AccessControl;

namespace Utility
{
    /// <summary>
    /// ͨ���ࡣ
    /// </summary>
    public class LogFileWriter
    {
        /// <summary>
        /// ��־�ļ���
        /// </summary>
        private string m_FileName = null;
        /// <summary>
        /// ��־Ŀ¼��
        /// </summary>
        private string m_Directory = null;
        /// <summary>
        /// ��־����·����
        /// </summary>
        private string m_FullPath = null;
        /// <summary>
        /// ��������־�ļ���ַ��
        /// </summary>
        /// <param name="logDirectory">��־����Ŀ¼��</param>
        /// <param name="logFileName">��־�ļ�����</param>
        public LogFileWriter(string logDirectory,string logFileName)
        {
            m_Directory = logDirectory;
            m_FileName = logFileName;
            m_FullPath = Path.Combine(m_Directory, m_FileName);
        }
        /// <summary>
        /// д�쳣���ļ��С�
        /// </summary>
        /// <param name="type">�쳣�ࡣ</param>
        /// <param name="message">������Ϣ��</param>
        /// <param name="exception">�ڲ��쳣��</param>
        /// <param name="operatorName">����Ա���ơ�</param>
        /// <param name="organizator">��λ���ơ�</param>
        public void WriteExceptionLog(string type,string message,Exception exception,string operatorName,string organizator)
        {
            XmlDocument doc = GetExceptionLog();
            XmlElement root = doc.DocumentElement;
            XmlElement element = doc.CreateElement(type);
            if (!string.IsNullOrEmpty(operatorName))
            {
                element.SetAttribute("Operator", operatorName);
            }
            if (!string.IsNullOrEmpty(organizator))
            {
                element.SetAttribute("Organazition", organizator);
            }
            element.SetAttribute("DateTime", DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss"));
            XmlElement xmlMessage = doc.CreateElement("message");
            xmlMessage.InnerText = message;
            element.AppendChild(xmlMessage);
            if (exception != null)
            {
                XmlElement xmlExp = doc.CreateElement("Exception");
                xmlExp.InnerText = exception.ToString();
                element.AppendChild(xmlExp);
            }
            if (root.HasChildNodes)
                root.InsertBefore(element, root.FirstChild);
            else
                root.AppendChild(element);
            doc.Save(m_FullPath);
        }
        /// <summary>
        /// ��ȡ�쳣��־��
        /// </summary>
        /// <returns>����xml�ĵ���</returns>
        private XmlDocument GetExceptionLog()
        {
            if (!Directory.Exists(m_Directory))
            {
                if (!Directory.Exists(m_Directory))
                {
                    Directory.CreateDirectory(m_Directory);
                }
            }
            DirectoryInfo dirInfo = new DirectoryInfo(m_Directory);
            DirectorySecurity dirSecurity = dirInfo.GetAccessControl();
            try
            {
                dirSecurity.AddAccessRule(new FileSystemAccessRule("ASPNET", FileSystemRights.Write,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                dirSecurity.AddAccessRule(new FileSystemAccessRule("IIS_WPG", FileSystemRights.Write,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                dirInfo.SetAccessControl(dirSecurity);
            }
            catch
            {
            }
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(m_FullPath))
            {
                XmlElement root = doc.CreateElement("Exception");
                doc.AppendChild(root);
            }
            else
            {
                doc.Load(m_FullPath);
            }
            return doc;
        }
    }
}
