using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Algorithms.Graphs
{
    /// <summary>
    /// 有向图
    /// </summary>
    public class DirectGraph
    {
        /// <summary>
        /// 顶点集合
        /// </summary>
        public List<Vertex> Vertex_List = new List<Vertex>();
        /// <summary>
        /// 边集合
        /// </summary>
        public List<Edge> Edge_List = new List<Edge>();
        /// <summary>
        /// 添加顶点
        /// </summary>
        /// <param name="vertexArray"></param>
        public void Add(params Vertex[] vertexArray)
        {
            foreach (Vertex v in vertexArray)
            {
                Vertex_List.Add(v);
                Edge_List.AddRange(v.EdgeList);
            }
        }
        /// <summary>
        /// 添加边
        /// </summary>
        /// <param name="vertexFrom"></param>
        /// <param name="vertexTo"></param>
        public void AddEdge(Vertex vertexFrom, Vertex vertexTo)
        {
            Edge e = new Edge(vertexFrom, vertexTo);
            vertexFrom.EdgeList.Add(e);
        }
        /// <summary>
        /// 添加边
        /// </summary>
        /// <param name="vertexFrom">源点（主点，父节点）</param>
        /// <param name="vertexTo">目标点（从点，子节点）</param>
        public void AddEdge(string vertexFrom, string vertexTo)
        {
            //自引用的不处理，源点与目标点同名。
            if (vertexFrom == vertexTo) return;
            //已经引用过的不处理。
            foreach (Edge temp in Edge_List)
            {
                if (temp.From.ID == vertexFrom && temp.To.ID == vertexTo)
                    return;
            }
            //定义点的引用
            Vertex v1, v2;
            //取源点，如果已经在点集内直接取出，没有则创建新的点并添加到点集。
            var q = from c in Vertex_List where c.ID == vertexFrom select c;
            if (!(q.ToList().Count > 0))
            {
                v1 = new Vertex(vertexFrom);
                Vertex_List.Add(v1);
            }
            else
            {
                v1 = q.ToArray()[0];
            }
            //取目标点，如果已经在点集内直接取出，没有则创建新的点并添加到点集。
            var q2 = from c in Vertex_List where c.ID == vertexTo select c;
            if (!(q2.ToList().Count > 0))
            {
                v2 = new Vertex(vertexTo);
                Vertex_List.Add(v2);
            }
            else
            {
                v2 = q2.ToArray()[0];
            }
            //创建边并添加到边集合
            Edge e = new Edge(v1, v2);
            v1.EdgeList.Add(e);
            Edge_List.Add(e);
        }
        /// <summary>
        /// 有向图 ****拓扑排序算法****
        /// 遍历所有点，找到顶点，顶点判断依据所有打开边没有指向此点，
        /// 找到顶点后将顶点加入队列
        /// 关闭所有从顶点出发边
        /// 递归找顶点，同样操作直到所有的点都加入了队列
        /// 队列的顺序就是 偏序有向图的拓扑排序。
        /// </summary>
        /// <param name="g"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static Queue<Vertex> GetTopoList(DirectGraph g, Queue<Vertex> temp)
        {
            if (temp == null)
                temp = new Queue<Vertex>();
            foreach (Vertex v in g.Vertex_List)
            {
                if (temp.Contains(v)) continue;
                bool isTop = true;
                foreach (Edge e in g.Edge_List)
                {
                    if (e.To == v && e.Open)
                    {
                        isTop = false;
                        break;
                    }
                }
                if (isTop)
                {
                    foreach (Edge e in v.EdgeList)
                    {
                        e.Open = false;
                    }
                    temp.Enqueue(v);
                }
            }
            if (temp.Count == g.Vertex_List.Count)
                return temp;
            else
                return GetTopoList(g, temp);
        }
        /// <summary>
        /// 获取有向拓扑图名称的有序数组。
        /// </summary>
        /// <param name="g"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string[] GetTopoListNames(DirectGraph g, Queue<Vertex> temp)
        {
            Queue<Vertex> q = GetTopoList(g, temp);
            return (from c in q select c.ID).ToArray();
        }
    }
    /// <summary>
    /// 向量边
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// 打开状态
        /// </summary>
        public bool Open { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 起点
        /// </summary>
        public Vertex From { get; set; }
        /// <summary>
        /// 终点
        /// </summary>
        public Vertex To { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="f"></param>
        /// <param name="t"></param>
        public Edge(Vertex f, Vertex t)
        {
            Open = true;
            From = f;
            To = t;
        }
    }
    /// <summary>
    /// 顶点
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// 顶点名称
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 此顶点出发的边的集合
        /// </summary>
        public List<Edge> EdgeList = new List<Edge>();
        /// <summary>
        /// 构造方法
        /// </summary>
        public Vertex() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="n"></param>
        public Vertex(string n) { ID = n; }
    }
}
