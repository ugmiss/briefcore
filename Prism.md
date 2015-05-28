![http://i.msdn.microsoft.com/dynimg/IC448610.png](http://i.msdn.microsoft.com/dynimg/IC448610.png)
## Modules ##
模块，多模块共同组成模块化系统的程序。
Module是一些逻辑上相关的程序集或者资源文件的集合，在Silverlight程序中通常以xap文件为单位存在。而每一个Module中都需要有一个负责进行初始化工作以及与系统进行集成的角色，它需要实现IModule接口。IModule接口中只有一个Initialize方法，一方面这个接口将这个工程标记为一个Module，另一方面你可以在Initialize方法中实现一些逻辑，比如向容器中注册一些Service，或者将视图集成到程序中等等。
![http://i.msdn.microsoft.com/dynimg/IC448639.png](http://i.msdn.microsoft.com/dynimg/IC448639.png)

![http://i.msdn.microsoft.com/dynimg/IC448642.png](http://i.msdn.microsoft.com/dynimg/IC448642.png)
#### 使用Unity方式 ####
```
using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Model;

namespace UserManageModule
{
    [Module(ModuleName = ModuleNames.UserManageModule, OnDemand = true)]
    public class UserManageModule : IModule
    {
        private readonly IRegionManager regionManager;
        public IUnityContainer container { get; set; }
        public UserManageModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("MainRegion", typeof(UserInfoView));
            //IRegion mainRegion = this.regionManager.Regions["MainRegion"];
            //mainRegion.Add(new UserInfoView());
        }
    }
}
```

## ModuleInfo ##
在创建了一个Module之后，需要通知Prism这个Module的存在，也就是要注册一下。在Prism中，Module是以ModuleInfo的形式存在的。ModuleInfo记录了Module的信息，ModuleName属性是Module的标识符，相当于Module的ID；ModuleType是Module的AssemblyQualifiedName；DependsOn属性是该Module依赖的其它Module的ModuleName的集合，在加载该Module时，如果有依赖项没有加载的话，会先将依赖项加载；InitializationMode，有两种情况——WhenAvailable和OnDemand，当选择了WhenAvailable时，该Module会在程序启动时自动加载，如果选择了OnDemand，则会按需加载，默认情况下是WhenAvailable；Ref，存储该Module的位置，如XXX.xap；State，定义了Module从注册到加载到初始化的整个过程中的状态。

## ModuleCatalog ##
ModuleCatalog实现了IModuleCatalog接口，它是ModuleInfo的容器，保存着系统中所有Module的信息，不仅会管理哪些Module需要加载，什么时候加载以什么顺序加载等问题，还要检查各个Module之间是否存在着循环依赖、是否有重复的Module等等。ModuleCatalog提供了含参构造方法和AddModule方法，可以通过代码将Module注册进去，同时也可以在xaml文件中配置好Module，然后通过ModuleCatalog.CreateFromXaml方法来加载。

## ModuleManager ##
ModuleManager实现了IModuleManager接口。顾名思义就是管理Module的类。IModuleManager中含有两个方法和两个事件：Run方法会将所有InitializationMode为WhenAvailable的Module加载，然后进行初始化，初始化的工作委托给了IModuleInitializer来完成，它会获取到Module类(上面提到的实现了IModule接口的类)的实例，然后调用其Initialize方法。LoadModule方法用来加载InitializationMode为OnDemand的Module。两个事件分别用来通知下载Module的进度变化以及Module加载完成。
#### 按名称手动加载 ####
```
this.moduleManager.LoadModule(ModuleNames.UserManageModule);
```
## Shell ##
宿主程序
## Region ##
## RegionManager ##
## Views ##
前端UI展现
## ViewModel或Presenters ##
ViewModel对控件的属性，命令，事件进行封装。
## Model ##
Model封装数据
## Commands ##
命令，方法
## Controllers ##

## Bootstrapper ##
框架会在这里进行初始化，处理相关配置信息等
Prism提供了一个抽象基类Bootstrapper，这个类里面包含了包含了许多空的虚方法，可以重写它们添加自己的逻辑。Prism默认提供了两个基于特定容器的Bootstrapper——`UnityBootstrapper`和`MefBootstrapper`，分别使用Unity和Mef来实现依赖注入。
![http://i.msdn.microsoft.com/dynimg/IC448593.png](http://i.msdn.microsoft.com/dynimg/IC448593.png)
```
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
```
## 架构图 ##
![http://i.msdn.microsoft.com/dynimg/IC448577.png](http://i.msdn.microsoft.com/dynimg/IC448577.png)
## 设计模式 ##
http://msdn.microsoft.com/en-us/library/ff921146(v=pandp.40).aspx

