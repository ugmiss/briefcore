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
    /// 关于窗口。
    /// </summary>
    partial class AboutBox : RadForm
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public AboutBox()
        {
            InitializeComponent();
            this.Text = string.Format("关于：{0}", AssemblyTitle);
            this.radLabelProductName.Text =string.Format("产品名称：{0}", AssemblyProduct);
            this.radLabelVersion.Text = string.Format("版本：{0}", AssemblyVersion);
            this.radLabelCopyright.Text = string.Format("版权：{0}", AssemblyCopyright);
            this.radLabelCompanyName.Text =string.Format("公司名称：{0}",  AssemblyCompany);
            this.radTextBoxDescription.Text = string.Format("描述：{0}", AssemblyDescription);
        }
        /// <summary>
        /// 标题。
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                // 获取所有的Assembly属性。
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
        /// 版本。
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        /// <summary>
        /// 描述。
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
        /// 产品。
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
        /// 版权。
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
        /// 公司。
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
