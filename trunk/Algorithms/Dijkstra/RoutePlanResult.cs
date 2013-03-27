using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Dijkstra
{
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
