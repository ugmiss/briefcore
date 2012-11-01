using System.Windows;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
namespace ApplicationHost
{
    // 启动引导器
    public class Bootstrapper : UnityBootstrapper
    {



        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            DirectoryModuleCatalog directoryCatalog = new DirectoryModuleCatalog() { ModulePath = @"DirectoryModules" };
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

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }




    }
}
