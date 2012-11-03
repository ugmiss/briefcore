using System.Windows;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using DevExpress.Xpf.Core;
namespace ApplicationHost
{
    // 启动引导器
    public class ShellBootstrapper : UnityBootstrapper
    {



        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            // 直接注册方式
            // Type moduleAType = typeof(UserManageModule.UserManageModule);
            // ModuleCatalog.AddModule(new ModuleInfo(moduleAType.Name, moduleAType.AssemblyQualifiedName));

            // 物理目录方式加载模块。
            DirectoryModuleCatalog directoryCatalog = new DirectoryModuleCatalog() { ModulePath = @".\DirectoryModules" };
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(directoryCatalog);
            ConfigurationModuleCatalog configurationCatalog = new ConfigurationModuleCatalog();
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(configurationCatalog);

        }

        protected override IUnityContainer CreateContainer()
        {
            return base.CreateContainer();
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            ThemeManager.ApplicationThemeName = Theme.MetropolisDark.Name;
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Width = 1300.0;
            App.Current.MainWindow.Height = 730.0;
            //App.Current.MainWindow.MinWidth = 1000.0;
            //App.Current.MainWindow.MinHeight = 600.0;
            App.Current.MainWindow.Show();
        }




    }
}
