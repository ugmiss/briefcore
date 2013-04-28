using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Log4netDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogInfo_Click(object sender, EventArgs e)
        {
            Logger.Instance.Info("消息：系统操作日志XXX");
        }

        private void btnLogError_Click(object sender, EventArgs e)
        {
            Logger.Instance.Error("错误：系统异常XXX");
        }

        private void btnLogDebug_Click(object sender, EventArgs e)
        {
            Logger.Instance.Debug("调试：调试信息XXX");
        }

        private void btLogWarn_Click(object sender, EventArgs e)
        {
            Logger.Instance.Warn("警告:逻辑错误或已知异常XXX");
        }

        private void btnLogFatal_Click(object sender, EventArgs e)
        {
            Logger.Instance.Fatal("严重错误：未捕获的异常XXX");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ThreadPool.QueueUserWorkItem(o =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(555);
            //        Logger.Info("测试信息");
            //    }
            //});
        }
    }
}
