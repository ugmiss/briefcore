using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MJClient;
using System.Threading;

namespace MJ
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        void ClearFlow(FlowLayoutPanel flowLayoutPanel)
        {
            //foreach (Control c in flowLayoutPanel.Controls)
            //{
            //    if (c is Button)
            //        flowLayoutPanel.Controls.Remove(c);
            //}
            flowLayoutPanel.Controls.Clear();
        }

        void LoadData()
        {

            ClearFlow(flowLayoutPanel1);
            ClearFlow(flowLayoutPanel2);
            ClearFlow(flowLayoutPanel3);
            ClearFlow(flowLayoutPanel4);

            foreach (var ds in MJCache.H1)
            {
                Button btn = new Button();
                btn.Text = ds.Mname;
                btn.Tag = ds;
                flowLayoutPanel1.Controls.Add(btn);
                btn.Click += delegate
                {
                    flowLayoutPanel1.Controls.Remove(btn);
                    MJCache.H1.Remove(btn.Tag as MJClient.MJ);
                    Next();
                };
            }
            foreach (var ds in MJCache.H2)
            {
                Button btn = new Button();
                btn.Text = ds.Mname;
                btn.Tag = ds;
                flowLayoutPanel2.Controls.Add(btn);
                btn.Click += delegate
                {
                    flowLayoutPanel2.Controls.Remove(btn);
                    MJCache.H2.Remove(btn.Tag as MJClient.MJ);
                    Next();

                };
            }
            foreach (var ds in MJCache.H3)
            {
                Button btn = new Button();
                btn.Tag = ds;
                btn.Text = ds.Mname;
                flowLayoutPanel3.Controls.Add(btn);
                btn.Click += delegate
                {
                    flowLayoutPanel3.Controls.Remove(btn);
                    MJCache.H3.Remove(btn.Tag as MJClient.MJ);
                    Next();
                };
            }
            foreach (var ds in MJCache.H4)
            {
                Button btn = new Button();
                btn.Tag = ds;
                btn.Text = ds.Mname;
                flowLayoutPanel4.Controls.Add(btn);
                btn.Click += delegate
                {
                    flowLayoutPanel4.Controls.Remove(btn);
                    MJCache.H4.Remove(btn.Tag as MJClient.MJ);
                    Next();
                };
            }
        }
        int x = 1;
        MJLogic logic = new MJLogic();
        private void MainForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            logic.Fapai();
            LoadData();
            Next();
        }

        void Next()
        {
            while (true)
            {
                if (MJCache.H1.Count == 13 && MJCache.H2.Count == 13 && MJCache.H3.Count == 13 && MJCache.H4.Count == 13)
                {
                    logic.FapaiOne(x);

                    LoadData();
                    x++;
                }
                else
                    return;
            }
        }
    }
}
