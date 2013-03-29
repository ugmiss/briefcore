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
            TestPrim();
            TestDijkstra();
            Console.ReadLine();
        }
        static void TestDijkstra()
        {
            Edge eAB = new Edge() { FromID = "A", ToID = "B", Weight = 10 };
            Edge eBA = new Edge() { FromID = "B", ToID = "A", Weight = 10 };

            Edge eAC = new Edge() { FromID = "A", ToID = "C", Weight = 30 };
            Edge eCA = new Edge() { FromID = "C", ToID = "A", Weight = 30 };


            Edge eBC = new Edge() { FromID = "B", ToID = "C", Weight = 10 };
            Edge eCB = new Edge() { FromID = "C", ToID = "B", Weight = 10 };

            Vertex vA = new Vertex() { ID = "A", EdgeList = new List<Edge>() { eAB, eAC } };
            Vertex vB = new Vertex() { ID = "B", EdgeList = new List<Edge>() { eBC, eBA } };
            Vertex vC = new Vertex() { ID = "C", EdgeList = new List<Edge>() { eCA, eCB } };

            //非负权有向图的最短路径Dijkstra算法。
            Route p = Dijkstra.GetShortestRoute(new List<Vertex>() { vA, vB, vC }, "A", "C");
        }

        static void TestPrim()
        {
            Edge eAB = new Edge() { FromID = "A", ToID = "B", Weight = 10 };
            Edge eBA = new Edge() { FromID = "B", ToID = "A", Weight = 10 };

            Edge eAC = new Edge() { FromID = "A", ToID = "C", Weight = 30 };
            Edge eCA = new Edge() { FromID = "C", ToID = "A", Weight = 30 };


            Edge eBC = new Edge() { FromID = "B", ToID = "C", Weight = 10 };
            Edge eCB = new Edge() { FromID = "C", ToID = "B", Weight = 10 };

            Vertex vA = new Vertex() { ID = "A", EdgeList = new List<Edge>() { eAB, eAC } };
            Vertex vB = new Vertex() { ID = "B", EdgeList = new List<Edge>() { eBC, eBA } };
            Vertex vC = new Vertex() { ID = "C", EdgeList = new List<Edge>() { eCA, eCB } };

            List<Edge> list = Prim.GetMinTree(new List<Vertex>() { vB, vA, vC });
            double sum = 0;
            foreach (Edge e in list)
            {
                sum += e.Weight;
            }
            Console.WriteLine("最小生成树的权值之和为" + sum);
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
