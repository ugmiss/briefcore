using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Algorithms.Genetic
{
    //进化
    public class Revolution
    {
        #region 非必要参数
        //交叉率 90%让孵化池中的个体交叉产生新个体，10%个体直接进入新生代。
        public static double CrossRate = 0.9;
        //突变率 1%基因位突变，引起物种多样性，进化或者退化。
        public static double MutationRate = 0.01;
        //精英权重 
        public static double EliteRight = 10;
        //轮盘权重
        public static double RouletteRight = 90;
        //免疫率
        public static double ImmuneRate = 0.1;
        #endregion

        #region 必要参数
        //种群长度
        public int PopulationLength { get; set; }
        //孵化池长度
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
        public Func<int[][], int[][]> CrossFunc { get; set; }
        #endregion

        //初始数据
        public void InitData(int PopulationCount, int ChromosomeLength, int MaxGenarationCount)
        {
            this.PopulationLength = PopulationCount;
            this.ChromosomeLength = ChromosomeLength;
            this.MaxGenarationCount = MaxGenarationCount;
        }
        //初始函数
        public void InitFunc(Func<int[], double> FitnessFunc, Func<int[]> ChromosomeFunc)
        {
            this.FitnessFunc = FitnessFunc;
            this.ChromosomeFunc = ChromosomeFunc;
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
            //选择并生成孵化池
            ConcurrentBag<int[]> SpawningPool = new ConcurrentBag<int[]>();
            Parallel.For(0, SpawningPoolLength, i =>
            {
                SpawningPool.Add(ChooseFunc(CurrentPopulation));
            });
        }
    }
}
