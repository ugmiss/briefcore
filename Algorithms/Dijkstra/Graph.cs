using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Dijkstra
{
    public class Graph
    {
        public Graph()
        {
        }
        public List<Node> NodeList { get; set; }
        public void Init(List<Edge> EdgeList)
        {
            List<Node> nodelist = new List<Node>();
            foreach (Edge e in EdgeList)
            {
                Node n1 = nodelist.Find(p => p.ID == e.StartNodeID);
                Node n2 = nodelist.Find(p => p.ID == e.EndNodeID);
                if (n1 == null)
                {
                    n1 = new Node(e.StartNodeID);
                    nodelist.Add(n1);
                }
                n1.EdgeList.Add(e);
                if (n2 == null)
                {
                    n2 = new Node(e.EndNodeID);
                    nodelist.Add(n2);
                }
                Edge te = new Edge(e.EndNodeID, e.StartNodeID, e.Weight);
                n2.EdgeList.Add(te);
            }
            NodeList = nodelist;
        }
    }
}
