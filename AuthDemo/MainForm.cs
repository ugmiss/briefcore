﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spring.Aop.Framework;

namespace AuthDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "admin";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "eo";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.CurrrentUser = "mo";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProxyFactory fac = new ProxyFactory(new Data());
            fac.AddAdvice(new DataAroundAdvise());
            IData idata = (IData)fac.GetProxy();
            string res = idata.GetData();
            MessageBox.Show(res);
        }
    }
}
