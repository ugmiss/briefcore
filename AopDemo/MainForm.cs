using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spring.Aop.Framework;

namespace AopDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "admin";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "eo";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "mo";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //创建SpringAop代理工厂
            ProxyFactory proxyFactory = new ProxyFactory(new DataProvider());
            //添加通知
            proxyFactory.AddAdvice(new DataFilterAdvice());
            proxyFactory.AddAdvice(new AuthVarifyAdvice());
            proxyFactory.AddAdvice(new LoggingAdvice());
            proxyFactory.AddAdvice(new ExceptionAdvice());
            //动态代理接口
            IDataProvider idata = (IDataProvider)proxyFactory.GetProxy();
            try
            {
                List<string> res = idata.GetData();
                MessageBox.Show(string.Join("\n", res));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "lo";
        }
    }
}
