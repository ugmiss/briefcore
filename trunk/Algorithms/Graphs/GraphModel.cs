﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class Route
    {
        public string EndVertexID { get; set; }  //终点
        public bool Flag { get; set; }   //是否已被处理
        public double Weight { get; set; }       //累积的权值
        public List<string> IDList { get; set; } //路径
        public string RouteString
        {
            get
            {
                return string.Join(",", IDList) + "," + EndVertexID;
            }
        }
    }
    public class Vertex
    {
        public string ID { get; set; } //顶点ID
        public List<Edge> EdgeList { get; set; }//出边
        public object Tag { get; set; }
    }
    public class Edge
    {
        public string FromID { get; set; }
        public string ToID { get; set; }
        public double Weight { get; set; }
    }
}
