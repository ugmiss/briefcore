using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Genetic
{
    public class GeneticCache
    {
        //种群个体数量
        public static int PopulationCount { get; set; }
        //染色体长度
        public static int ChromosomeLength { get; set; }
        //当前代数
        public static int CurrentGenarationCount { get; set; }
        //当前最优解
        public static int[] BestIndividual { get; set; }
        //最大代数
        public static int MaxGenarationCount { get; set; }
        //适应度函数
        public static Func<int[], double> FitnessFunc { get; set; }
        //染色体生成函数
        public static Func<int[]> ChromosomeFunc { get; set; }
        //选择策略
        public static IChooseStrategy ChooseStrategy { get; set; }
        //交叉策略
        public static ICrossStrategy CrossStrategy { get; set; }
    }
}
