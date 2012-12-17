using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Utility;

namespace Algorithms.Genetic
{
    //进化
    public class Revolution
    {
        #region 非必要参数
        //交叉率 90%让孵化池中的个体交叉产生新个体，10%个体直接进入新生代。
        public static double CrossRate = 0.9;
        //突变率 1%基因位突变，引起物种多样性，进化或者退化。
        public static double MutationRate = 0.1;
        //精英率 10%的概率历史最优个体直接进入新生代，不参加选择，交叉和突变。 
        //精英率值越大收敛越快
        public static double EliteRate = 0.1;
        //免疫率
        public static double ImmuneRate = 0.1;
        //灾难率
        public static double DisasterRate = 0.01;
        #endregion

        #region 必要参数
        //种群长度初始 维持种群多样性，可通过环境影响，模拟灾难，移除部分解，并逐渐加入新个体。
        //种群长度越高，越可能产生最优解
        public int PopulationLength { get; set; }
        //孵化池长度 小于等于种群长度，小于时需要补充新生个体保持种群的
        public int SpawningPoolLength { get; set; }
        //染色体长度
        public int ChromosomeLength { get; set; }
        //当前代数
        public int CurrentGenarationIndex { get; set; }
        //当代种群
        public int[][] CurrentPopulation { get; set; }
        //历史最优解
        public int[] BestIndividual { get; set; }
        //最大代数
        public int MaxGenarationCount { get; set; }
        //适应度函数
        public Func<int[], double> FitnessFunc { get; set; }
        //染色体生成函数
        public Func<int[]> ChromosomeFunc { get; set; }
        //选择函数
        public Func<int[][], int[]> ChooseFunc { get; set; }
        //交叉函数
        public Func<int[], int[], int[][]> CrossFunc { get; set; }
        //变异方法
        public Action<int[]> MutationAction { get; set; }
        #endregion

        //初始数据
        public void InitData(int PopulationCount, int ChromosomeLength, int MaxGenarationCount, int SpawningPoolLength)
        {
            this.PopulationLength = PopulationCount;
            this.ChromosomeLength = ChromosomeLength;
            this.MaxGenarationCount = MaxGenarationCount;
            this.SpawningPoolLength = SpawningPoolLength;
        }
        //初始函数
        public void InitFunc(Func<int[], double> FitnessFunc, Func<int[]> ChromosomeFunc, Func<int[][], int[]> ChooseFunc, Func<int[], int[], int[][]> CrossFunc, Action<int[]> MutationAction)
        {
            this.FitnessFunc = FitnessFunc;
            this.ChromosomeFunc = ChromosomeFunc;
            this.ChooseFunc = ChooseFunc;
            this.CrossFunc = CrossFunc;
            this.MutationAction = MutationAction;
        }
        //开始进化
        public void Begin()
        {
            CurrentPopulation = new int[PopulationLength][];
            //初始化第一代
            Parallel.For(0, PopulationLength, i =>
            {
                int[] Chromosome = new int[ChromosomeLength];
                Chromosome = ChromosomeFunc();
                CurrentPopulation[i] = Chromosome;
            });

            do { Loop(); }
            while (MaxGenarationCount-- > 0);
        }


        public void Loop()
        {
            //新生代
            ConcurrentBag<int[]> NewGeneration = new ConcurrentBag<int[]>();
            //选择并生成孵化池
            ConcurrentBag<int[]> SpawningPool = new ConcurrentBag<int[]>();
            Parallel.For(0, SpawningPoolLength, i =>
            {   //选择
                SpawningPool.Add(ChooseFunc(CurrentPopulation));
            });
            //交叉
            Action action = new Action(() =>
            {
                do
                {
                    int[] ChromosomeA;
                    int[] ChromosomeB;
                    SpawningPool.TryTake(out ChromosomeA);
                    SpawningPool.TryTake(out ChromosomeB);
                    if (ChromosomeA != null && ChromosomeB != null)
                    {
                        int[][] ChromosomeABandBA = CrossFunc(ChromosomeA, ChromosomeB);
                        NewGeneration.Add(ChromosomeABandBA[0]);
                        NewGeneration.Add(ChromosomeABandBA[1]);
                    }
                }
                while (SpawningPool.Count != 0);
            });
            //起两个任务去执行交叉操作，不知道Task数是不是与双核有关，还待研究。
           Task t1= Task.Factory.StartNew(action);
           Task t2= Task.Factory.StartNew(action);
           Task.WaitAll(t1, t2);
            //变异
            if (RandomFactory.NextDouble() < MutationRate)
            {
                int[] Chromosomex;
                NewGeneration.TryPeek(out Chromosomex);
                MutationAction(Chromosomex);
            }
            //精英率 精英直接进入新生代
            if (BestIndividual != null && RandomFactory.NextDouble() < EliteRate)
            {
                //添加精英个体。
                NewGeneration.Add(BestIndividual);
            }
            CurrentPopulation = NewGeneration.OrderByDescending(p => FitnessFunc(p)).ToArray();
            if (BestIndividual == null || FitnessFunc(BestIndividual) < FitnessFunc(CurrentPopulation[0]))
                BestIndividual = CurrentPopulation[0];
        }
    }
}
