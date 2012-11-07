using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Model;
using UserManageModule.View;

namespace UserManageModule
{
    //模块(模块名称，默认加载 OnDemand=false 不加载OnDemand=true）;
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
            //注册视图区域
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(UserInfoView));
            regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(UserInfoNavigationItemView));

            container.RegisterType<object, UserInfoView>("UserInfoView");
            container.RegisterType<object, UserInfoNavigationItemView>("UserInfoNavigationItemView");
            //注册导航
            //regionManager.RequestNavigate("MainRegion", "UserInfoView");
            //IRegion mainRegion = this.regionManager.Regions["MainRegion"];
            //mainRegion.Add(new UserInfoView());
        }

    }
}
