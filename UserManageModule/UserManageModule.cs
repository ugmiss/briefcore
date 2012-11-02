using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Model;
using Microsoft.Practices.Unity;

namespace UserManageModule
{
    [Module(ModuleName = ModuleNames.UserManageModule, OnDemand = false)]
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
            IRegion mainRegion = this.regionManager.Regions["MainRegion"];
            mainRegion.Add(new UserInfoView());
        }

    }
}
