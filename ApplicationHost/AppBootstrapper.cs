using System.Windows;
using DevExpress.Xpf.Core;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
namespace ApplicationHost
{
    // 应用程序启动引导
    public class AppBootstrapper : UnityBootstrapper
    {
        // 1.创建模块目录
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }
        // 2.配置模块目录
        protected override void ConfigureModuleCatalog()
        {
            // 直接注册方式
            // Type moduleAType = typeof(UserManageModule.UserManageModule);
            // ModuleCatalog.AddModule(new ModuleInfo(moduleAType.Name, moduleAType.AssemblyQualifiedName));
            // 物理目录方式 加载模块
            DirectoryModuleCatalog directoryCatalog = new DirectoryModuleCatalog() { ModulePath = @".\DirectoryModules" };
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(directoryCatalog);
            // 配置文件方式 加载模块
            ConfigurationModuleCatalog configurationCatalog = new ConfigurationModuleCatalog();
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(configurationCatalog);

        }
        // 3.创建Unity IOC容器
        protected override IUnityContainer CreateContainer()
        {
            return base.CreateContainer();
        }
        // 4.配置容器
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }
        // 5.创建程序Shell
        protected override DependencyObject CreateShell()
        {
            // Resolve解析
            return this.Container.Resolve<Shell>();
        }
        // .初始化Shell
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
