using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Dijkstra
{
    public class Test
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        public static void Try()
        {
            Graph graph = new Graph();
            List<Edge> list = new List<Edge>();
            list.Add(new Edge("A", "B", 10));
            list.Add(new Edge("A", "E", 30));
            list.Add(new Edge("A", "C", 20));
            list.Add(new Edge("C", "B", 5));
            list.Add(new Edge("E", "B", 10));
            list.Add(new Edge("E", "D", 20));
            list.Add(new Edge("C", "D", 30));
            graph.Init(list);
            RoutePlanner planner = new RoutePlanner();
            RoutePlanResult result = planner.Plan(graph.NodeList, "A", "D");
            Console.WriteLine(string.Join("->", result.ResultNodes));
            Console.ReadLine();
        }
    }
}
