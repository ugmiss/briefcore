using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Algorithms
{
    public class Search
    {
        class Program
        {
            static void Main(string[] args)
            {
                for (int i = 0; i < 4; i++)
                {
                    DirectoryInfo root = new DirectoryInfo(@"E:\");
                    var sw = Stopwatch.StartNew();
                    int fileCount = TreeParallelSearch.TraverseTreeParallelForEach(root);
                    Console.WriteLine("Processed {0} files in {1} milleseconds", fileCount, sw.ElapsedMilliseconds);
                    sw = Stopwatch.StartNew();
                    int strBFS = BreadthFirstSearch.BFS(root);
                    Console.WriteLine("Processed {0} files in {1} milleseconds", strBFS, sw.ElapsedMilliseconds);
                    sw = Stopwatch.StartNew();
                    int strDFS = DepthFirstSearch.DFS(root);
                    Console.WriteLine("Processed {0} files in {1} milleseconds", strDFS, sw.ElapsedMilliseconds);
                }
                Console.ReadLine();
            }
        }
    }
}
