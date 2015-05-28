# Dijkstra #
带权边的有向图或无向图上两点的最短路径求法。
### 权（weight）: ###
可以理解为边的长度;
### 松散 ###
```
A-B:20 
B-C:10
A-C:40
```
当AB+BC<AC称为松散
### 算法 ###
通过初始化所有边 令距离为无穷大（double.MaxValue)
从起点开始搜索邻边，并缓存，当发现到达一个点的距离小于缓存距离时，更新此缓存搜索遍历到所有顶点结束，缓存中就有这个顶点到所有点的最短路径的集合。也就找出了两点间的最短路径。

## 对象类 ##
```
    public class Edge
    {
        public string StartNodeID;
        public string EndNodeID;
        public double Weight;
        public Edge(string sid, string eid, double w)
        {
            StartNodeID = sid;
            EndNodeID = eid;
            Weight = w;
        }
    }
    public class Node
    {
        public string ID { get; set; }
        public List<Edge> EdgeList { get; set; }//Edge的集合－－出边表

        public Node(string id)
        {
            this.ID = id;
            this.EdgeList = new List<Edge>();
        }
    }
    public class Graph
    {
        public Graph()
        {
        }
        public List<Node> NodeList { get; set; }
        public void Init(List<Edge> EdgeList)
        {
            List<Node> nodelist = new List<Node>();
            foreach (Edge e in EdgeList)
            {
                Node n1 = nodelist.Find(p => p.ID == e.StartNodeID);
                Node n2 = nodelist.Find(p => p.ID == e.EndNodeID);
                if (n1 == null)
                {
                    n1 = new Node(e.StartNodeID);
                    nodelist.Add(n1);
                }
                n1.EdgeList.Add(e);
                if (n2 == null)
                {
                    n2 = new Node(e.EndNodeID);
                    nodelist.Add(n2);
                }
                Edge te = new Edge(e.EndNodeID, e.StartNodeID, e.Weight);
                n2.EdgeList.Add(te);
            }
            NodeList = nodelist;
        }
    }
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
    public class RoutePlanResult
    {
        public RoutePlanResult(string[] passedNodes, double value)
        {
            ResultNodes = passedNodes;
            Value = value;
        }
        public string[] ResultNodes { get; set; }
        public double Value { get; set; }
    }
```
## 缓存 ##
```
/// <summary>
    /// PlanCourse 缓存从源节点到其它任一节点的最小权值路径（路径表）
    /// </summary>
    public class PlanCache
    {
        private Hashtable htPassedPath;

        public PlanCache(List<Node> nodeList, string originID)
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
```

## 计划 ##
```
public class DijkstraPlanner
    {
        public DijkstraPlanner()
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
```