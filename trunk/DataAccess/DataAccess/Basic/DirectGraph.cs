using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DataAccess
{
    public class DirectGraph
    {
        public List<Vertex> V_List = new List<Vertex>();
        public List<Edge> E_List = new List<Edge>();
        public void Add(params Vertex[] vs)
        {
            foreach (Vertex v in vs)
            {
                V_List.Add(v);
                E_List.AddRange(v.Follow_E);
            }
        }
        public void AddE(Vertex vFrom, Vertex vTo)
        {
            Edge e = new Edge(vFrom, vTo);
            vFrom.Follow_E.Add(e);
        }
        public void AddE(string vFrom, string vTo)
        {
            Vertex v1, v2;
            var q = from c in V_List where c.Name == vFrom select c;
            if (!(q.ToList().Count > 0))
            {
                v1 = new Vertex(vFrom);
                V_List.Add(v1);
            }
            else
            {
                v1 = q.ToArray()[0];
            }
            var q2 = from c in V_List where c.Name == vTo select c;
            if (!(q2.ToList().Count > 0))
            {
                v2 = new Vertex(vTo);
                V_List.Add(v2);
            }
            else
            {
                v2 = q2.ToArray()[0];
            }

            Edge e = new Edge(v1, v2);
            v1.Follow_E.Add(e);
            E_List.Add(e);
        }

        public static Queue<Vertex> GetTopoList(DirectGraph g, Queue<Vertex> temp)
        {
            if (temp == null)
                temp = new Queue<Vertex>();
            foreach (Vertex v in g.V_List)
            {
                if (temp.Contains(v)) continue;
                bool isTop = true;
                foreach (Edge e in g.E_List)
                {
                    if (e.To == v && e.Open)
                    {
                        isTop = false;
                        break;
                    }
                }
                if (isTop)
                {
                    foreach (Edge e in v.Follow_E)
                    {
                        e.Open = false;
                    }
                    bool noE = (from c in g.E_List where c.Open select c).ToList().Count == 0;
                    if (noE)
                    {
                        temp.Enqueue(v);
                        continue;
                    }
                    temp.Enqueue(v);
                }
            }
            if (temp.Count == g.V_List.Count)
                return temp;
            else
                return GetTopoList(g, temp);
        }
        public static string[] GetTopoListNames(DirectGraph g, Queue<Vertex> temp)
        {
            Queue<Vertex> q = GetTopoList(g, temp);
            return (from c in q select c.Name).ToArray();
        }
    }
    public class Edge
    {
        public bool Open { get; set; }
        public int Weight { get; set; }
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public Edge(Vertex f, Vertex t)
        {
            Open = true;
            From = f;
            To = t;
        }
    }
    public class Vertex
    {
        public string Name { get; set; }
        public object Tag { get; set; }
        public List<Edge> Follow_E = new List<Edge>();
        public Vertex() { }
        public Vertex(string n) { Name = n; }
    }
}
