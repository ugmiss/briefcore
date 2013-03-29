using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Algorithms
{
    /// <summary>
    /// 非负权有向图的最短路径Dijkstra算法。
    /// </summary>
    public class Dijkstra
    {
        //求最短路径(其实已经求出了源点的最小路径树）
        public static Route GetShortestRoute(List<Vertex> vertexList, string startID, string endID)
        {
            Dictionary<string, Route> RouteCache = new Dictionary<string, Route>();
            Vertex currentVertex = null;
            foreach (Vertex vertex in vertexList)
            {
                Route route = new Route()
                {
                    EndVertexID = vertex.ID,
                    Weight = double.MaxValue,//初始路径权为无穷大
                    IDList = new List<string>(),
                    Flag = false
                };
                if (vertex.ID == startID)
                {   //初始起点为当前端点
                    currentVertex = vertex;
                    route.Weight = 0;
                }
                RouteCache.Add(route.EndVertexID, route);
            }
            while (currentVertex != null)
            {
                foreach (Edge edge in currentVertex.EdgeList)//遍历当前顶点的出方向边
                {
                    Route route = RouteCache[edge.ToID];//目标点的路径
                    Route routePrev = RouteCache[currentVertex.ID]; //前一段路径
                    double weightTemp = routePrev.Weight + edge.Weight;//计算临时新路径的权值=前一段路径与边的权值之和
                    if (weightTemp < route.Weight)
                    {   //新路径的权值如果比历史路径短，就替换历史路径
                        route.Weight = weightTemp;
                        route.IDList = new List<string>(routePrev.IDList);
                        route.IDList.Add(currentVertex.ID);
                    }
                }
                RouteCache[currentVertex.ID].Flag = true;//目标点计算结束，标示为true
                currentVertex = null;
                foreach (var vertex in vertexList)
                {
                    if (!RouteCache[vertex.ID].Flag && RouteCache[vertex.ID].Weight != double.MaxValue)
                        currentVertex = vertex;
                }
            }
            return RouteCache[endID].Weight == double.MaxValue ? null : RouteCache[endID];
        }
    }
}
