using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechTree
{
    public partial class NodeForm : Form
    {
        Node parent;
        public NodeForm(Node parent)
        {
            this.parent = parent;
            InitializeComponent();
        }
        public Node addNode = null;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            addNode = new Node();
            addNode.ID = IdFactory.NewGuid();
            if (parent != null)
                addNode.ParentID = parent.ID;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
