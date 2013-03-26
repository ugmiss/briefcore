using System.Collections.Generic;
using System;

namespace Algorithms
{
    public class Edge
    {
        public string StartNodeID;
        public string EndNodeID;
        public double Weight; //权值，代价
    }
    public class Node
    {
        public string ID { get; set; }
        public List<Edge> EdgeList = new List<Edge>();//Edge的集合－－出边表
    }
    public class PassedPath
    {
        public string CurNodeID { get; set; }
        public bool BeProcessed; //是否已被处理
        public double Weight; //累积的权值
        public List<string> PassedIDList; //路径
        public PassedPath(string ID)
        {
            this.CurNodeID = ID;
            this.Weight = double.MaxValue;
            this.PassedIDList = new List<string>();
            this.BeProcessed = false;
        }
    }
    ///
    /// PlanCourse 缓存从源节点到其它任一节点的最小权值路径=》路径表
    ///
    public class PlanCourse
    {
        public Dictionary<string, PassedPath> htPassedPath = new Dictionary<string, PassedPath>();
        public PlanCourse(List<Node> nodeList, string originID)
        {
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
        public PassedPath this[string nodeID]
        {
            get
            {
                return (PassedPath)this.htPassedPath[nodeID];
            }
        }
    }
    public class RoutePlanResult
    {
        public RoutePlanResult(string[] passedNodes, double value)
        {
            m_resultNodes = passedNodes;
            m_value = value;
        }
        private string[] m_resultNodes;
        /// <summary>
        /// 最短路径经过的节点
        /// </summary>
        public string[] ResultNodes
        {
            get { return m_resultNodes; }
        }

        private double m_value;
        /// <summary>
        /// 最短路径的值
        /// </summary>
        private double Value
        {
            get { return m_value; }
        }
    }
    public class RoutePlanner
    {
        public RoutePlanner()
        {
        }

        #region Paln
        //获取权值最小的路径
        public RoutePlanResult Paln(List<Node> nodeList, string originID, string destID)
        {
            //初始化起始节点到其他节点的路径表(权值，经过的节点，是否被处理）
            //同时初始化其他节点的路径表
            PlanCourse planCourse = new PlanCourse(nodeList, originID);

            Node curNode = this.GetMinWeightRudeNode(planCourse, nodeList, originID);

            #region 计算过程
            while (curNode != null)
            {
                PassedPath curPath = planCourse[curNode.ID];
                foreach (Edge edge in curNode.EdgeList)
                {
                    PassedPath targetPath = planCourse[edge.EndNodeID];
                    double tempWeight = curPath.Weight + edge.Weight;

                    if (tempWeight < targetPath.Weight)
                    {
                        targetPath.Weight = tempWeight;
                        targetPath.PassedIDList.Clear();

                        for (int i = 0; i < curPath.PassedIDList.Count; i++)
                        {
                            targetPath.PassedIDList.Add(curPath.PassedIDList[i].ToString());
                        }

                        targetPath.PassedIDList.Add(curNode.ID);
                    }
                }

                //标志为已处理
                planCourse[curNode.ID].BeProcessed = true;
                //获取下一个未处理节点
                curNode = this.GetMinWeightRudeNode(planCourse, nodeList, originID);
            }
            #endregion

            //表示规划结束
            return this.GetResult(planCourse, destID);
        }
        #endregion

        #region private method
        #region GetResult

        /// <summary>
        /// 从PlanCourse表中取出目标节点的PassedPath，这个PassedPath即是规划结果
        /// </summary>
        /// <returns></returns>
        private RoutePlanResult GetResult(PlanCourse planCourse, string destID)
        {
            PassedPath pPath = planCourse[destID];

            if (pPath.Weight == int.MaxValue)
            {
                RoutePlanResult result1 = new RoutePlanResult(null, int.MaxValue);
                return result1;
            }

            string[] passedNodeIDs = new string[pPath.PassedIDList.Count];
            for (int i = 0; i < passedNodeIDs.Length; i++)
            {
                passedNodeIDs[i] = pPath.PassedIDList[i].ToString();
            }

            RoutePlanResult result = new RoutePlanResult(passedNodeIDs, pPath.Weight);

            return result;
        }
        #endregion

        #region GetMinWeightRudeNode

        /// <summary>
        /// 从PlanCourse取出一个当前累积权值最小，并且没有被处理过的节点
        /// </summary>
        /// <returns></returns>
        private Node GetMinWeightRudeNode(PlanCourse planCourse, List<Node> nodeList, string originID)
        {
            double weight = double.MaxValue;
            Node destNode = null;

            foreach (Node node in nodeList)
            {
                if (node.ID == originID)
                {
                    continue;
                }

                PassedPath pPath = planCourse[node.ID];
                if (pPath.BeProcessed)
                {
                    continue;
                }

                if (pPath.Weight < weight)
                {
                    weight = pPath.Weight;
                    destNode = node;
                }
            }

            return destNode;
        }
        #endregion
        #endregion
    }

}

