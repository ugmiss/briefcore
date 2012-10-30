using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFService
{
    public delegate void StatusChanged();
    public class BusinessManager
    {
        public static event StatusChanged StatusChanged;
        public bool Startup()
        {
            BusinessManager.StatusChanged += new WCFService.StatusChanged(BusinessManager_StatusChanged);
            return true;
        }

        void BusinessManager_StatusChanged()
        {
            //DO Something
        }
    }
}
