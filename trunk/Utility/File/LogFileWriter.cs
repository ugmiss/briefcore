using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.AccessControl;

namespace Utility
{
    /// <summary>
    /// 通用类。
    /// </summary>
    public class LogFileWriter
    {
        /// <summary>
        /// 日志文件。
        /// </summary>
        private string m_FileName = null;
        /// <summary>
        /// 日志目录。
        /// </summary>
        private string m_Directory = null;
        /// <summary>
        /// 日志完整路径。
        /// </summary>
        private string m_FullPath = null;
        /// <summary>
        /// 请输入日志文件地址。
        /// </summary>
        /// <param name="logDirectory">日志所在目录。</param>
        /// <param name="logFileName">日志文件名。</param>
        public LogFileWriter(string logDirectory,string logFileName)
        {
            m_Directory = logDirectory;
            m_FileName = logFileName;
            m_FullPath = Path.Combine(m_Directory, m_FileName);
        }
        /// <summary>
        /// 写异常到文件中。
        /// </summary>
        /// <param name="type">异常类。</param>
        /// <param name="message">错误消息。</param>
        /// <param name="exception">内部异常。</param>
        /// <param name="operatorName">错作员名称。</param>
        /// <param name="organizator">单位名称。</param>
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
            element.SetAttribute("DateTime", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
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
        /// 获取异常日志。
        /// </summary>
        /// <returns>返回xml文档。</returns>
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
