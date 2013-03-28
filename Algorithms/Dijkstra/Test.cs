using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Dijkstra
{
    public class Test
    {
        /// <summary>
        /// Dijkstra算法求图上一个端点到另一个端点的最小路径
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
            Console.WriteLine(start + "到" + end + "的最短路径为：" + string.Join("->", result.ResultNodes));
            Console.WriteLine("路径距离为：" + result.Value);
            Console.ReadLine();
        }
        public static void Try2()
        {
            int[,] L ={
                {-1,  5, -1, -1, -1,  3, -1, -1}, 
                { 5, -1,  2, -1, -1, -1,  3, -1}, 
                {-1,  2, -1,  6, -1, -1, -1, 10}, 
                {-1, -1,  6, -1,  3, -1, -1, -1},
                {-1, -1, -1,  3, -1,  8, -1,  5}, 
                { 3, -1, -1, -1,  8, -1,  7, -1}, 
                {-1,  3, -1, -1, -1,  7, -1,  2}, 
                {-1, -1, 10, -1,  5, -1,  2, -1} 
            };
            Dijkstra clss = new Dijkstra((int)Math.Sqrt(L.Length), L);
            clss.Run();
            Console.WriteLine("Solution is");
            foreach (int i in clss.D)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Press Enter for exit.");
            Console.Read();
        }
    }
}
