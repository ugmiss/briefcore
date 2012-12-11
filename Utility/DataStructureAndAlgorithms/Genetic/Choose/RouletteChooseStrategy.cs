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
        public int GetIndividualIndex(Population population)
        {

            double m = 0;
            double r = RandomFactory.NextDouble(); //r为0至1的随机数
            for (int i = 1; i <= Environment.PopulationCount; i++)
            {
                m = m + population.IndividualList[i].ProbabilityOfSelect;
                if (r <= m)
                    return i;
            }
            return 0;
        }

    }
}
