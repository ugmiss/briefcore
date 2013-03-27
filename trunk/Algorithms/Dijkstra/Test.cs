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
            DijkstraPlanner planner = new DijkstraPlanner();
            string start = "A";
            string end = "D";
            RoutePlanResult result = planner.Plan(graph.NodeList, start, end);
            Console.WriteLine(start + "到" + end + "的最短路径为："+string.Join("->", result.ResultNodes));
            Console.WriteLine("路径距离为：" + result.Value);
            Console.ReadLine();
        }
    }
}
