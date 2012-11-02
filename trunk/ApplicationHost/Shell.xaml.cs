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
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Modularity;
using Model;

namespace ApplicationHost
{
    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : Window
    {
        IModuleManager moduleManager;

        public Shell()
        { }
        public Shell(IModuleManager moduleManager)
        {
            if (moduleManager == null)
            {
                throw new ArgumentNullException("moduleManager");
            }
            this.moduleManager = moduleManager;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.moduleManager.LoadModule(ModuleNames.UserManageModule);
        }
    }
}
