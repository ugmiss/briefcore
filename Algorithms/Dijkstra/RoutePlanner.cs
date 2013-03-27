using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Dijkstra
{
    public class RoutePlanner
    {
        public RoutePlanner()
        {
        }
        //获取权值最小的路径
        public RoutePlanResult Plan(List<Node> nodeList, string originID, string destID)
        {
            //初始化起始节点到其他节点的路径表(权值，经过的节点，是否被处理）
            //同时初始化其他节点的路径表
            PlanCache planCourse = new PlanCache(nodeList, originID);
            Node curNode = this.GetMinWeightRudeNode(planCourse, nodeList, originID);
            while (curNode != null)
            {
                PassedPath curPath = planCourse[curNode.ID];
                foreach (Edge edge in curNode.EdgeList)
                {
                    if (edge.EndNodeID == originID) continue;
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
                planCourse[curNode.ID].HasProcessed = true;
                //获取下一个未处理节点
                curNode = this.GetMinWeightRudeNode(planCourse, nodeList, originID);
            }
            //表示规划结束
            return this.GetResult(planCourse, destID);
        }
        /// <summary>
        /// 从PlanCourse表中取出目标节点的PassedPath，这个PassedPath即是规划结果
        /// </summary>
        /// <returns></returns>
        private RoutePlanResult GetResult(PlanCache planCourse, string destID)
        {
            PassedPath pPath = planCourse[destID];
            if (pPath.Weight == double.MaxValue)
            {
                RoutePlanResult result1 = new RoutePlanResult(null, double.MaxValue);
                return result1;
            }
            string[] passedNodeIDs = new string[pPath.PassedIDList.Count + 1];
            for (int i = 0; i < passedNodeIDs.Length-1; i++)
            {
                passedNodeIDs[i] = pPath.PassedIDList[i].ToString();
            }
            passedNodeIDs[passedNodeIDs.Length-1] = destID;
            RoutePlanResult result = new RoutePlanResult(passedNodeIDs, pPath.Weight);
            return result;
        }
        /// <summary>
        /// 从PlanCourse取出一个当前累积权值最小，并且没有被处理过的节点
        /// </summary>
        /// <returns></returns>
        private Node GetMinWeightRudeNode(PlanCache planCourse, List<Node> nodeList, string originID)
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
                if (pPath.HasProcessed)
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
    }
}
