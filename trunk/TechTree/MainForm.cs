using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace TechTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 组织集合
        List<Node> list;
        // 加载树
        void Loadtree()
        {
            treeList1.BeginUnboundLoad();
            treeList1.ClearNodes();
            list = GetList();
            Node root = list.Find(p => p.ParentID == null);
            if (root != null)
            {
                TreeListNode node = this.treeList1.AppendNode(new object[] { root.NodeName }, null);
                node.Tag = root;
                BuildTree(node);
            }
            treeList1.EndUnboundLoad();
            treeList1.ExpandAll();
        }
        // 加载树节点
        void BuildTree(TreeListNode node)
        {
            Node parent = node.Tag as Node;
            foreach (var s in list.ToArray())
            {
                if (s.ParentID == parent.ID)
                {
                    TreeListNode cnode = this.treeList1.AppendNode(new object[] { s.NodeName }, node);
                    cnode.Tag = s;
                    BuildTree(cnode);
                }
            }
        }
        // 添加组织
        void btiAddOrg_Click(object sender, EventArgs e)
        {
            Node parent = null;
            if (treeList1.FocusedNode != null)
                parent = treeList1.FocusedNode.Tag as Node;
            NodeForm form = new NodeForm(parent);
            form.ShowDialog();
            if (form.addNode != null)
            {
                list.Add(form.addNode);
                SaveList();
            }
            Loadtree();
        }
        // 删除组织
        void btiDelOrg_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode.Tag == null)
            {
                MessageBox.Show("未选中任何节点。");
                return;
            }
            Node parent = treeList1.FocusedNode.Tag as Node;
            list.Remove(parent);
            SaveList();
            Loadtree();
        }
        // 右键菜单
        void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(Control.MousePosition);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loadtree();
        }

        void SaveList()
        {
            list.XmlSerialize().WriteToFile8("data.xml");
        }
        List<Node> GetList()
        {
            string xml = "".ReadFromFile("data.xml");
            if (xml.NotNullOrEmpty())
                return xml.XmlDeserialize<List<Node>>();
            else
                return new List<Node>();
        }
    }

    public class Node
    {
        public string ID { get; set; }
        public string NodeName { get; set; }
        public string ParentID { get; set; }
    }
}
