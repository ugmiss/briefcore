using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Algorithms
{
    public class Program
    {
        static void Main(string[] args)
        {
            TestDijkstra();
            Console.ReadLine();
        }
        static void TestDijkstra()
        {
            Edge eAB = new Edge() { FromID = "A", ToID = "B", Weight = 10 };
            Edge eAC = new Edge() { FromID = "A", ToID = "C", Weight = 30 };
            Vertex vA = new Vertex() { ID = "A", EdgeList = new List<Edge>() { eAB, eAC } };
            Edge eBC = new Edge() { FromID = "B", ToID = "C", Weight = 10 };
            Vertex vB = new Vertex() { ID = "B", EdgeList = new List<Edge>() { eBC } };
            Vertex vC = new Vertex() { ID = "C", EdgeList = new List<Edge>() { } };
            Vertex vD = new Vertex() { ID = "D", EdgeList = new List<Edge>() { } };
            //非负权有向图的最短路径Dijkstra算法。
            Route p = Dijkstra.GetShortestRoute(new List<Vertex>() { vA, vB, vC, vD }, "A", "C");
        }

        static void TestKMP()
        {
            string zstr = "ababcabababdc";
            string mstr = "babdc";
            var index = KMP.Find(zstr, mstr);
            if (index == -1)
                Console.WriteLine("没有匹配的字符串！");
            else
                Console.WriteLine("找到字符位置为：" + index);
            Console.Read();
        }

        static void TestSeach()
        {
            for (int i = 0; i < 4; i++)
            {
                DirectoryInfo root = new DirectoryInfo(@"E:\");
                var sw = Stopwatch.StartNew();
                int strBFS = BreadthFirstSearch.BFS(root);
                Console.WriteLine("Processed {0} files in {1} milleseconds", strBFS, sw.ElapsedMilliseconds);
                sw = Stopwatch.StartNew();
                int strDFS = DepthFirstSearch.DFS(root);
                Console.WriteLine("Processed {0} files in {1} milleseconds", strDFS, sw.ElapsedMilliseconds);
            }
        }
    }
}
