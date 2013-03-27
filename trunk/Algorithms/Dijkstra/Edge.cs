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
}
