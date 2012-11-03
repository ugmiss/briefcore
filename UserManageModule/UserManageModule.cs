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
