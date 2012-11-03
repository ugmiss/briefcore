using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Modularity;
using Model;
using DevExpress.Xpf.Core;
using System.Windows.Media.Animation;

namespace ApplicationHost
{
    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : DXWindow
    {
        IModuleManager moduleManager;

        public Shell()
        { }
        public Shell(IModuleManager moduleManager)
        {
            if (moduleManager == null)
            {
                throw new ArgumentNullException("moduleManager");
            }
            this.moduleManager = moduleManager;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.moduleManager.LoadModule(ModuleNames.UserManageModule);
            DoubleAnimation myani = new DoubleAnimation(); //实例化一个DoubleAninmation对象

            //this.MainRegion.Margin = new Thickness(600, 90, -600, 90);
            myani.From = 0;//开始值
            myani.To = 1000;//结束值
            myani.Duration = TimeSpan.FromSeconds(0.8); //所用时间
            Storyboard.SetTarget(myani, this.MainRegion);  //设置应用的对象
            Storyboard.SetTargetProperty(myani, new PropertyPath("Width"));  //设置应用的依赖项属性
            Storyboard s = new Storyboard();// 实例化一个故事板
            //s.Completed += new EventHandler(s_Completed);
            s.Children.Add(myani);//将先前动画添加进来
            s.Begin(); //启动故事版
        }
    }
}
