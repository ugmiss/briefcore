using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using DomainModel;

namespace ReportingModule.View
{
    /// <summary>
    /// ReportNavigationItemView.xaml 的交互逻辑
    /// </summary>
    public partial class ReportNavigationItemView : UserControl
    {
        public IRegionManager regionManager;
        public ReportNavigationItemView(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            InitializeComponent();
        }
        private static Uri emailsViewUri = new Uri("ReportingView", UriKind.Relative);
        private void NavToReportBtn_Click(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.MainRegion, emailsViewUri);
        }
    }
}
