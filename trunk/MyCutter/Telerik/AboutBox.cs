using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using Telerik.WinControls.UI;

namespace System
{
    /// <summary>
    /// ���ڴ��ڡ�
    /// </summary>
    partial class AboutBox : RadForm
    {
        /// <summary>
        /// ���췽����
        /// </summary>
        public AboutBox()
        {
            InitializeComponent();
            this.Text = string.Format("���ڣ�{0}", AssemblyTitle);
            this.radLabelProductName.Text =string.Format("��Ʒ���ƣ�{0}", AssemblyProduct);
            this.radLabelVersion.Text = string.Format("�汾��{0}", AssemblyVersion);
            this.radLabelCopyright.Text = string.Format("��Ȩ��{0}", AssemblyCopyright);
            this.radLabelCompanyName.Text =string.Format("��˾���ƣ�{0}",  AssemblyCompany);
            this.radTextBoxDescription.Text = string.Format("������{0}", AssemblyDescription);
        }
        /// <summary>
        /// ���⡣
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                // ��ȡ���е�Assembly���ԡ�
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        /// <summary>
        /// �汾��
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                    return "";
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
        /// <summary>
        /// ��Ʒ��
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                    return "";
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
        /// <summary>
        /// ��Ȩ��
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                    return "";
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        /// <summary>
        /// ��˾��
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                    return "";
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}
