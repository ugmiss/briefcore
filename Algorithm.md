参考http://972169909-qq-com.iteye.com/blog/1463593

## 基本遍历 ##
深度优先搜索 · 广度优先搜索 · A**· Flood fill
## 最短路径 ##
[Dijkstra](Dijkstra.md)· Bellman-Ford · Floyd-Warshall · Kneser图
## 最小生成树 ##
[Prim](Prim.md) · Kruskal
## 强连通分量 ##
Kosaraju算法 · Gabow算法 · Tarjan算法
## 图匹配 ##
匈牙利算法 · Hopcroft–Karp · Edmonds's matching
## 网络流 ##
Ford-Fulkerson · Edmonds-Karp · Dinic · Push-relabel maximum flow**



### 冒泡排序 ###
### 二分查找 ###
### 递归 ###
### 回溯 ###
### 深度优先 ###
[DFS](DFS.md)
### 广度优先 ###
[BFS](BFS.md)
### 有向拓扑排序 ###
### 最小树形图 ###
### 凸包 ###
图象处理
模式识别
地理信息系统
### KMP算法 ###
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupportCenter.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            string zstr = "ababcabababdc";
            string mstr = "babdc";
            var index = KMP(zstr, mstr);
            if (index == -1)
                Console.WriteLine("没有匹配的字符串！");
            else
                Console.WriteLine("哈哈，找到字符啦，位置为：" + index);
            Console.Read();
        }
        static int KMP(string bigstr, string smallstr)
        {
            int i = 0;
            int j = 0;
            //计算“前缀串”和“后缀串“的next  
            int[] next = GetNextVal(smallstr);
            while (i < bigstr.Length && j < smallstr.Length)
            {
                if (j == -1 || bigstr[i] == smallstr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];
                }
            }
            if (j == smallstr.Length)
                return i - smallstr.Length;
            return -1;
        }

        /// <summary>  
        /// p0,p1....pk-1         （前缀串）  
        /// pj-k,pj-k+1....pj-1   （后缀串)  
        /// </summary>  
        /// <param name="match"></param>  
        /// <returns></returns>  
        static int[] GetNextVal(string smallstr)
        {
            //前缀串起始位置("-1"是方便计算）  
            int k = -1;
            //后缀串起始位置（"-1"是方便计算）  
            int j = 0;
            int[] next = new int[smallstr.Length];
            //根据公式： j=0时，next[j]=-1  
            next[j] = -1;
            while (j < smallstr.Length - 1)
            {
                if (k == -1 || smallstr[k] == smallstr[j])
                {
                    //pk=pj的情况: next[j+1]=k+1 => next[j+1]=next[j]+1  
                    next[++j] = ++k;
                }
                else
                {
                    //pk != pj 的情况:我们递推 k=next[k];  
                    //要么找到，要么k=-1中止  
                    k = next[k];
                }
            }
            return next;
        }
    }
}
```
### viterbi算法 ###
```
package algorithm;

public class Viterbi {
	/**
	 * 维特比算法（Viterbi algorithm）是一种动态规划算法。它用于寻找最有可能产生观测事件序列的-维特比路径-隐含状态序列,特别是在马尔可夫信息源上下文和隐马尔可夫模型中。
术语“维特比路径”和“维特比算法”也被用于寻找观察结果最有可能解释相关的动态规划算法。例如在统计句法分析中动态规划算法可以被用于发现最可能的上下文无关的派生(解析)的字符串，有时被称为“维特比分析”。
	 * @param args
	 */
	public static void main(String[] args) {
		// 用分词举例有如下结构.采用少词方式
		// 0 中 中国 中国人
		// 1 国 国人
		// 2 人 人民
		// 构建一个数组将如上结构放入数组中

		Node begin = new Node("B", 0);
		begin.score = 1 ;
		Node end = new Node("END", 5);
		Node[][] graph = new Node[6][0];
		graph[0] = new Node[] { begin };
		graph[1] = new Node[] { new Node("中", 1), new Node("中国", 1), new Node("中国人", 1) };
		graph[2] = new Node[] { new Node("国", 2), new Node("国人", 2) };
		graph[3] = new Node[] { new Node("人", 3), new Node("人民", 3) };
		graph[4] = new Node[] { new Node("民", 4) };
		graph[5] = new Node[] { end };

		int to = 0;
		Node node = null;

		// viterbi寻找最优路径
		for (int i = 0; i < graph.length - 1; i++) {
			for (int j = 0; j < graph[i].length; j++) {
				node = graph[i][j];
				to = node.getTo();
				for (int k = 0; k < graph[to].length; k++) {
					graph[to][k].setFrom(node);
				}
			}
		}

		// 反向遍历寻找结果
		node = graph[5][0];
		while ((node = node.getFrom()) != null) {
			System.out.println(node.getName());
		}

	}

	static class Node {
		private String name;
		private Node from;
		private int offe;
		private Integer score;

		public Node(String name, int offe) {
			this.name = name;
			this.offe = offe;
		}

		public Node(Node node, Node node2, Node node3) {
			// TODO Auto-generated constructor stub
		}

		public String getName() {
			return name;
		}

		public void setName(String name) {
			this.name = name;
		}

		public Node getFrom() {
			return from;
		}

		public void setFrom(Node from) {
			if (this.score == null) {
				this.score = from.score + 1;
				this.from = from;
			} else if (this.score > from.score + 1) {
				this.score = from.score + 1;
				this.from = from;
			}
		}

		public int getTo() {
			return this.offe + name.length();
		}

	}
}

```
### 最短路径算法 ###
Dijkstra算法
A\*算法
Bellman-Ford算法
SPFA算法 (Bellman-Ford算法的改进版本)
Floyd-Warshall算法
Johnson算法
Bi-Direction BFS算法
### Flood fill算法 ###
是从一个区域中提取若干个连通的点与其他相邻区域区分开（或分别染成不同颜色）的经典算法。因为其思路类似洪水从一个区域扩散到所有能到达的区域而得名
### 遗传算法 ###
```
Environment = "环境";
Population = "种群";
Individual = "个体";
Chromosome = "染色体";
Fitness = "适应度";
Probability = "选择概率";
Elite = "精英";
Roulette = "轮盘赌";
Choose = "选择";
SpawningPool = "孵化池";
Cross = "交叉";
Mutation = "突变";
Revolution = "进化";
```