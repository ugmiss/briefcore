using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace UserManageModule
{

    public class UserManageModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;

        public UserManageModule(IRegionViewRegistry registry)
        {
            this.regionViewRegistry = registry;
        }

        public void Initialize()
        {
            regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof(RoleView));
        }

    }
}
