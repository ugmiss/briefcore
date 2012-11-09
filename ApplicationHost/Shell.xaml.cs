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
using DomainModel;
using DevExpress.Xpf.Core;
using System.Windows.Media.Animation;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace ApplicationHost
{
    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        [Import(AllowRecomposition = false)]

        IModuleManager moduleManager;
        [Import(AllowRecomposition = false)]

        IRegionManager regionManager;
        public Shell()
        { }
        public Shell(IRegionManager regionManager, IModuleManager moduleManager)
        {
            if (moduleManager == null)
            {
                throw new ArgumentNullException("moduleManager");
            }
            this.moduleManager = moduleManager;
            this.regionManager = regionManager;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.moduleManager.LoadModule(ModuleNames.ReportingModule);
            //this.moduleManager.LoadModule(ModuleNames.UserManageModule);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Uri uri = new Uri("UserInfoView", UriKind.Relative);
        //    this.moduleManager.LoadModule(ModuleNames.UserManageModule);
        //    regionManager.RequestNavigate("MainRegion", uri);
        //    Story();
        //}
        ////public void Story()
        ////{
        ////    DoubleAnimation myani = new DoubleAnimation(); //实例化一个DoubleAninmation对象
        ////    myani.From = App.Current.MainWindow.Width;//开始值
        ////    myani.To = 0;//结束值
        ////    myani.Duration = TimeSpan.FromSeconds(0.8); //所用时间
        ////    Storyboard.SetTarget(myani, this.MainRegion);  //设置应用的对象
        ////    Storyboard.SetTargetProperty(myani, new PropertyPath("(Canvas.Left)"));  //设置应用的依赖项属性
        ////    Storyboard s = new Storyboard();// 实例化一个故事板
        ////    s.Children.Add(myani);//将先前动画添加进来
        ////    s.Begin(); //启动
        ////}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    Uri uri = new Uri("ReportingView", UriKind.Relative);
        //    regionManager.RequestNavigate("MainRegion", uri);

        //    Story();
        //}
    }
}
