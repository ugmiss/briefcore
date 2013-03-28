using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Dijkstra
{
    public class Edge
    {
        public string StartNodeID;
        public string EndNodeID;
        public double Weight;
        public Edge(string sid, string eid, double w)
        {
            StartNodeID = sid;
            EndNodeID = eid;
            Weight = w;
        }
    }
    public class Node
    {
        public string ID { get; set; }
        public List<Edge> EdgeList { get; set; }//Edge的集合－－出边表

        public Node(string id)
        {
            this.ID = id;
            this.EdgeList = new List<Edge>();
        }
    }
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
    /// <summary>
    /// PassedPath 用于缓存计算过程中的到达某个节点的权值最小的路径
    /// </summary>
    public class PassedPath
    {
        public string CurNodeID { get; set; }
        public bool HasProcessed { get; set; }   //是否已被处理
        public double Weight { get; set; }        //累积的权值
        public List<string> PassedIDList { get; set; } //路径

        public PassedPath(string ID)
        {
            this.CurNodeID = ID;
            this.Weight = double.MaxValue;
            this.PassedIDList = new List<string>();
            this.HasProcessed = false;
        }
    }
    public class RoutePlanResult
    {
        public RoutePlanResult(string[] passedNodes, double value)
        {
            ResultNodes = passedNodes;
            Value = value;
        }
        public string[] ResultNodes { get; set; }
        public double Value { get; set; }
    }
}
