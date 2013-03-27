using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Algorithms.Dijkstra
{
    /// <summary>
    /// PassedPath 用于缓存计算过程中的到达某个节点的权值最小的路径
    /// </summary>
    public class PassedPath
    {
        public string CurNodeID { get; set; }
        public bool HasProcessed { get; set; }   //是否已被处理
        public double Weight { get; set; }        //累积的权值
        public List<string> PassedIDList { get; set; } //路径

        public PassedPath(string ID)
        {
            this.CurNodeID = ID;
            this.Weight = double.MaxValue;
            this.PassedIDList = new List<string>();
            this.HasProcessed = false;
        }
    }
}
