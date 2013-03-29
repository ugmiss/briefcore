using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    //普里姆算法（最小生成树算法）
    public class Prim
    {
        public static List<Edge> GetMinTree(List<Vertex> vertexList)
        {
            List<Edge> result = new List<Edge>();
            List<Vertex> inlist = new List<Vertex>();
            inlist.Add(vertexList[0]);
            while (inlist.Count != vertexList.Count)
            {
                List<Edge> list = new List<Edge>();
                foreach (Vertex vertext in inlist)
                {
                    foreach (Edge e in vertext.EdgeList)
                    {
                        if (inlist.Find(p => p.ID == e.ToID) == null)
                            list.Add(e);
                    }
                }
                Edge tempedge = list.OrderBy(p => p.Weight).First();
                result.Add(tempedge);
                inlist.Add(vertexList.Find(p => p.ID == tempedge.ToID));
            }
            return result;
        }
    }
}
