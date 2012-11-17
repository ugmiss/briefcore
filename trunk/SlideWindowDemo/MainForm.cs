using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SlideWindowDemo
{
    public partial class MainForm : Form
    {
        const string page0Text = @"方案概要：
    采用滑动窗口机制，把分区表的分区依次移入到另一个分区表。
分区初始工作：
    创建新库或已有库追加分区文件组（File Group），
    创建分区函数（Partition Function），
    创建分区方案（Partition Scheme），
    创建或修改分区表（Partition Table）；
滑动归档工作：
    分区表分区切换（Partition Swtich）,
    临时表归档并清理（Achieve & Clean Up）,
    分区函数合并（Partition Merge）,
    拆分（Partition Split）";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            welcomeWizardPage1.IntroductionText = page0Text;
        }
    }
}
