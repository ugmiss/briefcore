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
using Microsoft.Practices.Prism.Modularity;
using UserManageModule.ViewModel;

namespace UserManageModule
{
    /// <summary>
    /// RoleView.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoView : UserControl
    {
        UserInfoViewModel ViewModel = new UserInfoViewModel();
        public UserInfoView()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
