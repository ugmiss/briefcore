using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Dijkstra
{

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
}
