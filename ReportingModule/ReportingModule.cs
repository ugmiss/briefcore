using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using DomainModel;
using ReportingModule.View;

namespace ReportingModule
{
    //模块(模块名称，默认加载 OnDemand=false 不加载OnDemand=true）;
    [Module(ModuleName = ModuleNames.ReportingModule, OnDemand = false)]
    public class ReportingModule : IModule
    {
        private readonly IRegionManager regionManager;
        public IUnityContainer container { get; set; }
        public ReportingModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {

            //注mainRegion.Add(new UserInfoView());册视图区域
            regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ReportNavigationItemView));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ReportingView));
            //注册导航
            //regionManager.RequestNavigate("", "");
            //
            container.RegisterType<object, ReportNavigationItemView>("ReportNavigationItemView");
            container.RegisterType<object, ReportingView>("ReportingView");
            //注册导航
            regionManager.RequestNavigate(RegionNames.MainNavigationRegion, "ReportNavigationItemView");

            //
        }
    }
}
