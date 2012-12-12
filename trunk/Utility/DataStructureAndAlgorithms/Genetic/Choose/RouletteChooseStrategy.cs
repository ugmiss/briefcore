using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic.Choose
{
    /// <summary>
    /// 轮盘赌选择策略
    /// </summary>
    public class RouletteChooseStrategy : IChooseStrategy
    {
        public Individual[] GetNewChoose(Individual[] population)
        {
            List<Individual> result = new List<Individual>();
            do
            {
                double m = 0;
                double r = RandomFactory.NextDouble(); //r为0至1的随机数
                for (int i = 0; i < Environment.PopulationCount; i++)
                {
                    if (population[i].ProbabilityOfSelect == 0)
                        continue;
                    m = m + population[i].ProbabilityOfSelect;
                    if (r <= m)
                    {
                        result.Add(population[i]);
                        break;
                    }
                }
            }
            while (result.Count != Environment.PopulationCount);
            if (result.Count == 0)
                throw new Exception("s");
            return result.ToArray();
        }
    }
}
