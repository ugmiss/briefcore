using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Dijkstra
{
    /// <summary>
    /// PlanCourse 缓存从源节点到其它任一节点的最小权值路径（路径表）
    /// </summary>
    public class PlanCourse
    {
        private Hashtable htPassedPath;

        public PlanCourse(List<Node> nodeList, string originID)
        {
            this.htPassedPath = new Hashtable();

            Node originNode = null;
            foreach (Node node in nodeList)
            {
                if (node.ID == originID)
                {
                    originNode = node;
                }
                else
                {
                    PassedPath pPath = new PassedPath(node.ID);
                    this.htPassedPath.Add(node.ID, pPath);
                }
            }
            if (originNode == null)
            {
                throw new Exception("The origin node is not exist !");
            }
            this.InitializeWeight(originNode);
        }

        /// <summary>
        /// 通过指定节点的边的权值初始化路径表
        /// </summary>
        /// <param name="originNode"></param>
        private void InitializeWeight(Node originNode)
        {
            if ((originNode.EdgeList == null) || (originNode.EdgeList.Count == 0))
            {
                return;
            }

            foreach (Edge edge in originNode.EdgeList)
            {
                PassedPath pPath = this[edge.EndNodeID];
                if (pPath == null)
                {
                    continue;
                }

                pPath.PassedIDList.Add(originNode.ID);
                pPath.Weight = edge.Weight;
            }
        }
        /// <summary>
        /// 获取指定点的路径表
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public PassedPath this[string nodeID]
        {
            get
            {
                return (PassedPath)this.htPassedPath[nodeID];
            }
        }
    }
}
