﻿using System;
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

namespace UserManageModule
{
    /// <summary>
    /// RoleView.xaml 的交互逻辑
    /// </summary>
    public partial class RoleView : UserControl
    {
        IModuleManager moduleManager;
        public RoleView(IModuleManager moduleManager)
        {
            InitializeComponent();
            this.moduleManager = moduleManager;
        }
    }
}
